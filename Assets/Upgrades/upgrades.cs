using UnityEngine;
using System;
using System.Collections.Generic;
public class UpgradeModuleUpgrade : Upgrade
{
    public override UpgradeID id { get { return UpgradeID.UPGRADE_MODULE; } }

    public override string title { get { return "Work Smarter Not Harder"; } }
    public override string description { get { return "Begin research into improving the money-making machine"; } }

    protected override bool CheckTriggerConditions() { return true; }

    public override bool Cost() { return true; }

    protected override void Execute() { }
}

public class MoneyMachineUpgrades : Upgrade
{
    public override UpgradeID id { get { return UpgradeID.MONEY_MACHINE; } }
    public override Dictionary<string, object> values
    {
        get
        {
            return new Dictionary<string, object>
            {
                { "title", new string[] {
                    "A Slight Improvement",
                    "Improved Clicking Technique",
                    "Larger Clicking Surface",
                    "Lubricated Button",
                    "Hardware Optimization"
                } },
                { "price", new int[] {1, 4, 10, 25, 50} },
                { "clickVal", new double[] {.05, .10, .25, .50, 1} }
            };
        }
    }
    public override string title
    {
        get
        {
            return this.getNext<string>("title");
        }
    }
    public override string description
    {
        get
        {
            return "Money machine gives " + this.getNext<double>("clickVal") + " per click";
        }
    }
    public override string pricetag
    {
        get
        {
            return "($" + this.getNext<int>("price") + ")";
        }
    }
    public override int maxUses { get { return 5; } }

    protected override bool CheckTriggerConditions()
    {
        return this.CheckDependency(UpgradeID.UPGRADE_MODULE);
    }

    public override bool Cost()
    {
        MoneyManager mm = MoneyManager.Instance;
        return mm.CurrentMoney > this.getNext<int>("price")-.01;
    }

    protected override void Execute()
    {
        MoneyManager mm = MoneyManager.Instance;
        mm.SpendMoney(this.getNext<int>("price"));
        mm.SetUserClickVal(this.getNext<double>("clickVal"));
    }

}

public class BusinessModuleUpgrade : Upgrade
{
    public override UpgradeID id { get { return UpgradeID.BUSINESS_MODULE; } }
    public override string title { get { return "Local Business License"; } }
    public override string pricetag { get { return "($10)"; } }
    public override string description { get { return "Why click when you can pay someone to click for you?"; } }

    protected override bool CheckTriggerConditions() 
    {
        return this.CheckDependency(UpgradeID.UPGRADE_MODULE) && MoneyManager.Instance.AccumulatedMoney >= 10;
    }

    public override bool Cost() { return MoneyManager.Instance.CurrentMoney >= 10; }

    protected override void Execute() 
    {
        MoneyManager.Instance.SpendMoney(10);
        WorkManager.Instance.BusinessUnlocked = true;
    }
}

public class LowerManagersUpgrade : Upgrade
{
    public override UpgradeID id { get { return UpgradeID.LOWER_MANAGERS; } }
    public override string title { get { return $"Beginner Business Techniques"; } }
    public override string pricetag { get { return "($100.00)"; } }
    public override string description { get { return $"Managers make {HiringManager.Instance.WageMultiplier}x workers' wages and oversee up to {HiringManager.Instance.WorkersPerManager} workers"; } }

    protected override bool CheckTriggerConditions()
    {
        return this.CheckDependency(UpgradeID.BUSINESS_MODULE) && CEO.Instance.NumWorkers >= HiringManager.Instance.WorkersPerManager && MoneyManager.Instance.AccumulatedMoney >= 100;
    }

    public override bool Cost() { return MoneyManager.Instance.CurrentMoney >= 100; }

    protected override void Execute()
    {
        MoneyManager.Instance.SpendMoney(100);
        HiringManager.Instance.MaxManagerLevel++;
    }
}

public class MiddleManagersUpgrade : Upgrade
{
    public override UpgradeID id { get { return UpgradeID.MIDDLE_MANAGERS; } }

    public override string title
    {
        get
        {
            return $"Middle Management{(this.UseCount > 0 ? " " + this.UseCount : "")}";
        }
    }
    public override string description
    {
        get
        {
            return $"Middle-managers make {HiringManager.Instance.WageMultiplier}x their subordinates' wages and oversee up to {HiringManager.Instance.ManagersPerManager} managers";
        }
    }
    public override string pricetag
    {
        get
        {
            return $"({this.Price})";
        }
    }
    public Money Price { get { return new Money(400 * Math.Pow(2, this.UseCount)); } }
    public override int maxUses { get { return int.MaxValue; } }

    protected override bool CheckTriggerConditions()
    {
        return this.CheckDependency(UpgradeID.LOWER_MANAGERS) && CEO.Instance.Level == HiringManager.Instance.MaxManagerLevel && CEO.Instance.IsFullAllLevels;
    }

    public override bool Cost()
    {
        MoneyManager mm = MoneyManager.Instance;
        return mm.CurrentMoney >= this.Price;
    }

    protected override void Execute()
    {
        MoneyManager mm = MoneyManager.Instance;
        mm.SpendMoney(this.Price);
        HiringManager.Instance.MaxManagerLevel++;
    }

}

public class WorkerMachineUpgrades : Upgrade
{
    public override UpgradeID id { get { return UpgradeID.WORKER_MACHINES; } }
    public override Dictionary<string, object> values
    {
        get
        {
            return new Dictionary<string, object>
            {
                { "price", new int[] { 25, 50, 100, 150, 250 } },
                { "clickVal", new double[] { .05, .10, .25, .50, 1 } }
            };
        }
    }
    public override string title
    {
        get
        {
            return $"Company Rollout ${(this.UseCount > 0 ? this.UseCount : "")}";
        }
    }
    public override string description
    {
        get
        {
            return $"Worker machines give ${new Money(this.getNext<double>("clickVal"))} per click";
        }
    }
    public override string pricetag
    {
        get
        {
            return "($" + this.getNext<int>("price") + ")";
        }
    }
    public override int maxUses { get { return 5; } }

    protected override bool CheckTriggerConditions()
    {
        return MoneyManager.Instance.UserClickVal >= this.getNext<double>("clickVal") && CEO.Instance.NumWorkers > 0;
    }

    public override bool Cost()
    {
        MoneyManager mm = MoneyManager.Instance;
        return mm.CurrentMoney > this.getNext<int>("price") - .01;
    }

    protected override void Execute()
    {
        MoneyManager mm = MoneyManager.Instance;
        mm.SpendMoney(this.getNext<int>("price"));
        mm.SetWorkerClickVal(this.getNext<double>("clickVal"));
    }

}

public class FriendsFamilyUpgrade : Upgrade
{
    public override UpgradeID id { get { return UpgradeID.FRIENDS_AND_FAM; } }
    public override string title { get { return "Friends and Family Investment"; } }
    public override string description { get { return "Receive $200 - This counts as your birthday present too..."; } }

    protected override bool CheckTriggerConditions()
    {
        return this.CheckDependency(UpgradeID.BUSINESS_MODULE);
    }

    public override bool Cost() { return true; }

    protected override void Execute()
    {
        MoneyManager.Instance.EarnMoney(200);
    }
}