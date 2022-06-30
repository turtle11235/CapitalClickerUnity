using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeModuleBehaviour : MonoBehaviour
{
    private UpgradeManager um = UpgradeManager.Instance();
    private List<Upgrade> upgrades = new List<Upgrade>();
    public GameObject UpgradeModulePrefab;
    private Transform UpgradeModule;
    public GameObject UpgradeButton;
    public GameObject UpgradeButtonBehaviourPrefab;
    public Transform parent;
    bool instantiated = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!instantiated && Game.money >= .75) {
            UpgradeModule = Instantiate(UpgradeModulePrefab).transform as Transform;
            UpgradeModule.SetParent(parent, false);
            instantiated = true;
        }

        if (instantiated) {
            List<Upgrade> newUpgrades = um.getUpgrades();
            // Debug.Log("upgrades, " + upgrades.Count);
            // Debug.Log("newUpgrades, " + newUpgrades.Count);
            foreach(Upgrade upgrade in newUpgrades){
                if (!upgrades.Contains(upgrade)){
                    Debug.Log(upgrade.maxUses);
                    displayUpgrade(upgrade);
                }
            }
            upgrades = newUpgrades;
        }
    }

    void displayUpgrade(Upgrade upgrade){
        GameObject button = Instantiate(UpgradeButton);
        Transform buttonT = button.transform as Transform;
        buttonT.SetParent(UpgradeModule.GetChild(2), false);

        Text title = buttonT.GetChild(0).GetChild(0).GetComponent(typeof(Text)) as Text;
        title.text = upgrade.ToString();

        Text description = buttonT.GetChild(0).GetChild(1).GetComponent(typeof(Text)) as Text;
        description.text = upgrade.description;     

        button.GetComponent<Button>().onClick.AddListener(delegate { OnUpgradeButtonClick(upgrade, button); });

        GameObject buttonBehaviour = Instantiate(UpgradeButtonBehaviourPrefab);
        buttonBehaviour.GetComponent<UpgradeButtonBehaviour>().button = button.GetComponent<Button>();
        buttonBehaviour.GetComponent<UpgradeButtonBehaviour>().upgrade = upgrade;
    }

    void OnUpgradeButtonClick(Upgrade upgrade, GameObject button){
        upgrade.Use();
        Destroy(button);
        upgrades.Remove(upgrade);
    }
}
