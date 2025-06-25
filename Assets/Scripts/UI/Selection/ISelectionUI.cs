using System;
using UnityEngine;

namespace Gameplay.Selection
{
    public interface ISelectionUI<T> where T : class
    {
        public T SelectedObject { get; set; }
    }
}