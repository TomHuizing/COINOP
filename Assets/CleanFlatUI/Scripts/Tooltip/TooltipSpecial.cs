using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
namespace RainbowArt.CleanFlatUI
{
    [ExecuteAlways]
    public class TooltipSpecial : MonoBehaviour
    {
        public enum Anchor
        {
            TopLeft,
            TopMid,
            TopRight,
            MidRight,
            BottomRight,
            BottomMid,
            BottomLeft,
            MidLeft
        }           

        [SerializeField] TextMeshProUGUI description;
        [SerializeField] Animator animator; 
        [SerializeField] RectTransform rectTransform;
        [SerializeField] Anchor origin = Anchor.BottomLeft;
        [SerializeField] Vector2 offset = new(10, 10);

         
        public string DescriptionValue
        {
            get
            {
                if (description != null)
                {
                    return description.text;
                }
                return "";
            }
            set
            {
                if (description != null)
                {
                    description.text = value;
                    UpdateHeight();
                }
            }
        }

        // public void InitTooltip(Vector2 mousePosition, RectTransform areaScope)
        // { 
        //     UpdateHeight();                 
        //     UpdatePosition(mousePosition,areaScope);
        // }

        // void OnEnable()
        // {
        //     rectTransform = GetComponent<RectTransform>();
        //     if (rectTransform == null)
        //         throw new ArgumentNullException("rectTransform");
        // }

        public void ShowTooltip()
        {
            gameObject.SetActive(true);
            if (animator != null)
            {
                animator.enabled = false;
                animator.gameObject.transform.localScale = Vector3.one;
                animator.gameObject.transform.localEulerAngles = Vector3.zero;
            }
            // PlayAnimation();
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);    
        }   

        // void Update()
        // {
        //     if(bDelayedUpdate)
        //     {
        //         bDelayedUpdate = false;
        //         UpdateHeight();
        //     }
        // }
        [UnityEngine.ContextMenu("Update Height")]
        public void UpdateHeight()
        {
            if(description != null)
            {
                RectTransform selfRect = GetComponent<RectTransform>();
                // float finalHeight = description.preferredHeight;
                selfRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, description.preferredHeight);
            }
        }

        public void UpdatePosition(Vector2 position, RectTransform bounds)
        {
            rectTransform.pivot = origin switch
            {
                Anchor.TopLeft => new(0,1),
                Anchor.TopMid => new(0.5f, 1),
                Anchor.TopRight => new(1, 1),
                Anchor.MidRight => new(1, 0.5f),
                Anchor.BottomRight => new(1, 0),
                Anchor.BottomMid => new(0.5f, 0),
                Anchor.BottomLeft => new(0, 0),
                Anchor.MidLeft => new(0, 0.5f),
                _ => Vector2.zero
            };
            Vector2 actualOffset = offset * ((rectTransform.pivot - new Vector2(0.5f, 0.5f)) * -2);
            rectTransform.position = position + actualOffset;
        }

        // public void UpdatePosition(Vector2 mousePosition, RectTransform areaScope)
        // {
        //     RectTransform selfRect = GetComponent<RectTransform>();
        //     selfRect.localPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
        //     Vector3[] corners = new Vector3[4];
        //     selfRect.GetWorldCorners(corners);
        //     Vector3[] cornersInArea = new Vector3[4];
        //     float correctionX = 0;
        //     float correctionY = 0;
        //     for (int i = 0; i < 4; i++)
        //     {
        //         cornersInArea[i] = areaScope.InverseTransformPoint(corners[i]);
        //     }
        //     if (cornersInArea[2].x >= areaScope.rect.xMax)
        //     {
        //         if (cornersInArea[0].x - selfRect.rect.width / 2 < areaScope.rect.xMin)
        //         {
        //             correctionX = cornersInArea[0].x - selfRect.rect.width / 2 - areaScope.rect.xMin;
        //         }
        //         if (cornersInArea[2].y >= areaScope.rect.yMax)
        //         {
        //             origin = Origin.LeftBottom;
        //             if (cornersInArea[0].y - selfRect.rect.height < areaScope.rect.yMin)
        //             {
        //                 correctionY = cornersInArea[0].y - selfRect.rect.height - areaScope.rect.yMin;
        //             }
        //         }
        //         else
        //         {
        //             origin = Origin.LeftTop;
        //         }
        //     }
        //     else if (cornersInArea[0].x <= areaScope.rect.xMin)
        //     {
        //         if (cornersInArea[2].x + selfRect.rect.width / 2 > areaScope.rect.xMax)
        //         {
        //             correctionX = cornersInArea[2].x + selfRect.rect.width / 2 - areaScope.rect.xMax;
        //         }
        //         if (cornersInArea[2].y >= areaScope.rect.yMax)
        //         {
        //             origin = Origin.RightBottom;
        //             if (cornersInArea[0].y - selfRect.rect.height < areaScope.rect.yMin)
        //             {
        //                 correctionY = cornersInArea[0].y - selfRect.rect.height - areaScope.rect.yMin;
        //             }
        //         }
        //         else
        //         {
        //             origin = Origin.RightTop;
        //         }
        //     }
        //     else
        //     {
        //         if (cornersInArea[2].y >= areaScope.rect.yMax)
        //         {
        //             if (cornersInArea[0].y - selfRect.rect.height < areaScope.rect.yMin)
        //             {
        //                 correctionY = cornersInArea[0].y - selfRect.rect.height - areaScope.rect.yMin;
        //             }
        //             origin = Origin.Bottom;
        //         }
        //         else
        //         {
        //             origin = Origin.Top;
        //         }
        //     }

        //     Vector3 pos = selfRect.localPosition;
        //     float selfWidth = selfRect.rect.width;
        //     float selfHeight = selfRect.rect.height;
        //     switch (origin)
        //     {
        //         case Origin.Top:
        //         {
        //             break;
        //         }
        //         case Origin.RightTop:
        //         {
        //             pos.x = pos.x + selfWidth / 2 - correctionX;
        //             break;
        //         }
        //         case Origin.LeftTop:
        //         {
        //             pos.x = pos.x - selfWidth / 2 - correctionX;
        //             break;
        //         }
        //         case Origin.Bottom:
        //         {
        //             pos.y = pos.y - selfHeight - correctionY;
        //             break;
        //         }
        //         case Origin.RightBottom:
        //         {
        //             pos.x = pos.x + selfWidth / 2 - correctionX;
        //             pos.y = pos.y - selfHeight - correctionY;
        //             break;
        //         }
        //         case Origin.LeftBottom:
        //         {
        //             pos.x = pos.x - selfWidth / 2 - correctionX;
        //             pos.y = pos.y - selfHeight - correctionY;
        //             break;
        //         }
        //     }
        //     selfRect.localPosition = pos;
        // }

        // void PlayAnimation()
        // {
        //     if(animator != null)
        //     {
        //         if(animator.enabled == false)
        //         {
        //             animator.enabled = true;
        //         }
        //         animator.Play("Transition",0,0);  
        //     }            
        // }
    }
}