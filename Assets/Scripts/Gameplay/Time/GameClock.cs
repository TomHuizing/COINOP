using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Time
{
    [CreateAssetMenu(fileName = "GameClock", menuName = "Singletons/GameClock")]
    public class GameClock : ScriptableObject
    {
        private CancellationTokenSource cts;
        private Task runClockTask;

        [SerializeField] private SerializableDateTime now;
        [SerializeField] private TickPeriod tickPeriod;
        [SerializeField] private TickDuration tickDuration;
        [SerializeField] private CyclePeriod cyclePeriod;

        public bool IsRunning { get; private set; }

        public TimeSpan TickPeriod => TimeSpan.FromMinutes((int)tickPeriod);
        public TimeSpan CyclePeriod => TimeSpan.FromMinutes((int)cyclePeriod);
        public TimeSpan TickDuration => TimeSpan.FromMilliseconds((int)tickDuration);
        public DateTime Now
        {
            get => now.Value;
            set => now.Value = value;
        }

        public event Action<DateTime, TimeSpan> OnTick;
        public event Action<DateTime, TimeSpan> OnCycle;
        public event Action<DateTime> OnDay;

        void OnDestroy() => Stop();

        public void Start()
        {

            cts = new();
            runClockTask = RunClock(cts.Token);

        }

        public void Stop()
        {
            cts.Cancel();
            runClockTask.Wait();
            if (!runClockTask.IsCompletedSuccessfully)
            {
                foreach (Exception ex in runClockTask.Exception.InnerExceptions)
                {
                    throw ex;
                }
            }
        }

        private async Task RunClock(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Awaitable.WaitForSecondsAsync((float)TickDuration.TotalSeconds, token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                Now = Now.AddMinutes((int)tickPeriod);
                OnTick?.Invoke(Now, TickPeriod);
                if (Now.Ticks % CyclePeriod.Ticks == 0)
                    OnCycle?.Invoke(Now, CyclePeriod);
                if (Now.Hour == 0 && Now.Minute == 0 && Now.Second == 0)
                    OnDay?.Invoke(Now);

            }
        }

        public Timer SetTimer(DateTime trigger) => new(this, trigger);
        public Timer SetTimer(TimeSpan period) => SetTimer(Now + period);

        public Decay SetDecay(float startValue, DateTime end, float finalValue = 0) => new(this, startValue, end, finalValue);
        public Decay SetDecay(float startValue, TimeSpan period, float finalValue = 0) => SetDecay(startValue, Now + period, finalValue);
    }
}
