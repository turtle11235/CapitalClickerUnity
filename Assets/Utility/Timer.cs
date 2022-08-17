using System;
using System.Diagnostics;
using UnityEngine;

public class Timer : Stopwatch
{

    public long TimePeriodInMilliseconds { get; set; }
    public bool PeriodHasElapsed 
    {
        get
        {
            if (this.ElapsedMilliseconds >= this.TimePeriodInMilliseconds)
            {
                if (this.RestartOnPeriodElapsed)
                {
                    this.Restart();
                }
                return true;
            }
            return false;
        }
    }

    public bool RestartOnPeriodElapsed { get; set; }

    public Timer(long timePeriodInMilliseconds, bool resetOnPeriodElapsed = true)
    {
        this.TimePeriodInMilliseconds = timePeriodInMilliseconds;
        this.RestartOnPeriodElapsed = resetOnPeriodElapsed;
    }

}
