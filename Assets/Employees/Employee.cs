using System;
using System.Collections.Generic;
using UnityEngine;

public class Employee
{
    public int Level { get; set; }
    public Employee Boss { get; set; }
    public List<Employee> Subordinates {get; set;}
    public string Name { get; set; }
    public string Title { get; set; }
    public virtual WorkTasks Task { get; set; }
    public Money Wage
    {
        get
        {
            double baseWage = EmployeeManager.Instance.BaseWage;
            double multiplier = EmployeeManager.Instance.WageMultiplier;
            return new Money(baseWage * Math.Pow(multiplier, this.Level));
        }
    }
    public Money BranchWages
    {
        get
        {
            Money sum = this.Wage;
            foreach (Employee subordinate in this.Subordinates)
            {
                sum += subordinate.BranchWages;
            }
            return sum;
        }
    }

    private Timer workTimer = new Timer(1 * 1000); 
    
    public Employee()
    {

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
            return this.BranchWages;
        }
        else if (MoneyManager.Instance.CurrentMoney > this.BranchWages)
        {
            return new Money(0);
        }
        else
        {
            return new Money(0);
        }
    }

}
