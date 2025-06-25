using System.Collections.Generic;
using System.Linq;
using Gameplay.Selection;
using UI.Components;
using UI.Elements;
using UnityEngine;

namespace UI.Selection
{
    public class SelectionPanel : MonoBehaviour
    {
        [SerializeField] SelectionManager selectionManager;
        [SerializeField] GameObject selectionPanel;
        [SerializeField] GameObject viewParent;
        [SerializeField] Selector selector;

        Selectable[] selection;

        // Update is called once per frame
        void Update()
        {

        }

        void OnEnable() => selectionManager.OnSelectionChanged += SelectionChanged;
        void OnDisable() => selectionManager.OnSelectionChanged -= SelectionChanged;

        public void SelectionChanged(IEnumerable<Selectable> selectables)
        {
            selection = selectables.ToArray();
            if (selection.Length == 0)
            {
                selectionPanel.SetActive(false);
                return;
            }
            selectionPanel.SetActive(true);
            selector.SetOptions(selectables.Select(s => s.name));
            // UpdateView(selection[selectionIndex]);
        }

        public void UpdateView(int index)
        {
            foreach(RectTransform child in viewParent.transform)
            {
                Destroy(child.gameObject);
            }
            if (index == -1)
                return;
            if(selection[index].TryGetComponent<ISelectableUi>(out var selectableUi))
                selectableUi.ApplySelectionUi(viewParent);
        }
    }
}
