using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UI.Interaction
{
    [CreateAssetMenu(fileName = "SelectionManager", menuName = "Singletons/SelectionManager")]
    public class SelectionManager : ScriptableSingleton<SelectionManager>
    {
        private readonly List<ISelectable> selected = new();

        public IEnumerable<ISelectable> Selection => selected;

        public event Action<IEnumerable<ISelectable>> OnSelectionChanged;

        public void Select(ISelectable selectable)
        {
            if (selectable == null)
                return;
            if (selected.Count == 1 && selected.Contains(selectable))
                    return;
            foreach (ISelectable s in selected.Where(s => s != selectable).ToList())
                DeselectInternal(s);
            if (!selected.Contains(selectable))
                SelectInternal(selectable);
            OnSelectionChanged?.Invoke(selected);
        }

        public void Select(IEnumerable<ISelectable> selectables)
        {
            if (selectables == null)
                return;
            if (selectables.Count() == selected.Count && selectables.All(s => selected.Contains(s)))
                    return;
            foreach (ISelectable s in selected.Where(s => !selectables.Contains(s)).ToList())
                DeselectInternal(s);
            foreach (ISelectable s in selectables.Where(s => !selected.Contains(s)).ToList())
                SelectInternal(s);
            OnSelectionChanged?.Invoke(selected);
        }

        public void AddToSelection(ISelectable selectable)
        {
            if (selectable == null)
                return;
            if (selected.Contains(selectable))
                    return;
            SelectInternal(selectable);
            OnSelectionChanged?.Invoke(selected);
        }

        public void AddToSelection(IEnumerable<ISelectable> selectables)
        {
            if (selectables == null)
                return;
            if (selectables.All(s => selected.Contains(s)))
                    return;
            foreach (ISelectable s in selectables.Where(s => !selected.Contains(s)).ToList())
                SelectInternal(s);
            OnSelectionChanged?.Invoke(selected);
        }

        private void SelectInternal(ISelectable selectable)
        {
            if (selected.Contains(selectable))
                return;
            selected.Add(selectable);
            selectable.SelectCallback();
        }

        public void Deselect(ISelectable selectable)
        {
            if (selectable == null)
                return;
            if (!selected.Contains(selectable))
                    return;
            DeselectInternal(selectable);
            OnSelectionChanged?.Invoke(selected);
        }

        public void Deselect(IEnumerable<ISelectable> selectables)
        {
            if (selectables == null)
                return;
            if (selectables.All(s => !selected.Contains(s)))
                    return;
            foreach (ISelectable s in selectables.Where(s => selected.Contains(s)).ToList())
                DeselectInternal(s);
            OnSelectionChanged?.Invoke(selected);
        }

        public void DeselectAll()
        {
            if (selected.Count == 0)
                return;
            foreach (ISelectable s in selected.ToList())
                    DeselectInternal(s);
            OnSelectionChanged?.Invoke(selected);
        }
    
        private void DeselectInternal(ISelectable selectable)
        {
            if (!selected.Contains(selectable))
                return;
            selected.Remove(selectable);
            selectable.DeselectCallback();
        }
    }
}
