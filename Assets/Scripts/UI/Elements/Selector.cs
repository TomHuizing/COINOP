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
        }  
    }
}