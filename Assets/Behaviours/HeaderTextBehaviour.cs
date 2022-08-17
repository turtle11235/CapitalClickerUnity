using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderTextBehaviour : MonoBehaviour
{
    private MoneyManager mm = MoneyManager.Instance;
    public Text moneyText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = mm.CurrentMoney.ToString();
    }
}
