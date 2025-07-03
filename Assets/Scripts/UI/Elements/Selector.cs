using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;

namespace UI.Elements
{
    public class Selector : MonoBehaviour 
    {
        [SerializeField] private Button buttonPrevious;
        [SerializeField] private Button buttonNext;
        // [SerializeField] private Image imageCurrent;
        [SerializeField] private TextMeshProUGUI textCurrent;
        [SerializeField] private bool loop = true;
        [SerializeField] private List<string> options;
        [SerializeField] private UnityEvent<int> onValueChanged = new();
        // [SerializeField] bool hasIndicator = false;
        // [SerializeField] private TextMeshProUGUI indicator;
        // [SerializeField] RectTransform indicatorRect;        
        // [SerializeField] private int startIndex = 0;
        
        // [Serializable]
        // public class OptionItem
        // {
        //     public string optionText = "option"; 
        //     public Sprite optionImage;    

        //     public OptionItem()
        //     {
        //     }

        //     public OptionItem(string newText)
        //     {
        //         optionText = newText;
        //     }

        //     public OptionItem(Sprite newImage)
        //     {
        //         optionImage = newImage;
        //     }
        //     public OptionItem(string newText, Sprite newImage)
        //     {
        //         optionText = newText;
        //         optionImage = newImage;
        //     }                   
        // }


        // [Serializable]
        // public class SelectorSimpleEvent : UnityEvent<int>{ }


        // bool changed = true;
        // int newIndex = 0;
        private int index = 0;          

        public int Index
        {
            get => index;
            set
            {
                if (options.Count == 0)
                {
                    index = -1;
                    ApplyIndex();
                    OnValueChanged?.Invoke(index);
                    return;
                }
                index = value;
                if (index > options.Count - 1)
                {
                    if (loop)
                        index = 0;
                    else
                        index = options.Count - 1;
                }
                if (index < 0)
                {
                    if (loop)
                        index = options.Count - 1;
                    else
                        index = 0;
                }
                ApplyIndex();
                OnValueChanged?.Invoke(index);
            }
        }

        public int Count => options.Count;

        public event Action<int> OnValueChanged;

        // public int StartIndex
        // {
        //     get => startIndex;
        //     set
        //     {
        //         startIndex = value;
        //         SetCurrentOptions(value);
        //     }
        // }

        // public bool HasIndicator
        // {
        //     get => hasIndicator;
        //     set
        //     {
        //         hasIndicator = value;
        //         if (indicator != null && indicator.gameObject.activeSelf != hasIndicator)
        //         {
        //             indicator.gameObject.SetActive(hasIndicator);
        //         }
        //     }
        // }

        // public SelectorSimpleEvent OnValueChanged
        // {
        //     get => onValueChanged;
        //     set
        //     {
        //         onValueChanged = value;
        //     }
        // } 

        void Start()
        {
            OnValueChanged += onValueChanged.Invoke;
            if (buttonPrevious != null)
                buttonPrevious.onClick.AddListener(OnButtonClickPrevious);
            if (buttonNext != null)
                buttonNext.onClick.AddListener(OnButtonClickNext);
            UpdateSelector();
            Index = 0;
        }

        public void OnButtonClickPrevious()
        {
            // UpdateOptions(false);
            // if (changed)
            // {
            //     onValueChanged.Invoke(CurrentIndex);
            // }

            if (Index == 0)
            {
                if (loop)
                    Index = options.Count - 1;
                return;
            }
            Index--;
            
        }

        public void OnButtonClickNext()
        {
            // UpdateOptions(true);                    
            // if(changed)
            // {
            //     onValueChanged.Invoke(CurrentIndex);
            // }    
            if (Index == options.Count - 1)
            {
                if (loop)
                    Index = 0;
                return;
            }
            Index++;             
        }

        public void SetOptions(IEnumerable<string> newOptions)
        {
            options.Clear();
            options.AddRange(newOptions);
            UpdateSelector();
            Index = 0;
        }

        void UpdateSelector()
        {
            if (options.Count > 1)
            {
                buttonNext.gameObject.SetActive(true);
                buttonPrevious.gameObject.SetActive(true);
            }
            else
            {
                buttonNext.gameObject.SetActive(false);
                buttonPrevious.gameObject.SetActive(false);
            }
            if (options.Count == 0)
                textCurrent.text = string.Empty;
        }

        void ApplyIndex()
        {
            if (Index == -1)
                textCurrent.text = string.Empty;
            else
                textCurrent.text = options[Index];
            // textCurrent.text = options[currentIndex].optionText;
            // if (imageCurrent != null)
            // {
            //     if (options[currentIndex].optionImage != null)
            //     {
            //         imageCurrent.gameObject.SetActive(true);
            //         imageCurrent.sprite = options[currentIndex].optionImage;
            //     }
            //     else
            //     {
            //         imageCurrent.gameObject.SetActive(false);
            //         imageCurrent.sprite = null;
            //     }
            // }
            // if (hasIndicator && (indicator != null))
            // {
            //     indicator.text = (currentIndex + 1) + " / " + options.Count;
            // }
        }  

        // void UpdateOptions(bool bNext)
        // {
        //     changed = true;
        //     if( bNext )
        //     {
        //         if(currentIndex == options.Count - 1)
        //         {
        //             if(loop)
        //             {
        //                 newIndex = 0;
        //             }
        //             else
        //             {
        //                 changed = false;
        //             }                    
        //         }
        //         else
        //         {                  
        //             newIndex = currentIndex + 1;                    
        //         }             
        //     }
        //     else
        //     {
        //         if(currentIndex == 0)
        //         {
        //             if(loop)
        //             {
        //                 newIndex = options.Count -1;
        //             }
        //             else
        //             {
        //                 changed = false;
        //             }                    
        //         }
        //         else
        //         {                 
        //             newIndex = currentIndex - 1;                    
        //         }                 
        //     } 
        //     if(changed)
        //     {     
        //         currentIndex = newIndex;               
        //         SetOptions();           
        //         if(hasIndicator &&(indicator != null))
        //         {
        //             indicator.text = (newIndex+1) +" / "+ options.Count;
        //         }                
        //     }          
        // }
        
        // #if UNITY_EDITOR
        // protected void OnValidate()
        // {
        //     if (indicatorRect != null)
        //     {
        //         indicatorRect.gameObject.SetActive(hasIndicator);
        //     }
        // }
        // #endif
    }
}