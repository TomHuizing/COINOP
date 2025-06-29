using Gameplay.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Components
{
    public class LinkController : MonoBehaviour, IPointerMoveHandler
    {
        [SerializeField] TextMeshProUGUI text;

        public void OnPointerMove(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if (linkIndex < 0)
                return;
            string linkId = text.textInfo.linkInfo[linkIndex].GetLinkID();
            Debug.Log(linkId);
            if(IController.Lookup.TryGetValue(linkId, out var controller))
            {
                Debug.Log(controller.Name);
            }
        }
    }
}
