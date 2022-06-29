using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            UpgradeModule = Instantiate(UpgradeModulePrefab).transform as Transform;
            UpgradeModule.SetParent(parent, false);
            instantiated = true;
        }

        if (instantiated) {
            List<Upgrade> newUpgrades = um.getUpgrades();
            foreach(Upgrade upgrade in newUpgrades){
                if (!upgrades.Contains(upgrade)){
                    Debug.Log(upgrade);
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
        title.text = upgrade.title;

        Text description = buttonT.GetChild(0).GetChild(1).GetComponent(typeof(Text)) as Text;
        description.text = upgrade.description;        


        button.GetComponent<Button>().onClick.AddListener(delegate { OnUpgradeButtonClick(upgrade, button); });
    }

    void OnUpgradeButtonClick(Upgrade upgrade, GameObject button){
        upgrade.Use();
        Destroy(button);
    }
}
