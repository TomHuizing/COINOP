using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeReceiver : MonoBehaviour, ITimeReceiver
{
    [SerializeField] private UnityEvent onTimeStart;
    [SerializeField] private UnityEvent onTimeStop;
    [SerializeField] private UnityEvent<TickPeriod, DateTime> onTick;
    [SerializeField] private UnityEvent<CyclePeriod, DateTime> onCycle;
    [SerializeField] private UnityEvent<DateTime> onDay;

    public UnityEvent<TickPeriod, DateTime> OnTick => onTick;
    public UnityEvent<CyclePeriod, DateTime> OnCycle => onCycle;
    public UnityEvent<DateTime> OnDay => onDay;

    void Start()
    {
        if(TimeManager.Instance == null)
            throw new System.Exception("TimeManager instance not found");
        TimeManager.Instance.RegisterReceiver(this);
    }

    public void Tick(TickPeriod period, DateTime dateTime) => onTick?.Invoke(period, dateTime);
    public void Cycle(CyclePeriod period, DateTime dateTime) => onCycle?.Invoke(period, dateTime);
    public void Day(DateTime dateTime) => onDay?.Invoke(dateTime);

    public void TimeStart() => onTimeStart?.Invoke();
    public void TimeStop() => onTimeStop?.Invoke();
}
