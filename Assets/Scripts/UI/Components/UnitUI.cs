using System.Collections.Generic;
using Gameplay.Components;
using Gameplay.Map;
using Gameplay.Units;
using UI.Elements;
using UnityEngine;

namespace UI.Components
{
    [RequireComponent(typeof(MoveController))]
    public class UnitUI : SelectableUi<UnitController>
    {
        private MoveController moveController;

        void Start()
        {
            moveController = GetComponent<MoveController>();

            SetContextMenu<RegionController>(new List<NamedAction>
            {
                new(() => $"Move to {Context.name}", () => { moveController.SetTarget(Context.GetComponent<RegionController>()); }),
                new(() => $"Attack {Context.name}", () => Debug.Log("Attack action invoked")),
                new(() => $"Patrol {Context.name}", () => Debug.Log("Patrol action invoked")),
            });

            SetContextMenu<UnitController>(new List<NamedAction>
            {
                new(() => $"Follow {Context.name}", () => Debug.Log($"Following unit {Context.name}")),
                new(() => $"Attack {Context.name}", () => Debug.Log("Attack action invoked")),
            });
        }
    }
}
