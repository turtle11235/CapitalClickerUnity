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
    public int HireLevel { get; set; }
    public int HireBonus { get; set; }
    public int FireBonus { get; set; }
    public float DelayMean { get; set; }
    public float DelaySTD { get; set; }

    public readonly int WorkersPerManager = 5;
    public readonly int ManagersPerManager = 5;
    private HiringManager()
    {
        this.HireLevel = 0;
        this.HireBonus = 2;
        this.FireBonus = 2;
        this.DelayMean = 3;
        this.DelaySTD = 1;
    }

    public void Hire()
    {
        this.HireNextEmployee();
        return;
    }

    private Employee HireNextEmployee()
    {
        if (this.Root.IsFullAllLevels)
        {
            Employee e = new Employee(this.Root.Level, this.Root, this.Root.Subordinates);
            this.Root.Subordinates = new List<Employee>() { e };
            return e;
        }

        Employee currNode = this.Root;
        Employee nextNode = currNode;
        while (currNode.Level > 0)
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
        }

        if (!currNode.IsFull)
        {
            Employee newHire = new Employee(currNode.Level - 1, currNode);
            currNode.Subordinates.Add(newHire);
            return newHire;
        }
        else
        {
            throw new System.Exception("An error has occured while hiring");
        }
        
        throw new System.Exception("failed to hire employee");
        
    }

    /*private Employee CreateEmployee(int level, Employee boss, List<Employee> subordinates, bool bossIsCEO = false )
    {
        Employee newEmployee = new Employee(level, boss, subordinates);
        if (bossIsCEO)
        {

        }
    }*/
}
