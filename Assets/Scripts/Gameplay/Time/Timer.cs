using System;

namespace Gameplay.Time
{
    public class Timer
    {
        private readonly GameClock clock;
        private readonly DateTime trigger;

        public DateTime Trigger => trigger;
        public event Action OnTrigger;

        internal Timer(GameClock clock, DateTime trigger)
        {
            this.clock = clock != null ? clock : throw new ArgumentNullException(nameof(clock));
            this.trigger = trigger;
            clock.OnTick += Tick;
        }

        private void Tick(DateTime time, TimeSpan delta)
        {
            if (time >= trigger)
            {
                clock.OnTick -= Tick;
                OnTrigger?.Invoke();
            }
        }

        public void Cancel() => clock.OnTick -= Tick;
    }
}