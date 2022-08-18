using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeModuleBehaviour : MonoBehaviour
{
    private UpgradeManager um = UpgradeManager.Instance();
    private List<Upgrade> currUpgrades = new List<Upgrade>();

    private Dictionary<UpgradeID, GameObject> buttons = new Dictionary<UpgradeID, GameObject>();
    public GameObject UpgradeButton;

    public Transform UpgradeArea;

/*    public GameObject UpgradeModulePrefab;
    private Transform UpgradeModule;
    public Transform parent;*/

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        List<Upgrade> newUpgrades = um.GetUpgrades();
        IEnumerable<Upgrade> allUpgrades = newUpgrades.Union(currUpgrades);

        foreach(Upgrade upgrade in allUpgrades){
            if (!newUpgrades.Contains(upgrade))
            {
                currUpgrades.Remove(upgrade);
                buttons.Remove(upgrade.id);
            }
            else
            {
                if (!currUpgrades.Contains(upgrade)){
                    GameObject button = CreateUpgradeButton(upgrade);
                    currUpgrades.Add(upgrade);
                    buttons[upgrade.id] = button;
                }
                updateUpgradeButton(upgrade);
            }
        }

    }

/*    void Instantiate()
    {
        UpgradeModule = Instantiate(UpgradeModulePrefab).transform as Transform;
        UpgradeModule.SetParent(parent, false);
        instantiated = true;
    }*/

    GameObject CreateUpgradeButton(Upgrade upgrade){
        GameObject button = Instantiate(UpgradeButton);
        Transform buttonT = button.transform as Transform;
        buttonT.SetParent(UpgradeArea, false);

        Text title = buttonT.GetChild(0).GetChild(0).GetComponent(typeof(Text)) as Text;
        title.text = upgrade.ToString();

        Text description = buttonT.GetChild(0).GetChild(1).GetComponent(typeof(Text)) as Text;
        description.text = upgrade.description;     

        button.GetComponent<Button>().onClick.AddListener(delegate { OnUpgradeButtonClick(upgrade, button); });

        return button;
    }

    void OnUpgradeButtonClick(Upgrade upgrade, GameObject button){
        upgrade.Use();
        Destroy(button);
        buttons.Remove(upgrade.id);
        currUpgrades.Remove(upgrade);
    }

    void updateUpgradeButton(Upgrade upgrade)
    {
        GameObject button = buttons[upgrade.id];
        button.GetComponent<Button>().interactable = upgrade.Cost();
    }
}
