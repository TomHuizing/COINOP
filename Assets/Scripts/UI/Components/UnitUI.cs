using System.Collections.Generic;
using System.Linq;
using UI.Elements;
using UnityEngine;

namespace UI.Components
{
    public class UnitUI : MonoBehaviour
    {
        [SerializeField] private UiController uiController;
        [SerializeField] private List<NamedAction> contextMenuItems;

        public void ShowContextMenu()
        {
            uiController.ShowContextMenu(contextMenuItems.Select(c => c.Text).ToArray(), index => contextMenuItems[index].Invoke());
        }
    }
}
