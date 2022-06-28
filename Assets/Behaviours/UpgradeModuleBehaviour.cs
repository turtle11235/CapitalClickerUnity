using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeModuleBehaviour : MonoBehaviour
{
    public Game game;
    private UpgradeManager um = UpgradeManager.Instance();
    private List<Upgrade> upgrades = new List<Upgrade>();
    public GameObject UpgradeModulePrefab;
    private Transform UpgradeModule;
    public GameObject UpgradeButton;
    public Transform parent;
    bool instantiated = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!instantiated && game.money >= .75) {
            UpgradeModule = Instantiate(UpgradeModulePrefab).transform.Find("Upgrades") as Transform;
            UpgradeModule.SetParent(parent, false);
            instantiated = true;
        }

        if (instantiated) {
            List<Upgrade> newUpgrades = um.getUpgrades();
            foreach(Upgrade upgrade in newUpgrades){
                if (!upgrades.Contains(upgrade)){
                    displayUpgrade(upgrade);
                }
            }
            upgrades = newUpgrades;
        }
    }

    void displayUpgrade(Upgrade upgrade){
        Transform button = Instantiate(UpgradeButton).transform as Transform;
        button.SetParent(UpgradeModule, false);
    }
}
