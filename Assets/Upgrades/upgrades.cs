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
        return mm.CurrentMoney > new Money((double)this.getNext<int>("price")-.01);
    }

    protected override void Execute()
    {
        MoneyManager mm = MoneyManager.Instance;
        mm.SpendMoney(new Money((double)this.getNext<int>("price")));
        mm.SetUserClickVal(new Money(this.getNext<double>("clickVal")));
    }

}