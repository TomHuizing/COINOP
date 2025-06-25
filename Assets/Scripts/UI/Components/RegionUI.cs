using System.Collections.Generic;
using Gameplay.Map;
using Gameplay.Units;
using UI.Elements;
using UnityEditor;
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

            SetTooltipText($"<B>{Controller.name}</B>\n\n{Controller.Stats}");
        }
    }
}
