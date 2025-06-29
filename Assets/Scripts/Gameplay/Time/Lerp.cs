using System;
using UnityEngine;

namespace Gameplay.Time
{
    public class Lerp
    {
        private readonly DateTime end;
        private readonly DateTime start;

        public float Value => 1 - (float)(TimeLeft / Duration);
        public TimeSpan TimeLeft => end - GameClock.instance.Now;
        public TimeSpan Duration => end - start;
        public DateTime Start => start;
        public DateTime End => end;
        public event Action OnEnd;

        public Lerp(DateTime end)
        {
            if (GameClock.instance == null)
                throw new ArgumentNullException(nameof(GameClock.instance), "GameClock instance must be initialized before creating a Lerp.");
            if(end <= GameClock.instance.Now)
                throw new ArgumentException("End time must be in the future.", nameof(end));
            start = GameClock.instance.Now;
            this.end = end;
            GameClock.instance.OnTick += Tick;
        }

        public Lerp(TimeSpan period) : this(GameClock.instance.Now + period) { }

        private void Tick(DateTime time, TimeSpan delta)
        {
            if (time >= end)
            {
                GameClock.instance.OnTick -= Tick;
                OnEnd?.Invoke();
            }
        }

        public void Cancel() => GameClock.instance.OnTick -= Tick;
    }
}
