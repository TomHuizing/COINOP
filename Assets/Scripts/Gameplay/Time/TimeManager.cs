using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Serializable]
    public struct TimeInspector
    {
        [Range(0, 23)] public int Hour;
        [Range(0, 59)] public int Minute;
    }

    [Serializable]
    public struct DateInspector
    {
        public int Year;
        public int Month;
        public int Day;
    }

    public static TimeManager Instance { get; private set; }
    
    [SerializeField] private TimeInspector time;
    [SerializeField] private DateInspector date;
    [SerializeField] private TickPeriod tickPeriod;
    [SerializeField] private TickDuration tickDuration;
    [SerializeField] private CyclePeriod cyclePeriod;

    private bool isTimeRunning = false;
    private readonly List<ITimeReceiver> timeReceivers = new();

    public DateTime Now { get; private set; }
    public bool IsTimeRunning => isTimeRunning;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        Now = new(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
    }

    void Start()
    {
        StartTime();
    }

    private IEnumerator TickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds((float)tickDuration / 1000f);
            Now = Now.AddMinutes((int)tickPeriod);
            time.Hour = Now.Hour;
            time.Minute = Now.Minute;
            foreach (ITimeReceiver timeReceiver in timeReceivers)
            {
                timeReceiver.Tick(tickPeriod, Now);
            }

        }
    }

    private IEnumerator CycleCoroutine()
    {
        DateTime compareDate;
        while (true)
        {
            yield return new WaitUntil(() => (int)Now.TimeOfDay.TotalMinutes % (int)cyclePeriod == 0);
            foreach (ITimeReceiver timeReceiver in timeReceivers)
            {
                timeReceiver.Cycle(cyclePeriod, Now);
            }
            compareDate = Now;
            yield return new WaitUntil(() => Now >= compareDate.AddMinutes((int)cyclePeriod));
        }
    }

    private IEnumerator DayCoroutine()
    {
        DateTime compareDate = Now;
        while (true)
        {
            yield return new WaitUntil(() => Now >= compareDate.AddDays(1).Add(-compareDate.TimeOfDay));
            compareDate = Now;
            foreach (ITimeReceiver timeReceiver in timeReceivers)
            {
                timeReceiver.Day(Now);
            }
        }
    }

    public void StartTime()
    {
        StartCoroutine(TickCoroutine());
        StartCoroutine(CycleCoroutine());
        StartCoroutine(DayCoroutine());
        isTimeRunning = true;
    }

    public void StopTime()
    {
        StopAllCoroutines();
        isTimeRunning = false;
    }

    public void RegisterReceiver(ITimeReceiver timeReceiver)
    {
        if (timeReceivers.Contains(timeReceiver))
            return;
        timeReceivers.Add(timeReceiver);
    }

    public void UnregisterReceiver(ITimeReceiver timeReceiver) => timeReceivers.Remove(timeReceiver);
}
