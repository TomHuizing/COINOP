using System.Collections.Generic;
using System.Linq;
using Gameplay.Components;
using Gameplay.Map;
using Gameplay.Selection;
using Gameplay.Units;
using UI.Elements;
using UnityEngine;

namespace UI.Components
{
    [RequireComponent(typeof(MoveController))]
    public class UnitUI : MonoBehaviour
    {
        [SerializeField] private UiController uiController;

        private MoveController moveController;

        private Selectable context;

        private RegionController regionContext;
        private UnitController unitContext;

        private List<NamedAction> regionContextMenu;
        private List<NamedAction> unitContextMenu;

        void Awake()
        {
            moveController = GetComponent<MoveController>();
        }

        void Start()
        {
            regionContextMenu = new()
            {
                new NamedAction("Move", () => { moveController.SetTarget(regionContext); }),
                new NamedAction("Attack", () => Debug.Log("Attack action invoked")),
                new NamedAction("Patrol", () => Debug.Log("Patrol action invoked")),
            };

            unitContextMenu = new()
            {
                new NamedAction("Follow", () => Debug.Log($"Following unit {unitContext.Name}")),
                new NamedAction("Attack", () => Debug.Log("Attack action invoked")),
            };
        }

        public void ShowContextMenu(Selectable selectable)
        {
            context = selectable;
            if(selectable.TryGetComponent(out RegionController region))
            {
                regionContext = region;
                uiController.ShowContextMenu(regionContextMenu.Select(c => c.Name).ToArray(), index => regionContextMenu[index].Invoke());
            }
            else if (selectable.TryGetComponent(out UnitController unit))
            {
                unitContext = unit;
                uiController.ShowContextMenu(unitContextMenu.Select(c => c.Name).ToArray(), index => unitContextMenu[index].Invoke());
            }
        }
    }
}
