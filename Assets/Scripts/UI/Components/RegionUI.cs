using System.Collections.Generic;
using Gameplay.Map;
using Gameplay.Units;
using UI.Elements;
using UnityEngine;

namespace UI.Components
{
    public class RegionUI : SelectableUi<RegionController>
    {
        void Start()
        {
            SetContextMenu<RegionController>(new List<NamedAction>
            {
                new(() => $"Move capitol to {Context.name}", () => Debug.Log("Capitol move action invoked")),
            });

            SetContextMenu<UnitController>(new List<NamedAction>
            {
                new(() => $"Reinforce {Context.name}", () => Debug.Log($"Reinforcing unit {Context.name}")),
            });
        }

        // public void FillSelectionPanel(GameObject parent)
        // {
        //     if (selectedUiPrefab == null)
        //     {
        //         Debug.LogWarning("Selected UI prefab is not set.");
        //         return;
        //     }

        //     GameObject selectedUi = Instantiate(selectedUiPrefab, parent.transform);
        //     if (selectedUi.TryGetComponent(out ISelectionUI<RegionController> selectionUi))
        //         selectionUi.SelectedObject = controller;
        //     selectedUi.transform.SetParent(parent.transform, false);
        //     selectedUi.transform.localScale = Vector3.one;
        //     selectedUi.transform.localPosition = Vector3.zero;
        //     selectedUi.SetActive(true);
        // }

        // public void ShowContextMenu(Selectable selectable)
        // {
        //     if (selectable.TryGetComponent(out RegionController region))
        //     {
        //         regionContext = region;
        //         ContextMenu.Instance.Show(Input.mousePosition, regionContextMenu.Select(c => c.Name).ToArray(), index => regionContextMenu[index].Invoke());
        //     }
        //     else if (selectable.TryGetComponent(out UnitController unit))
        //     {
        //         unitContext = unit;
        //         ContextMenu.Instance.Show(Input.mousePosition, unitContextMenu.Select(c => c.Name).ToArray(), index => unitContextMenu[index].Invoke());
        //     }
        // }

        // public void ShowTooltip()
        // {
        //     Tooltip.Instance.DescriptionValue = name;
        //     Tooltip.Instance.Show();
        // }

        // public void HideTooltip()
        // {
        //     Tooltip.Instance.Hide();
        // }
    }
}
