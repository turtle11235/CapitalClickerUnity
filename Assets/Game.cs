using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static double money;
    public static double clickVal;

    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        clickVal = .01;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
