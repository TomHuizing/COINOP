using System;
using UnityEngine;

namespace Gameplay.Time
{
    public class Decay
    {
        private readonly float startValue;
        private readonly float finalValue;
        private readonly DateTime end;
        private readonly GameClock clock;

        public float Value => Mathf.Lerp(startValue, finalValue, (float)(clock.Now - end).TotalMinutes / (float)(end - clock.Now).TotalMinutes);
        public TimeSpan TimeLeft => end - clock.Now;
        public event Action OnEnd;

        internal Decay(GameClock clock, float startValue, DateTime end, float finalValue)
        {
            this.clock = clock != null ? clock : throw new ArgumentNullException(nameof(clock));
            this.startValue = startValue;
            this.finalValue = finalValue;
            this.end = end;
            clock.OnTick += Tick;
        }

        private void Tick(DateTime time, TimeSpan delta)
        {
            if (time >= end)
            {
                clock.OnTick -= Tick;
                OnEnd?.Invoke();
            }
        }

        public void Cancel() => clock.OnTick -= Tick;
    }
}