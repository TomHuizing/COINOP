using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectionManager", menuName = "Singletons/SelectionManager")]
public class SelectionManager : ScriptableObject
{
    private readonly List<Selectable> selected = new();

    public IEnumerable<Selectable> Selected => selected;

    public event Action<IEnumerable<Selectable>> OnSelectionChanged;

    public void Select(Selectable selectable)
    {
        if (selectable == null)
            return;
        if (selected.Count == 1 && selected.Contains(selectable))
                return;
        foreach (Selectable s in selected.Where(s => s != selectable).ToList())
            DeselectInternal(s);
        if (!selected.Contains(selectable))
            SelectInternal(selectable);
        OnSelectionChanged?.Invoke(selected);
    }

    public void Select(IEnumerable<Selectable> selectables)
    {
        if (selectables == null)
            return;
        if (selectables.Count() == selected.Count && selectables.All(s => selected.Contains(s)))
                return;
        foreach (Selectable s in selected.Where(s => !selectables.Contains(s)).ToList())
            DeselectInternal(s);
        foreach (Selectable s in selectables.Where(s => !selected.Contains(s)).ToList())
            SelectInternal(s);
        OnSelectionChanged?.Invoke(selected);
    }

    public void AddToSelection(Selectable selectable)
    {
        if (selectable == null)
            return;
        if (selected.Contains(selectable))
                return;
        SelectInternal(selectable);
        OnSelectionChanged?.Invoke(selected);
    }

    public void AddToSelection(IEnumerable<Selectable> selectables)
    {
        if (selectables == null)
            return;
        if (selectables.All(s => selected.Contains(s)))
                return;
        foreach (Selectable s in selectables.Where(s => !selected.Contains(s)).ToList())
            SelectInternal(s);
        OnSelectionChanged?.Invoke(selected);
    }

    private void SelectInternal(Selectable selectable)
    {
        if (selected.Contains(selectable))
            return;
        selected.Add(selectable);
        selectable.OnSelected?.Invoke();
    }

    public void Deselect(Selectable selectable)
    {
        if (selectable == null)
            return;
        if (!selected.Contains(selectable))
                return;
        DeselectInternal(selectable);
        OnSelectionChanged?.Invoke(selected);
    }

    public void Deselect(IEnumerable<Selectable> selectables)
    {
        if (selectables == null)
            return;
        if (selectables.All(s => !selected.Contains(s)))
                return;
        foreach (Selectable s in selectables.Where(s => selected.Contains(s)).ToList())
            DeselectInternal(s);
        OnSelectionChanged?.Invoke(selected);
    }

    public void DeselectAll()
    {
        if (selected.Count == 0)
            return;
        foreach (Selectable s in selected.ToList())
                DeselectInternal(s);
        OnSelectionChanged?.Invoke(selected);
    }
    
    private void DeselectInternal(Selectable selectable)
    {
        if (!selected.Contains(selectable))
            return;
        selected.Remove(selectable);
        selectable.OnDeselected?.Invoke();
    }
}
