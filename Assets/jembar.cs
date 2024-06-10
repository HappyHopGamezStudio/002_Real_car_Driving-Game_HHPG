using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jembar : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Text>().text = PrefsManager.GetJEMValue().ToString();
    }

  
    void Update()
    {
        GetComponent<Text>().text = PrefsManager.GetJEMValue().ToString();
    }
}
