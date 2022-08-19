using System.Collections;
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
                return 0;
            }
            return this.Subordinates[0].Level + 1;
        }
    }

    private CEO()
    {
        this.Task = WorkTasks.MANAGEMENT;
    }
}