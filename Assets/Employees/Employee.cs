using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Employee
{
    public virtual int Level { get; set; }
    public Employee Boss { get; set; }
    public List<Employee> Subordinates {get; set;}
    public string Name { get; set; }
    public string Title { get; set; }
    public WorkTasks Task { get; set; }
    public int NumWorkers
    {
        get
        {
            if (this.Level == 0)
            {
                return 0;
            }
            else if (this.Level == 1)
            {
                return this.Subordinates.Count;
            }
            else
            {
                return this.Subordinates.Sum(subordinate => subordinate.NumWorkers);
            }
        }
    }
    public int NumManagers
    {
        get
        {
            if (this.Level <= 1)
            {
                return 0;
            }
            else if (this.Level == 2)
            {
                return this.Subordinates.Count;
            }
            else
            {
                return this.Subordinates.Sum(subordinate => subordinate.NumManagers) + this.Subordinates.Count;
            }
        }
    }
    public int MaxSubordinates
    {
        get
        {
            if (this.Level == 0)
            {
                return 0;
            }
            else if (this.Level == 1)
            {
                return HiringManager.Instance.WorkersPerManager;
            }
            else
            {
                return HiringManager.Instance.ManagersPerManager;
            }
        }
    }
    public bool IsFull
    {
        get
        {
            return this.Subordinates.Count >= this.MaxSubordinates;
        }
    }

    public bool IsFullAllLevels
    {
        get 
        { 
            if (this.MaxSubordinates == 0)
            {
                return true;
            }
            return this.IsFull && this.Subordinates.TrueForAll(s => s.IsFullAllLevels);
        }
    }

    public Money Wage
    {
        get
        {
            double baseWage = WorkManager.Instance.BaseWage;
            double multiplier = WorkManager.Instance.WageMultiplier;
            return new Money(baseWage * Math.Pow(multiplier, this.Level));
        }
    }
    public Money TotalBranchWages
    {
        get
        {
            Money sum = this.Wage;
            foreach (Employee subordinate in this.Subordinates)
            {
                sum += subordinate.TotalBranchWages;
            }
            return sum;
        }
    }

    private Timer workTimer = new Timer(1 * 1000);

    public Employee(int level = 0, Employee boss = null, List<Employee> subordinates = null)
    {
        this.Level = level;
        this.Boss = boss;
        this.Subordinates = subordinates != null ? subordinates : new List<Employee>();
        this.Task = WorkManager.Instance.AssignTask(this);
        if (this.Level == 0)
        {
            this.workTimer.Start();
        }
    }

    public void PerformTask()
    {
        switch (this.Task) {
            case WorkTasks.CLICK:
                MoneyManager.Instance.WorkerClick();
                break;
            case WorkTasks.MANAGEMENT:
                break;
            default:
                return;
        }
    }

    public void Work()
    {
        if (this.Level > 0)
        {
            foreach(Employee subordinate in this.Subordinates)
            {
                subordinate.Work();
            }
        }
        else
        {
            if (this.workTimer.PeriodHasElapsed)
            {
                this.PerformTask();
            }
        }
    }

    public Money Pay()
    {
        if (MoneyManager.Instance.CurrentMoney < this.Wage)
        {
            // TODO: HiringManager.Instance.quit();
            return this.TotalBranchWages;
        }
        else if (MoneyManager.Instance.CurrentMoney > this.TotalBranchWages)
        {
            return new Money(0);
        }
        else
        {
            return new Money(0);
        }
    }

}
