using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Time
{
    [CreateAssetMenu(fileName = "GameClock", menuName = "Singletons/GameClock")]
    public class GameClock : ScriptableObject
    {
        private class Timer
        {
            public readonly DateTime Trigger;
            public readonly Action Callback;

            public Timer(DateTime trigger, Action callback)
            {
                Trigger = trigger;
                Callback = callback;
            }
        }

        private class Lerp
        {
            public readonly DateTime Start;
            public readonly DateTime End;
            public readonly TimeSpan Span;
            public readonly Action<float> Callback;

            public Lerp(DateTime start, DateTime end, Action<float> callback)
            {
                Start = start;
                End = end;
                Span = End - Start;
                Callback = callback;
            }
        }

        private CancellationTokenSource cts;
        private Task runClockTask;

        private readonly List<Timer> timers = new();
        private readonly List<Lerp> lerps = new();

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

        public event Action<TimeSpan> OnTick;
        public event Action<TimeSpan> OnCycle;
        public event Action OnDay;

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
                CheckTimers();
                CheckLerps();
                OnTick?.Invoke(TickPeriod);
                if (Now.Ticks % CyclePeriod.Ticks == 0)
                    OnCycle?.Invoke(CyclePeriod);
                if (Now.Hour == 0 && Now.Minute == 0 && Now.Second == 0)
                    OnDay?.Invoke();

            }
        }

        private void CheckTimers()
        {
            foreach (Timer t in timers.ToList())
            {
                if (t.Trigger >= Now)
                {
                    t.Callback?.Invoke();
                    CancelTimer(t);
                }
            }
        }

        private void CheckLerps()
        {
            foreach (Lerp l in lerps.ToList())
            {
                if (Now >= l.End)
                {
                    l.Callback?.Invoke(1);
                    CancelLerp(l);
                }
                else if (Now >= l.Start)
                {
                    TimeSpan spent = Now - l.Start;
                    l.Callback?.Invoke((float)(spent / l.Span));
                }
            }
        }

        public void SetTimer(DateTime trigger, Action callback, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;
            if (trigger >= Now)
            {
                callback?.Invoke();
                return;
            }
            Timer timer = new(trigger, callback);
            cancellationToken.Register(() => CancelTimer(timer));
            timers.Add(timer);
        }

        public void SetTimer(TimeSpan trigger, Action callback, CancellationToken cancellationToken)
        {
            DateTime dateTrigger = Now + trigger;
            SetTimer(dateTrigger, callback, cancellationToken);
        }

        private void CancelTimer(Timer timer)
        {
            timers.Remove(timer);
        }

        public void SetLerp(DateTime Start, DateTime End, Action<float> callback, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;
            if (End >= Now)
            {
                callback?.Invoke(1);
                return;
            }
            Lerp lerp = new(Start, End, callback);
            cancellationToken.Register(() => CancelLerp(lerp));
            lerps.Add(lerp);
        }

        public void SetLerp(DateTime End, Action<float> callback, CancellationToken cancellationToken)
        {
            SetLerp(Now, End, callback, cancellationToken);
        }

        private void CancelLerp(Lerp lerp)
        {
            lerps.Remove(lerp);
        }
    }
}
