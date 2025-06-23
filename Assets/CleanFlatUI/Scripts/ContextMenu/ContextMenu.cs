using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace RainbowArt.CleanFlatUI
{
    [ExecuteAlways]
    public class ContextMenu : MonoBehaviour, IPointerDownHandler
    {
        // protected internal class ContextMenuItem : MonoBehaviour, IPointerEnterHandler
        // {
        //     public TextMeshProUGUI itemText;
        //     public Image itemImage;
        //     public Image itemLine;
        //     public Button button;
        //     public virtual void OnPointerEnter(PointerEventData eventData)
        //     {
        //         EventSystem.current.SetSelectedGameObject(gameObject);
        //     }
        // }

        // [Serializable]
        // public class OptionItem
        // {
        //     public string text;
        //     public Sprite icon;

        //     public OptionItem()
        //     {
        //     }

        //     public OptionItem(string newText)
        //     {
        //         text = newText;
        //     }

        //     public OptionItem(Sprite newImage)
        //     {
        //         icon = newImage;
        //     }
        //     public OptionItem(string newText, Sprite newImage)
        //     {
        //         text = newText;
        //         icon = newImage;
        //     }
        // }

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

        // [SerializeField] private RectTransform canvas;
        [SerializeField] private GameObject clickBlocker;
        [SerializeField] private ContextMenuItem contextMenuItemPrefab;
        // [SerializeField] private TextMeshProUGUI itemText;
        // [SerializeField] private Image itemImage;
        // [SerializeField] private Image itemLine;
        [SerializeField] private Animator animator;
        // [SerializeField] private RectOffset padding = new();
        // [SerializeField] private float spacing = 2;
        // [SerializeField] private List<OptionItem> options = new();
        [SerializeField] Anchor origin = Anchor.TopLeft;
        [SerializeField] Vector2 offset = new(10, 10);
        
        // Origin origin = Origin.RightBottom;

        private readonly List<ContextMenuItem> menuItems = new();
        // private GameObject clickerBlocker;
        // private IEnumerator diableCoroutine;
        // private float disableTime = 0.15f;
        // private float distanceX = 2.0f;
        // private float distanceY = 2.0f;
        private RectTransform rectTransform;



        // public RectOffset Padding
        // {
        //     get => padding;
        //     set
        //     {
        //         padding = value;
        //     }
        // }

        // public float Spacing
        // {
        //     get => spacing;
        //     set
        //     {
        //         spacing = value;
        //     }
        // }

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        void Update()
        {
            
        }

        public void Show(Vector2 position, string[] items, Action<int> callback)
        {
            // if(options.Count > 0)
            // {
            //     gameObject.SetActive(true);
            //     DestroyAllMenuItems();
            //     DestroyClickBlocker();
            //     SetupTemplate();
            //     CreateAllMenuItems(options);
            //     UpdatePosition(mousePosition,areaScope);
            //     CreateClickBlocker();
            //     PlayAnimation(true);
            // }
            clickBlocker.SetActive(true);
            CreateMenuItems(items, callback);

            rectTransform.pivot = origin switch
            {
                Anchor.TopLeft => new(0, 1),
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

        private void CreateMenuItems(string[] items, Action<int> callback)
        {
            for (int i = 0; i < items.Length; i++)
            {
                int index = i;
                ContextMenuItem item = Instantiate(contextMenuItemPrefab);
                item.Init(items[i]);
                item.OnClick += () => print(index);
                item.OnClick += () => callback?.Invoke(index);
                item.OnClick += () => Hide();
                item.transform.SetParent(transform);
                menuItems.Add(item);
            }
        }

        // public void AddOptions(List<OptionItem> optionList)
        // {
        //     options.AddRange(optionList);
        // }

        // public void AddOptions(List<string> optionList)
        // {
        //     for (int i = 0; i < optionList.Count; i++)
        //     {
        //         options.Add(new OptionItem(optionList[i]));
        //     }                
        // }

        // public void AddOptions(List<Sprite> optionList)
        // {
        //     for (int i = 0; i < optionList.Count; i++)
        //     {
        //         options.Add(new OptionItem(optionList[i]));
        //     }                
        // }

        // public void ClearOptions()
        // {
        //     options.Clear();
        // }

        // void SetupTemplate()
        // {
        //     // ContextMenuItem menuItem = contextMenuItemPrefab.GetComponent<ContextMenuItem>();
        //     // if (menuItem == null)
        //     // {
        //     //     menuItem = contextMenuItemPrefab.AddComponent<ContextMenuItem>();
        //     //     menuItem.itemText = itemText;
        //     //     menuItem.itemImage = itemImage;   
        //     //     menuItem.itemLine = itemLine;             
        //     //     menuItem.button = contextMenuItemPrefab.GetComponent<Button>();
        //     // }
        //     // contextMenuItemPrefab.SetActive(false);
        // }

        // void CreateAllMenuItems(List<OptionItem> options)
        // {
        //     // float itemWidth = contextMenuItemPrefab.GetComponent<RectTransform>().rect.width;
        //     // RectTransform templateParentTransform = contextMenuItemPrefab.transform.parent as RectTransform;
        //     // int dataCount = options.Count;
        //     // float curY = -padding.top;
        //     // for (int i = 0; i < dataCount; ++i)
        //     // {
        //     //     OptionItem itemData = options[i];
        //     //     int index = i;
        //     //     GameObject go = Instantiate(contextMenuItemPrefab) as GameObject;
        //     //     go.transform.SetParent(contextMenuItemPrefab.gameObject.transform.parent, false);
        //     //     go.transform.localPosition = Vector3.zero;
        //     //     go.transform.localEulerAngles = Vector3.zero;
        //     //     go.SetActive(true);
        //     //     go.name = "MenuItem" + i;
        //     //     ContextMenuItem curMenuItem = go.GetComponent<ContextMenuItem>();
        //     //     menuItems.Add(curMenuItem);
        //     //     curMenuItem.itemText.text = itemData.text;
        //     //     if(itemData.icon == null)
        //     //     {
        //     //         curMenuItem.itemImage.gameObject.SetActive(false);
        //     //         curMenuItem.itemImage.sprite = null;
        //     //     }
        //     //     else
        //     //     {
        //     //         curMenuItem.itemImage.gameObject.SetActive(true);
        //     //         curMenuItem.itemImage.sprite = itemData.icon;
        //     //     }   
        //     //     if(curMenuItem.itemLine != null)
        //     //     {
        //     //         if(i == (dataCount-1))
        //     //         {
        //     //             curMenuItem.itemLine.gameObject.SetActive(false);
        //     //         }
        //     //         else
        //     //         {
        //     //             curMenuItem.itemLine.gameObject.SetActive(true);
        //     //         }
        //     //     }
        //     //     curMenuItem.button.onClick.RemoveAllListeners();
        //     //     curMenuItem.button.onClick.AddListener(delegate { OnItemClicked(index); });
                
        //     //     RectTransform curRectTransform = go.GetComponent<RectTransform>();
        //     //     curRectTransform.anchoredPosition3D = new Vector3(padding.left, curY, 0);
        //     //     float curItemHeight = curRectTransform.rect.height;
        //     //     curY = curY - curItemHeight;
        //     //     if (i < (dataCount - 1))
        //     //     {
        //     //         curY = curY - spacing;
        //     //     }                
        //     // }
            
        //     // gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Abs(curY) + padding.bottom);
        //     // gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, itemWidth + padding.left+padding.right);
        // }

        // Canvas GetRootCanvas()
        // {
        //     List<Canvas> list = new List<Canvas>();
        //     gameObject.GetComponentsInParent(false, list);
        //     if (list.Count == 0)
        //     {
        //         return null;
        //     }
        //     var listCount = list.Count;
        //     Canvas rootCanvas = list[listCount - 1];
        //     for (int i = 0; i < listCount; i++)
        //     {
        //         if (list[i].isRootCanvas || list[i].overrideSorting)
        //         {
        //             rootCanvas = list[i];
        //             break;
        //         }
        //     }
        //     return rootCanvas;
        // }

        // RectTransform GetRootCanvasRectTrans()
        // {
        //     Canvas rootCanvas = GetRootCanvas();
        //     if (rootCanvas == null)
        //     {
        //         return null;
        //     }
        //     return rootCanvas.GetComponent<RectTransform>();
        // }

        // void CreateClickBlocker()
        // {
        //     //Canvas rootCanvas = GetRootCanvas();
        //     if(canvas == null)
        //     {
        //         return;
        //     }
        //     clickerBlocker = new GameObject("ClickBlocker");
        //     RectTransform blockerRect = clickerBlocker.AddComponent<RectTransform>();
        //     blockerRect.anchorMin = new Vector2(0.5f, 0.5f);
        //     blockerRect.anchorMax = new Vector2(0.5f, 0.5f);
        //     blockerRect.pivot = new Vector2(0.5f, 0.5f);
        //     Image blockerImage = clickerBlocker.AddComponent<Image>();
        //     blockerImage.color = Color.clear;
        //     // RectTransform rootCanvasRect = canvas.GetComponent<RectTransform>();
        //     // float rootCanvasWidth = rootCanvasRect.rect.width;
        //     // float rootCanvasHeight = rootCanvasRect.rect.height;
        //     blockerRect.SetParent(canvas.transform, false);
        //     blockerRect.localPosition = Vector3.zero;
        //     blockerRect.SetParent(gameObject.transform, true);
        //     blockerRect.SetAsFirstSibling();
        //     blockerRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, canvas.rect.height);
        //     blockerRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, canvas.rect.width);
        // }

        public void OnPointerDown(PointerEventData eventData)
        {
            Hide();
        }

        // void OnItemClicked(int index)
        // {
        //     onValueChanged.Invoke(index);
        //     Hide(true);
        // }

        // void DestroyAllMenuItems()
        // {
        //     var itemsCount = menuItems.Count;
        //     for (int i = 0; i < itemsCount; i++)
        //     {
        //         if (menuItems[i] != null)
        //         {
        //             Destroy(menuItems[i].gameObject);
        //         }
        //     }
        //     menuItems.Clear();
        // }

        // void DestroyClickBlocker()
        // {
        //     if(clickerBlocker != null)
        //     {
        //         Destroy(clickerBlocker);
        //         clickerBlocker = null;
        //     }
        // }

        public void Hide(/*bool playAnim*/)
        {
            // if (diableCoroutine != null)
            // {
            //     StopCoroutine(diableCoroutine);
            //     diableCoroutine = null;
            // }
            // if(playAnim)
            // {
            //     PlayAnimation(false);
            //     diableCoroutine = DisableTransition();
            //     StartCoroutine(diableCoroutine);
            // }
            // else
            // {
            //     if (animator != null)
            //     {
            //         animator.enabled = false;
            //         animator.gameObject.transform.localScale = Vector3.one;
            //         animator.gameObject.transform.localEulerAngles = Vector3.zero;
            //     }
            //     // DestroyAllMenuItems();
            //     // DestroyClickBlocker();
            // }
            foreach (var g in menuItems)
            {
                Destroy(g.gameObject);
            }
            clickBlocker.SetActive(false);
            gameObject.SetActive(false);
        }   

        // IEnumerator DisableTransition()
        // {
        //     yield return new WaitForSeconds(disableTime);
        //     gameObject.SetActive(false);
        //     DestroyAllMenuItems();       
        // }    

        public bool IsShowing()
        {
            return gameObject.activeSelf;
        }

        void UpdatePosition(Vector2 mousePosition, RectTransform areaScope)
        {
            // if (areaScope == null)
            // {
            //     areaScope = GetRootCanvasRectTrans();
            //     if (areaScope == null)
            //     {
            //         return;
            //     }
            // }
            // RectTransform selfRect = GetComponent<RectTransform>();      
            // selfRect.localPosition = new Vector3(mousePosition.x,mousePosition.y,0);                                
            // Vector3[] corners = new Vector3[4];
            // selfRect.GetWorldCorners(corners);
            // Vector3[] cornersInArea = new Vector3[4];
            // float correctionX = 0;
            // float correctionY = 0;
            // for(int i = 0; i < 4; i++)
            // {
            //     cornersInArea[i] = areaScope.InverseTransformPoint(corners[i]); 
            // } 
            // if(cornersInArea[2].x >= areaScope.rect.xMax)
            // {
            //     if(cornersInArea[0].x - selfRect.rect.width < areaScope.rect.xMin)
            //     {
            //         correctionX = cornersInArea[0].x - selfRect.rect.width - areaScope.rect.xMin;
            //     }
            //     if(cornersInArea[0].y < areaScope.rect.yMin)
            //     {
            //         origin = Origin.LeftTop;
            //         if(cornersInArea[2].y + selfRect.rect.height > areaScope.rect.yMax)
            //         {
            //             correctionY = cornersInArea[2].y + selfRect.rect.height - areaScope.rect.yMax;
            //         }   
            //     }
            //     else
            //     {
            //         origin = Origin.LeftBottom;
            //     }
            // }            
            // else if(cornersInArea[0].y < areaScope.rect.yMin)
            // {
            //     origin = Origin.RightTop;  
            //     if(cornersInArea[2].y + selfRect.rect.height > areaScope.rect.yMax)
            //     {
            //         correctionY = cornersInArea[2].y + selfRect.rect.height - areaScope.rect.yMax;
            //     }                                        
            // }   
            // else
            // {
            //     origin = Origin.RightBottom;     
            // }   
            
            // Vector3 pos = selfRect.localPosition;
            // float selfWidth = selfRect.rect.width;
            // float selfHeight = selfRect.rect.height;
            // switch (origin)
            // {
            //     case Origin.RightBottom:
            //     {
            //         pos.x = pos.x + distanceX;
            //         pos.y = pos.y - distanceY;
            //         break;
            //     }
            //     case Origin.RightTop:
            //     {
            //         pos.x = pos.x + distanceX;
            //         if(correctionY == 0)
            //         {
            //             pos.y = pos.y + selfHeight + distanceY; 
            //         }
            //         else
            //         {                        
            //             pos.y = pos.y + selfHeight - correctionY;
            //         }
            //         break;
            //     }
            //     case Origin.LeftTop:
            //     {
            //         if(correctionX == 0)
            //         {
            //             pos.x = pos.x - selfWidth - distanceX;
            //         }
            //         else
            //         {
            //             pos.x = pos.x - selfWidth - correctionX;
            //         }  
            //         if(correctionY == 0)
            //         {
            //             pos.y = pos.y + selfHeight + distanceY; 
            //         }
            //         else
            //         {                        
            //             pos.y = pos.y + selfHeight - correctionY;
            //         }                    
            //         break;
            //     }               
            //     case Origin.LeftBottom:
            //     {
            //         if(correctionX == 0)
            //         {
            //             pos.x = pos.x - selfWidth - distanceX;
            //         }
            //         else
            //         {
            //             pos.x = pos.x - selfWidth - correctionX;
            //         }  
            //         pos.y = pos.y - distanceY;                     
            //         break;
            //     } 
            // }  
            // selfRect.localPosition = pos;
        }

        // void PlayAnimation(bool bShow)
        // {
        //     if (animator != null)
        //     {
        //         animator.enabled = false;
        //         animator.gameObject.transform.localScale = Vector3.one;
        //         animator.gameObject.transform.localEulerAngles = Vector3.zero;
        //     }
        //     if (animator != null)
        //     {
        //         if(animator.enabled == false)
        //         {
        //             animator.enabled = true;
        //         }
        //         string animationStr = null; 
        //         if(bShow)
        //         {      
        //             animationStr =  "In Right Bottom";               
        //             switch (origin)
        //             {
        //                 case Origin.RightBottom:
        //                 {                            
        //                     break;
        //                 }
        //                 case Origin.RightTop:
        //                 {
        //                     animationStr = "In Right Top"; 
        //                     break;
        //                 }
        //                 case Origin.LeftTop:
        //                 {
        //                     animationStr = "In Left Top"; 
        //                     break;
        //                 }               
        //                 case Origin.LeftBottom:
        //                 {
        //                     animationStr = "In Left Bottom"; 
        //                     break;
        //                 } 
        //             }                       
        //         }
        //         else
        //         {
        //             animationStr = "Out Right Bottom"; 
        //             switch (origin)
        //             {
        //                 case Origin.RightBottom:
        //                 {                            
        //                     break;
        //                 }
        //                 case Origin.RightTop:
        //                 {
        //                     animationStr = "Out Right Top"; 
        //                     break;
        //                 }
        //                 case Origin.LeftTop:
        //                 {
        //                     animationStr = "Out Left Top"; 
        //                     break;
        //                 }               
        //                 case Origin.LeftBottom:
        //                 {
        //                     animationStr = "Out Left Bottom"; 
        //                     break;
        //                 } 
        //             }
        //         }
        //         animator.Play(animationStr,0,0);
        //     }
        // }   
    }
}