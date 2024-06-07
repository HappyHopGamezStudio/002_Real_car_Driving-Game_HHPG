using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTag : MonoBehaviour
{
    // Start is called before the first frame update

    public string Tag = "TPSTrigger";
    private void OnEnable()
    {
        this.gameObject.tag = Tag;
    }


   
}
