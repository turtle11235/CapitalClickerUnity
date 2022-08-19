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
    public double BaseWage { get; set; }
    public double WageMultiplier { get; set; }
    public int MaxManagerLevel { get; private set; }
    public List<Employee> Workers { get; set; }
    private WorkManager()
    {
        this.BaseWage = 10;
        this.WageMultiplier = 2;
        this.MaxManagerLevel = 1;
        this.BusinessUnlocked = false;
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
