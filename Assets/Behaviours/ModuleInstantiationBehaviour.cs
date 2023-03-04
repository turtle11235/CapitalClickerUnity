using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module
{
    public GameObject Prefab { get; set; }
    public Transform Parent { get; set; }
    public bool Instantiated { get; set; }

    public Module(GameObject prefab, Transform parent)
    {
        Prefab = prefab;
        Parent = parent;
        Instantiated = false;
    }
}
public class ModuleInstantiationBehaviour : MonoBehaviour
{
    public Transform LeftCol;
    public Transform MiddleCol;
    public Transform RightCol;

    public GameObject BusinessPrefab;
    private Module BusinessModule;

    public GameObject UpgradePrefab;
    private Module UpgradeModule;

    // Start is called before the first frame update
    void Start()
    {
        BusinessModule = new Module(BusinessPrefab, MiddleCol);
        UpgradeModule = new Module(UpgradePrefab, RightCol);
    }

    // Update is called once per frame
    void Update()
    {
        if (MoneyManager.Instance.CurrentMoney > new Money(.74))
        {
            InstantiateModule(UpgradeModule);
        }
        if (WorkManager.Instance.BusinessUnlocked)
        {
            InstantiateModule(BusinessModule);
        }
    }

    void InstantiateModule(Module module)
    {
        if (!module.Instantiated)
        {
            Transform instantiatedModule = Instantiate(module.Prefab).transform as Transform;
            instantiatedModule.SetParent(module.Parent, false);
            module.Instantiated = true;
        }

    }
}
