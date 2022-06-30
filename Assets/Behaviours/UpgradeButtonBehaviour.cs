using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonBehaviour : MonoBehaviour
{
    public Button button;
    public Upgrade upgrade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = upgrade.Cost();
    }
}
