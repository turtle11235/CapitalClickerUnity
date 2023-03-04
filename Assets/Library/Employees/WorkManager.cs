using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkManager
{
    private static WorkManager _Instance;
    public static WorkManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new WorkManager();
            }
            return _Instance;
        }
    }
    public bool BusinessUnlocked { get; set; }
    public List<Employee> Workers { get; set; }
    public float PayPeriodInSeconds { get; set; }
    public Timer PayTimer { get; private set; }
    private WorkManager()
    {
        this.BusinessUnlocked = false;
        this.PayPeriodInSeconds = (60 * 60 * 24) / Clock.IRLToInGameTimeRatio; // Employees are paid once per in-game day
        this.PayTimer = new Timer(this.PayPeriodInSeconds);
        this.PayTimer.Start();
    }

    public void Execute()
    {
        CEO.Instance.Work();
        if (CEO.Instance.NumEmployees > 0 && this.PayTimer.PeriodHasElapsed)
        {
            CEO.Instance.Pay();
            MoneyManager.Instance.SpendMoney(CEO.Instance.TotalBranchWages);
        }
    }

    public WorkTasks AssignTask(Employee e)
    {
        if (e.Level > 0)
        {
            return WorkTasks.MANAGEMENT;
        }
        else
        {
            return WorkTasks.CLICK;
        }
    }

}
