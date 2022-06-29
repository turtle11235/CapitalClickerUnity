using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MoneyManager
{
    private static MoneyManager instance;

    Money currentMoney;
    Money accumulatedMoney;
    Money maxMoneySoFar;

    public static MoneyManager Instance()
    {
        if (instance == null)
        {
            instance = new MoneyManager();
        }
        return instance;
    }
}
