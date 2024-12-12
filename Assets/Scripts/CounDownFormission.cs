using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class CounDownFormission : MonoBehaviour
{
    private float currentime = 0f;
    private float startingtime = 7f;

    [SerializeField] private Text countdountext;
     public Text MissionStament;
    // Start is called before the first frame update
    void OnEnable()
    {
        currentime = startingtime;
        MissionStament.text = GetCurrentMissionStatmentk();
      
    }
    public string GetCurrentMissionStatmentk()
    {
        return  HHG_LevelManager.instace.hhgOpenWorldManager.AllMission[PrefsManager.GetCurrentMission()].GetComponent<_LevelProperties>().LevelStatment; 
    }

    // Update is called once per frame
    void Update()
    {
        currentime -= Time.deltaTime;
        countdountext.text = currentime.ToString("Auto Accept in (0) Sec");
        if (currentime <= 0)
        {
            currentime = 0;
            HHG_LevelManager.instace.Mobilemanger?.AcceptCall();
        }
    }

    public void statemanent(string statment)
    {
      //  UiManagerObject_EG.instance.ObjectiveText.text = statment;
    }
    
}
