using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class HHG_OpenWorldManager : MonoBehaviour
{
   
    public Transform carPostion;
    public Transform TpsPosition;

    public _LevelProperties[] AllMission;

    public _LevelProperties CurrentMissionProperties;
    public static HHG_OpenWorldManager Instance;
   
    public bool IsComplte;
    public int TotalMisson;
   
    private void Awake()
    {
        Instance = this;
    }


    public string GetCurrentMissionStatmentk()
    {
        return AllMission[PrefsManager.GetCurrentMission()].GetComponent<_LevelProperties>().LevelStatment; 
    }


    

    public bool missionon = false;

    public void AcceptMissionformission()
    {
        foreach (var Mission in AllMission)
        {
            Mission.gameObject.SetActive(false);
        }

        CurrentMissionProperties = AllMission[PrefsManager.GetCurrentMission()];
        CurrentMissionProperties.gameObject.SetActive(true);
        missionon = true;
        HHG_LevelManager.instace.SetTransform(CurrentMissionProperties.PlayerPosition,
            CurrentMissionProperties.TpsPosition);
        setcarok();

    }

    public void setcarok()
    {
        if (HHG_GameManager.Instance.CurrentCar!=null)
        { 
            HHG_GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().velocity=Vector3.zero; 
            HHG_GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().angularVelocity=Vector3.zero; 
        }
        else 
        {
            return;
        }
    }



    private void OnEnable()
    {
        if (CurrentMissionProperties==null)
        {
            return;
        }
    }
}



















    


