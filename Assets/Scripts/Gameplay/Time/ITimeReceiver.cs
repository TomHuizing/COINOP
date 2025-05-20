using System;
using UnityEngine;

public interface ITimeReceiver
{
    public void Tick(TickPeriod period, DateTime dateTime);
    public void Cycle(CyclePeriod period, DateTime dateTime);
    public void Day(DateTime dateTime);
    public void TimeStart();
    public void TimeStop();
}
