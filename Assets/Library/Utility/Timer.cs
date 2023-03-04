using System;
using System.Diagnostics;
using UnityEngine;

public class Timer
{
    // TODO: Connect timer to Counter instead of Stopwatch
    public float TimePeriodInSeconds { get; set; }
    public bool IsRunning { get; private set; }
    public bool RestartOnPeriodElapsed { get; set; }
    public Timer(float timePeriodInSeconds, bool resetOnPeriodElapsed = true)
    {
        this.TimePeriodInSeconds = timePeriodInSeconds;
        this.RestartOnPeriodElapsed = resetOnPeriodElapsed;
    }
    public float ElapsedSeconds
    {
        get
        {
            if (InstanceHasStarted)
            {
                return Counter.ElapsedSeconds - this.StartTime + this.AccumulatedTime;
            }
            return 0;
        }
    }
    private float StartTime;
    private float AccumulatedTime = 0;
    private bool InstanceHasStarted = false;
    public void Start()
    {
        this.StartTime = Counter.ElapsedSeconds;
        this.IsRunning = true;
        this.InstanceHasStarted = true;
    }
    public void Stop()
    {
        this.IsRunning = false;
        this.AccumulatedTime += Counter.ElapsedSeconds - this.StartTime;
    }
    public void Reset()
    {
        this.Stop();
        this.AccumulatedTime = 0;
        this.InstanceHasStarted = false;
    }
    public void Restart()
    {
        this.Reset();
        this.Start();
    }
    public bool PeriodHasElapsed 
    {
        get
        {
            if (this.RemainingSeconds == 0)
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
    public float RemainingSeconds
    {
        get
        {
            return Math.Max(this.TimePeriodInSeconds - this.ElapsedSeconds, 0);
        }
    }

}
