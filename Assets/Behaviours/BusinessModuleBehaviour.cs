using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class BusinessModuleBehaviour : MonoBehaviour
{
    private HiringManager hm = HiringManager.Instance;

    public Text WorkerCountText;
    public Button HireButton;
    public Text HireButtonText;
    public Button FireButton;
    public Text FireButtonText;
    public Text WageText;
    public Text PaydayText;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WorkManager.Instance.Execute();
        WorkerCountText.text = string.Format("Workers: {0}\t\tManagers: {1}", CEO.Instance.NumWorkers, CEO.Instance.NumManagers);
        HireButton.interactable = HiringManager.Instance.CanHire;
        HireButtonText.text = string.Format("Hire: {0:C2}", HiringManager.Instance.HireNextCost);
        FireButton.interactable = HiringManager.Instance.CanFire;
        FireButtonText.text = $"Fire: {(HiringManager.Instance.FireNextCost == null ? "n/a" : HiringManager.Instance.FireNextCost)}";
        if (CEO.Instance.NumWorkers > 0)
        {
            float remainingSeconds = WorkManager.Instance.PayTimer.RemainingSeconds * Clock.IRLToInGameTimeRatio;
            float hours = Mathf.Ceil(remainingSeconds / (60 * 60));
            float days = Mathf.Floor(remainingSeconds / (60 * 60 * 24));
            if (hours == 24)
            {
                days++;
                hours = 0;
            }
            string s = $"Next Payday: {CEO.Instance.TotalBranchWages} in {days} day{(days != 1 ? 's' : "")}, ";
            s += $"{hours} hours";
            PaydayText.text = s;
        }
        else
        {
            PaydayText.text = "Next Payday: n/a";
        }
    }

    public void OnFireButtonClicked()
    {
        MoneyManager.Instance.SpendMoney(hm.FireNextCost);
        hm.Fire();
    }

    public void OnHireButtonClicked()
    {
        MoneyManager.Instance.SpendMoney(hm.HireNextCost);
        hm.Hire();
    }
}