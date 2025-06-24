using System.Collections.Generic;
using System.Linq;
using Gameplay.Selection;
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
        GameObject view;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        // void Start()
        // {
        //     selector.OnValueChanged += UpdateView;
        // }

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

        // public void NextSelection()
        // {
        //     selectionIndex++;
        //     if (selectionIndex >= selection.Length)
        //         selectionIndex = 0;
        //     UpdateView(selection[selectionIndex]);

        // }

        // public void PrevSelection()
        // {
        //     selectionIndex--;
        //     if (selectionIndex < 0)
        //         selectionIndex = selection.Length - 1;
        //     UpdateView(selection[selectionIndex]);
        // }

        public void UpdateView(int index)
        {
            print($"UpdateView {index}");
            if (view != null)
                Destroy(view);
            if (index == -1)
            {
                print("No selection, hiding view");
                return;
            }
            view = selection[index].GetSelectedUi();
            view.transform.SetParent(viewParent.transform, false);
            RectTransform rect = view.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
            // view.GetComponent<RectTransform>().
            view.SetActive(true);
        }
    }
}
