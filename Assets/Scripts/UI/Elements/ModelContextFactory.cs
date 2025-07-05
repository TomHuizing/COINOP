using System;
using UnityEngine;

namespace UI.Elements
{
    public class ModelContextFactory : MonoBehaviour
    {
        [SerializeField] Canvas parent;
        [SerializeField] ModalContext modalContextPrefab;

        public ModalContext Create() => Instantiate(modalContextPrefab, parent.transform);
    }
}
