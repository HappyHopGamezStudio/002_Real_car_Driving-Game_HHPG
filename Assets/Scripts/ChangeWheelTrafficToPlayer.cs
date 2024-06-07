using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWheelTrafficToPlayer : MonoBehaviour
{

    public GameObject TrafficCarCollider,PlayerCarCollider,PlayerCarWheel;
    public GameObject TrafficCarBody, PlayerCarBody;

    public void ChangeToAI()
    {
        TrafficCarCollider.SetActive(true);
        TrafficCarBody.SetActive(true);
        PlayerCarBody.SetActive(false);
        PlayerCarCollider.SetActive(false);
        PlayerCarWheel.SetActive(false);
    }


    public void ChangeToPlayer()
    {
        TrafficCarCollider.SetActive(false);
        TrafficCarBody.SetActive(false);
        PlayerCarBody.SetActive(true);
        PlayerCarCollider.SetActive(true);
        PlayerCarWheel.SetActive(true);
    }
}
