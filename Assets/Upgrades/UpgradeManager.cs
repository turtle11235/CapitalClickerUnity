using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager {

    private static UpgradeManager instance;
    private Dictionary<UpgradeID, Upgrade> upgrades;

    private UpgradeManager(){
        this.upgrades = new()
        {
            { UpgradeID.UPGRADE_MODULE, new UpgradeModuleUpgrade() },
            { UpgradeID.MONEY_MACHINE, new MoneyMachineUpgrades() }
        };
    }

    public static UpgradeManager Instance(){
        if (instance == null) {
            instance = new UpgradeManager();
        }
        return instance;
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