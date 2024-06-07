using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBar : MonoBehaviour
{
   
    void Awake()
    {
        GetComponent<Text>().text = PrefsManager.GetCoinsValue().ToString();
    }

  
    void Update()
    {
        GetComponent<Text>().text = PrefsManager.GetCoinsValue().ToString();
    }
}
