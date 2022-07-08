using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager {

    private static UpgradeManager instance;
    private List<Upgrade> upgrades;

    private UpgradeManager(){
        this.upgrades = new List<Upgrade>();
        this.upgrades.Add(new UpgradeModuleUpgrade());
        this.upgrades.Add(new MoneyMachineUpgrades());
        Debug.Log(this.upgrades[this.upgrades.Count-1].maxUses);
    }

    public static UpgradeManager Instance(){
        if (instance == null) {
            instance = new UpgradeManager();
        }
        return instance;
    }

    public List<Upgrade> getUpgrades(){
        List<Upgrade> visibleUpgrades = new List<Upgrade>();
        foreach (Upgrade upgrade in this.upgrades) {
            if (!upgrade.used && upgrade.Trigger()) {
                visibleUpgrades.Add(upgrade);
            }
        }
        return visibleUpgrades;
    }
}