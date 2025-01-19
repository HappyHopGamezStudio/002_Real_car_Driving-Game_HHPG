using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float time;

    // Start is called before the first frame update
    public void Destroyme()
    {
        Destroy(this.gameObject);
    }

    public bool isues = false;

    public void OnEnable()
    {
        if (isues)
        {
            Destroy(this.gameObject, time);
        }
    }
}
