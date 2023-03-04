using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager {

    private static UpgradeManager instance;
    public static UpgradeManager Instance(){
        if (instance == null) {
            instance = new UpgradeManager();
        }
        return instance;
    }

    private Dictionary<UpgradeID, Upgrade> upgrades;

    private UpgradeManager(){
        this.upgrades = new()
        {
            { UpgradeID.UPGRADE_MODULE, new UpgradeModuleUpgrade() },
            { UpgradeID.MONEY_MACHINE, new MoneyMachineUpgrades() },
            { UpgradeID.BUSINESS_MODULE, new BusinessModuleUpgrade() },
            { UpgradeID.LOWER_MANAGERS, new LowerManagersUpgrade() },
            { UpgradeID.WORKER_MACHINES, new WorkerMachineUpgrades() },
            { UpgradeID.MIDDLE_MANAGERS, new MiddleManagersUpgrade() },
            { UpgradeID.FRIENDS_AND_FAM, new FriendsFamilyUpgrade() }
        };
    }


    public Upgrade GetUpgrade(UpgradeID id)
    {
        return this.upgrades[id];
    }

    public List<Upgrade> GetUpgrades(){
        List<Upgrade> visibleUpgrades = new List<Upgrade>();
        foreach (Upgrade upgrade in this.upgrades.Values) {
            if (!upgrade.used && upgrade.Trigger()) {
                visibleUpgrades.Add(upgrade);
            }
        }
        return visibleUpgrades;
    }
}