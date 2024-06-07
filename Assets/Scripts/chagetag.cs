using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chagetag : MonoBehaviour
{
    public string Tag = "CarTrigger";
    private void OnEnable()
    {
        this.gameObject.tag = Tag;
    }

}
