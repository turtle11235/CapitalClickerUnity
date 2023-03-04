using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiringManager
{
    private static HiringManager _Instance;
    public static HiringManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new HiringManager();
            }
            return _Instance;
        }
    }

    private Employee Root = CEO.Instance;
    public readonly int WorkersPerManager = 5;
    public readonly int ManagersPerManager = 5;

    private HiringManager()
    {
        this.HireLevel = 0;
        this.BaseWage = 10;
        this.MaxManagerLevel = 1;
        this.WageMultiplier = 2;
        this.HireBonus = 3;
        this.FireBonus = 2;
        this.DelayMean = 3;
        this.DelaySTD = 1;
    }

    public int HireLevel { get; set; }
    public double BaseWage { get; set; }
    public int MaxManagerLevel { get; set; }
    public float WageMultiplier { get; set; }
    public float HireBonus { get; set; }
    public double FireBonus { get; set; }
    public float DelayMean { get; set; }
    public float DelaySTD { get; set; }

    public void Hire()
    {
        this.HireNextEmployee();
    }

    public void Fire(Employee employee=null)
    {
        employee = employee ?? this.GetNextFiringNode();
        employee.Boss.Subordinates.Remove(employee);
    }

    private Employee HireNextEmployee()
    {
        if (this.Root.IsFullAllLevels)
        {
            Employee e = new Employee(this.Root.Level, this.Root, this.Root.Subordinates);
            foreach(Employee subordinate in e.Subordinates)
            {
                subordinate.Boss = e;
            }
            this.Root.Subordinates = new List<Employee>() { e };
            return e;
        }

        Employee hiringNode = this.GetNextHiringNode();

        if (!hiringNode.IsFull)
        {
            Employee newHire = new Employee(hiringNode.Level - 1, hiringNode);
            hiringNode.Subordinates.Add(newHire);
            return newHire;
        }
        else
        {
            throw new System.Exception("An error has occured while hiring");
        }
        
        throw new System.Exception("failed to hire employee");
        
    }

    private Employee GetNextHiringNode()
    {
        Employee currNode = this.Root;
        Employee nextNode = currNode;
        while (currNode.Level > 1)
        {
            foreach (Employee subordinate in currNode.Subordinates)
            {
                if (!subordinate.IsFullAllLevels)
                {
                    nextNode = subordinate;
                    break;
                }
            }

            if (nextNode == currNode)
            {
                break;
            }
            else
            {
                currNode = nextNode;
            }
        }
        return currNode;
    }

    private Employee GetNextFiringNode(Employee startingNode = null)
    {
        if (this.Root.NumWorkers + this.Root.NumManagers == 0)
        {
            return null;
        }

        Employee currNode = startingNode ?? this.Root;
        Employee nextNode = currNode;
        while (currNode.Level > 0)
        {
            if (currNode.Subordinates.Count > 0)
            {
                currNode = currNode.Subordinates[currNode.Subordinates.Count - 1];
            }
            else
            {
                break;
            }
        }
        return currNode;
    }

    public double HireNextCost
    {
        get
        {
            if (this.Root.IsFullAllLevels)
            {
                return this.HireBonus * this.BaseWage * Mathf.Pow(this.WageMultiplier, this.Root.Level);
            }
            Employee hiringNode = this.GetNextHiringNode();
            return this.HireBonus * this.BaseWage * Mathf.Pow(this.WageMultiplier, hiringNode.Level - 1);
        }
    } 
    public Money FireNextCost
    {
        get
        {
            if (this.Root.NumEmployees == 0)
            {
                return null;
            }
            return this.GetNextFiringNode().Wage * this.FireBonus;
        }
    }

    public bool CanHire
    {
        get
        {
            if (this.Root.Level < this.MaxManagerLevel || !this.Root.IsFullAllLevels)
            {
                return MoneyManager.Instance.CurrentMoney >= this.HireNextCost;
            }
            return false;
        }
    }

    public bool CanFire
    {
        get
        {
            if (CEO.Instance.NumEmployees > 0)
            {
                Employee nextFire = this.GetNextFiringNode();
                return MoneyManager.Instance.CurrentMoney >= (this.FireNextCost ?? new Money(0));
            }
            return false;
        }
    }
}
