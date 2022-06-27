using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyButtonBehaviour : MonoBehaviour
{
    public Text moneyText;
    public Game game;
    
    public void OnButtonPress()
    {
        game.money += game.clickVal;
        moneyText.text = "$" + game.money;
    }
}
