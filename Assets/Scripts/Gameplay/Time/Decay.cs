using System;
using UnityEngine;

namespace Gameplay.Time
{
    public class Decay
    {
        private readonly Lerp lerp;
        private readonly float startValue;
        private readonly float finalValue;

        public float Value => Mathf.Lerp(startValue, finalValue, lerp.Value);
        public TimeSpan TimeLeft => lerp.TimeLeft;
        public event Action OnEnd;

        public Decay(float startValue, TimeSpan period, float finalValue = 0) : this(startValue, GameClock.instance.Now + period, finalValue)
        {
            if (GameClock.instance == null)
                throw new ArgumentNullException(nameof(GameClock.instance), "GameClock instance must be initialized before creating a Decay.");
        }
        public Decay(float startValue, DateTime end, float finalValue = 0)
        {
            this.startValue = startValue;
            this.finalValue = finalValue;
            lerp = new Lerp(end);
            lerp.OnEnd += () => OnEnd?.Invoke();
        }

        public void Cancel() => lerp.Cancel();
    }
}