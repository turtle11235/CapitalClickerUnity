using UnityEngine;
using System;
using System.Collections.Generic;

public class UpgradeModuleUpgrade : Upgrade {
    public override UpgradeID id { get { return UpgradeID.UPGRADE_MODULE;}}

    public override string title { get { return "Work Smarter Not Harder";} }
    public override string description { get {return "Begin research into improving the money-making machine"; } }

    protected override bool CheckTriggerConditions(){ return true; }

    public override bool Cost(){ return true; }

    protected override void Execute(){}
}

public class MoneyMachineUpgrades : Upgrade {
    public override UpgradeID id { get { return UpgradeID.MONEY_MACHINE; } }
    public override Dictionary<string, object[]> values {
        get {
            return new Dictionary<string, object[]>
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
    public override string title {
        get {
            return this.getNext<string>("title");
        }
    }
    public override string description {
        get {
            return "Money machine gives " + this.getNext<string>("description") + " per click";
        }
    }
    public override string pricetag {
        get {
            return "($" + this.getNext<int>("price") + ")"; 
        }
    }

    protected override bool CheckTriggerConditions(){
        return this.CheckDependencies(UpgradeID.UPGRADE_MODULE);
    }

    public override bool Cost(){
        return Game.money >= this.getNext<int>("price");
    }

    protected override void Execute(){
        Game.money -= this.getNext<int>("cost");
        Game.clickVal = this.getNext<int>("clickVal");
    }

}