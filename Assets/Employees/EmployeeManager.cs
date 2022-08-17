using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeManager
{
    private static EmployeeManager _Instance;
    public static EmployeeManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new EmployeeManager();
            }
            return _Instance;
        }
    }

    public double BaseWage { get; set; }
    public double WageMultiplier { get; set; }
    public int MaxManagerLevel { get; private set; }

    private EmployeeManager()
    {
        this.BaseWage = 10;
        this.WageMultiplier = 2;
        this.MaxManagerLevel = 1;
    }


}
