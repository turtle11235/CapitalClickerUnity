using UnityEngine;
using UnityEngine.UI;

public class MoneyButtonBehaviour : MonoBehaviour
{
    public Text moneyText;
    private MoneyManager moneyManager = MoneyManager.Instance;
    
    public void OnButtonPress()
    {
        moneyManager.UserClick();
        moneyText.text = moneyManager.CurrentMoney.ToString();
    }
}
