using System;
using UnityEngine;

namespace Gameplay.Time
{
    [Serializable]
    internal struct SerializableDateTime
    {
        [SerializeField] private long ticks;

        public DateTime Value
        {
            get => new(ticks);
            set => ticks = value.Ticks;
        }

        public SerializableDateTime(DateTime dateTime) => ticks = dateTime.Ticks;

        public override string ToString() => Value.ToString();
    }
}