//______________________________________________//
//________Realistic Car Shaders - Mobile________//
//______________________________________________//
//_______Copyright © 2019 Yugel Mobile__________//
//______________________________________________//
//_________ http://mobile.yugel.net/ ___________//
//______________________________________________//
//________ http://fb.com/yugelmobile/ __________//
//______________________________________________//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float rotateZSpeed = 100f;
    public GameObject[] AllFans;

    void Start()
    {
        foreach (var fan in AllFans)
        {
            StartCoroutine(RotateFan(fan));
        }
    }

    private IEnumerator RotateFan(GameObject fan)
    {
        while (true)
        {
            fan.transform.Rotate(Vector3.forward * rotateZSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }
    }
}