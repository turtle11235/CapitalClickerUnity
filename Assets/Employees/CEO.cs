using System.Collections.Generic;
using UnityEngine;

public class CEO : Employee
{
    private static CEO _Instance;
    public static CEO Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new CEO();
            }
            return _Instance;
        }
    }

    public override int Level
    {
        get
        {
            if (this.Subordinates.Count == 0)
            {
                return 1;
            }
            return this.Subordinates[0].Level + 1;
        }
    }

    public override Money Wage { get { return new Money(0); } }

    private CEO()
    {
        this.Task = WorkTasks.MANAGEMENT;
    }

    public Money Pay(Money funds = null)
    {
        funds = funds ?? MoneyManager.Instance.CurrentMoney;
        Queue<Employee> employees = new Queue<Employee>(this.Subordinates);
        Employee currEmployee;
        while(employees.TryDequeue(out currEmployee))
        {
            if (funds < currEmployee.Wage)
            {
                funds -= currEmployee.TotalBranchWages;
                HiringManager.Instance.Fire(currEmployee);
            }
            else
            {
                funds = funds - currEmployee.Wage;
                if (funds > 0)
                {
                    foreach(Employee e in currEmployee.Subordinates)
                    {
                        employees.Enqueue(e);
                    }
                }
            }

        }
        return funds;
    }
}