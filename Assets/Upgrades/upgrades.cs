using UnityEngine;

public class UpgradeModuleUpgrade : Upgrade {
    public override UpgradeID id { get { return UpgradeID.UPGRADE_MODULE;}}

    public override string title { get { return "Work Smarter Not Harder";} }
    public override string description { get {return "Begin research into improving the money-making machine"; } }

    protected override bool CheckTriggerConditions(){
        return true;
    }

    protected override void Execute(){}
}