using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class HHG_OpenWorldManager : MonoBehaviour
{
    public Transform CarPostiom;
    public Transform TpsPosition;
    
    public static HHG_OpenWorldManager Instance;
    public bool isCutScene;
    public GameObject Timeline;
    public PlayableDirector Director;
  
    private void Awake()
    {
        Instance = this;
    }


  
    public void Start()
    {
        if (isCutScene)
        {
            Time.timeScale = 1f;
            Timeline.SetActive(true);
            Director.Play();
            Invoke("HideTimeline", (float)Director.duration - 0.9f);
            HHG_UiManager.instance.HideGamePlay();
        }
    }

    public void HideTimeline()
    {
        Timeline.SetActive(false);
       // HHG_LevelManager.instace.Tpscamera.GetComponent<Camera>().farClipPlane = getFar();
        HHG_UiManager.instance.ShowGamePlay();
       // LevelManager.instace.canvashud.gameObject.SetActive(true);
    }

    private int  getFar()
    {
        if (SystemInfo.systemMemorySize > 3500)
        {
            return 250;
        }
        else if (SystemInfo.systemMemorySize > 2500)
        {
            return 200;
        }
        else if (SystemInfo.systemMemorySize > 1200)
        {
            return 150;
        }
        else
        {
            return 100;
        }
    }
}



















    


