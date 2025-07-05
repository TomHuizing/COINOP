using System.Collections.Generic;
using System.Linq;
using UI.Elements;
using UnityEngine;

namespace UI.Interaction
{
    public class SelectionPanel : MonoBehaviour
    {
        [SerializeField] SelectionManager selectionManager;
        [SerializeField] GameObject selectionPanel;
        [SerializeField] GameObject viewParent;
        [SerializeField] Selector selector;

        ISelectable[] selection;

        void OnEnable() => selectionManager.OnSelectionChanged += SelectionChanged;
        void OnDisable() => selectionManager.OnSelectionChanged -= SelectionChanged;

        public void SelectionChanged(IEnumerable<ISelectable> selectables)
        {
            selection = selectables.ToArray();
            if (selection.Length == 0)
            {
                selectionPanel.SetActive(false);
                return;
            }
            selectionPanel.SetActive(true);
            selector.SetOptions(selectables.Select(s => s.ToString()));
        }

        public void UpdateView(int index)
        {
            foreach (RectTransform child in viewParent.transform)
            {
                Destroy(child.gameObject);
            }
            if (index == -1)
                return;
            selection[index].ApplySelectionUi(viewParent.transform);
        }
    }
}
