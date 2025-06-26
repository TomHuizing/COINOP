using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Time
{
    [CreateAssetMenu(fileName = "GameClock", menuName = "Singletons/GameClock")]
    public class GameClock : ScriptableSingleton<GameClock>
    {
        private CancellationTokenSource cts;
        private Task runClockTask;

        [SerializeField] private SerializableDateTime now;
        [SerializeField] private TickPeriod tickPeriod;
        [SerializeField] private TickDuration tickDuration;
        [SerializeField] private CyclePeriod cyclePeriod;

        public bool IsRunning { get; private set; } = false;

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
            IsRunning = true;

        }

        public void Stop()
        {
            cts.Cancel();
            runClockTask.Wait();
            IsRunning = false;
            if (!runClockTask.IsCompletedSuccessfully)
            {
                foreach (Exception ex in runClockTask.Exception.InnerExceptions)
                {
                    throw ex;
                }
            }
        }

        public void Toggle()
        {
            if (IsRunning)
                Stop();
            else
                Start();
        }

        public void SpeedUp()
        {
        }

        public void SlowDown()
        {
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
    }
}
