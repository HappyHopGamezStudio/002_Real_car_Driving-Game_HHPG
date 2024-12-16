using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HHG_Mediation;
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


    public async void StartMission(int Value)
    {
      
        HHG_UiManager.instance.AdBrakepanel.SetActive(true);
        await Task.Delay(1000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
			
            PrefsManager.SetInterInt(1);
        }
        HHG_UiManager.instance.AdBrakepanel.SetActive(false);   
        PrefsManager.SetCurrentMission(Value);
        HHG_LevelManager.instace.Mobilemanger.AcceptCall();
        HHG_LevelManager.instace.isPanelOn=true;
        HHG_LevelManager.instace.ResetTimer();
        await Task.Delay(2000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
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
        HHG_LevelManager.instace.SetTransform(CurrentMissionProperties.PlayerPosition, CurrentMissionProperties.TpsPosition);
        setcarok();
        HHG_SoundManager.Instance.PlayAudio(HHG_SoundManager.Instance.BgSound);
    }

    public void setcarok()
    {
        
        if (HHG_GameManager.Instance.TpsStatus==PlayerStatus.CarDriving)
        {
            HHG_GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().velocity=Vector3.zero; 
            HHG_GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().angularVelocity=Vector3.zero; 
        }
        else 
        {
           HHG_LevelManager.instace.SelectedPlayer.GetComponent<Rigidbody>().velocity=Vector3.zero; 
           HHG_LevelManager.instace.SelectedPlayer.GetComponent<Rigidbody>().angularVelocity=Vector3.zero; 
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



















    


