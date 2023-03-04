using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketingManager : MonoBehaviour
{
    private static MarketingManager _Instance;
    public static MarketingManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new MarketingManager();
            }
            return _Instance;
        }
    }

    public bool BusinessUnlocked { get; set; }


}
