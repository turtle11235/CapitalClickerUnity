using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class BusinessModuleBehaviour : MonoBehaviour
{
    private HiringManager hm = HiringManager.Instance;

    public GameObject BusinessModulePrefab;
    private Transform BusinessModule;
    public Transform parent;

    public Text WorkerCountText;
    public Text WageText;
    public Text HireCostText;
    public Text PaydayText;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CEO.Instance.Work();
        WorkerCountText.text = string.Format("Workers: {0}\t\tManagers: {1}", CEO.Instance.NumWorkers, CEO.Instance.NumManagers);
    }

/*    void Instantiate()
    {
        BusinessModule = Instantiate(BusinessModulePrefab).transform as Transform;
        BusinessModule.SetParent(parent, false);
        instantiated = true;
    }*/

    public void OnFireButtonClicked()
    {

    }

    public void OnHireButtonClicked()
    {
        hm.Hire();
        Debug.Log("hiring employee");
    }
}