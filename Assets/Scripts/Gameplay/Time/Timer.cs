using System;

namespace Gameplay.Time
{
    public class Timer
    {
        private readonly DateTime trigger;

        public DateTime Trigger => trigger;
        public event Action OnTrigger;

        public Timer(TimeSpan period) : this(GameClock.instance.Now + period)
        {
            if (GameClock.instance == null)
                throw new ArgumentNullException(nameof(GameClock.instance), "GameClock instance must be initialized before creating a Timer.");
        }

        public Timer(DateTime trigger)
        {
            if (GameClock.instance == null)
                throw new ArgumentNullException(nameof(GameClock.instance), "GameClock instance must be initialized before creating a Lerp.");
            if (trigger <= GameClock.instance.Now)
                throw new ArgumentException("Trigger must be in the future.", nameof(trigger));
            this.trigger = trigger;
            GameClock.instance.OnTick += Tick;
        }

        private void Tick(DateTime time, TimeSpan delta)
        {
            if (time >= trigger)
            {
                GameClock.instance.OnTick -= Tick;
                OnTrigger?.Invoke();
            }
        }

        public void Cancel() => GameClock.instance.OnTick -= Tick;
    }
}