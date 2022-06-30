using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyButtonBehaviour : MonoBehaviour
{
    public Text moneyText;
    
    public void OnButtonPress()
    {
        Game.money += Game.clickVal;
        moneyText.text = "$" + Game.money;
    }
}
