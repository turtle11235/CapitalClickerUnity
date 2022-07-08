using UnityEngine;
using UnityEngine.UI;

public class MoneyButtonBehaviour : MonoBehaviour
{
    private MoneyManager moneyManager = MoneyManager.Instance;

    public void OnButtonPress()
    {
        moneyManager.UserClick();
    }
}
