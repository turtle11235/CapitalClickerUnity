
public class UpgradeModuleUpgrade : Upgrade {
    public new UpgradeID id = UpgradeID.UPGRADE_MODULE;

    public new string title = "Work Smarter Not Harder";
    public new string pricetag = "";
    public new string description = "Begin research into improving the money-making machine";

    protected override bool CheckTriggerConditions(){
        return true;
    }

    protected override void Execute(){}
}