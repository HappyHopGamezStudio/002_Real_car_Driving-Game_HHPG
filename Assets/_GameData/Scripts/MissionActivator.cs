using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionActivator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Mission;
    public float timeCheck = 3f;
    public Transform Player;
    public float Distance,DistanceToShowMission;


    // Update is called once per frame
    void Update()
    {
        timeCheck -= Time.deltaTime;
        if (timeCheck > 0)
            return;
        timeCheck = 3f;
      
        if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.ThirdPerson)
        {
            Player = HHG_GameManager.Instance.TPSPlayer.transform;
        }
        else if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.CarDriving)
        {
            Player = HHG_GameManager.Instance.CurrentCar.transform;
        }
        
        
        Distance = Vector3.Distance(transform.position, Player.position);
        if (Distance < DistanceToShowMission && !Mission.activeSelf)
        {
            Mission.SetActive(true);
        }
        else if (Distance >= DistanceToShowMission && Mission.activeSelf)
        {
            Mission.SetActive(false);
        }

    }

    public bool GetChild = false;
    private void OnDrawGizmosSelected()
    {
        if (GetChild)
        {
            GetChild = false;
            Mission = transform.GetChild(0).gameObject;
            Mission.SetActive(false);
            transform.name = Mission.gameObject.name + "_Mission";
        }
    }
}
