using UnityEngine;
using UnityEngine.UI;

internal class MoneyTextBehavior : MonoBehaviour
{
    public Text moneyText;
    private MoneyManager moneyManager = MoneyManager.Instance;

    void Update()
    {
        moneyText.text = moneyManager.CurrentMoney.ToString();
    }
}
