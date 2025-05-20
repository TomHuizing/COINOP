using System;
using TMPro;
using UnityEngine;

public class ClockController : MonoBehaviour, ITimeReceiver
{
    [SerializeField] private TextMeshProUGUI timeText;

    void Start()
    {
        TimeManager.Instance.RegisterReceiver(this);
        timeText.text = TimeManager.Instance.Now.ToString("HH:mm");
    }

    public void Tick(TickPeriod period, DateTime dateTime) => timeText.text = dateTime.ToString("HH:mm");
    public void Cycle(CyclePeriod period, DateTime dateTime) {}
    public void Day(DateTime dateTime) {}
    public void TimeStart() {}
    public void TimeStop() {}
}
