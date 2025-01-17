﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Activation;
using System.Threading.Tasks;
using GameAnalyticsSDK;
using HHG_Mediation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HHG_UiManager : MonoBehaviour
{
   
    public GameObject ObjectivePannel, Pause, Fail, Complete, controls,TpsControle, Loading,OutOfFuel,error;
    public Text ObjectiveText;
   
    public static HHG_UiManager instance;
    public int TotalLevels;
    public Image fuelBar;
    public GameObject Fadeimage,blankimage;
    public Image []NosFiller;
    public GameObject []NosButton; 
    public Text []NosCountText;
   
    public Text displayText,LapText;
    [Header("health WORK")] 
    public Text HealthText;
    public GameObject repairPanel ,Ripairebutton;
    public Image FillhealthBar;
    
    
    [Header("SS WORK")]
    public GameObject uiPanel;    // The UI Panel to show the captured picture
    public Image capturedImage;   // The Image component to display the captured picture
    public Button resumeButton;
    public Text rewradMoneyText;
    public Text[] SpeedCaputer;
    public GameObject ToSlowPanel;
    [Header("MissionWork")]
    public GameObject LoadingForMission,CheckpointShot,handBrake,Left,LeftForwadrace;
    public GameObject missionComplet,MissionWased,Map;
    public GameObject CheckPointBar, Racebar, LapBar,CarMobile,Getoutbutton,LapAlert;
    public GameObject ExitMissionBtn, ExitPanel,CallPanel;
    
    
    public GameObject AdButtonForranmp;
    public Text RanmpValuetext;
    public GameObject MssionPanel;
    public Text MissionStatment, RewardText, MissionTitle;
    public Image missionicon;
    public GameObject AdbrakenitySec;
    void Awake()
    {

        instance = this;
        HHG_SoundManager.Instance.PlayAudio(HHG_SoundManager.Instance.BgSound);
        Time.timeScale = 1f;
        HHG_LevelManager.instace.SelectedPlayer.GetComponent<VehicleProperties>().UpdateHealthText();
        FillhealthBar.color  = HHG_LevelManager.instace.colers[0];
        Ripairebutton.SetActive(false);
        uiPanel.SetActive(false);
        Invoke(nameof(offFadeimage),4f);
        Invoke(nameof(onnitysec),90f);
        
    }

    void onnitysec()
    {
        AdbrakenitySec.SetActive(true);
        Invoke(nameof(onnitysec),90f);
    }
   void offFadeimage()
    {
        Fadeimage.SetActive(false);
    }
    public void OnspeedCaputer()
    {
        ToSlowPanel.SetActive(true);
        ToSlowPanel.GetComponent<Animator>().Play("SpeedChacker");
        Invoke("OFFspeedCaputer",3.5f);
    }
    public void OFFspeedCaputer()
    {
        ToSlowPanel.SetActive(false);
    }
    void OnEnable()
    {
       
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showBanner1();
            FindObjectOfType<HHG_AdsCall>().showBanner2();
            FindObjectOfType<HHG_AdsCall>().hideBigBanner();
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
        AudioListener.volume =.7f;
      //  GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());

    }

    public void ShowObjective(string statment)
    {
        ObjectiveText.text = statment;
        //if (LevelManager.instace.FreeMode)
        if (PrefsManager.GetGameMode() == "free")
        {
           
            ObjectivePannel.SetActive(false);
            SetTimeScale(1);

           
        }
         else if(PrefsManager.GetLevelMode() == 0){
        ObjectivePannel.SetActive(true);
        SetTimeScale(0);
        HideGamePlay();
        }
        else if(PrefsManager.GetLevelMode() ==1){
        ObjectivePannel.SetActive(false);
            //MiniMap.GetComponent<CanvasGroup>().alpha = 0;

        }
    }
    public void FillFuelbar(float fillAmount) {
        fuelBar.fillAmount = fillAmount;
        if (fillAmount <= 0) {
            OutOfFuel.SetActive(true);
        }
    }
    public void FillFuelTank() {
        if (PrefsManager.GetCoinsValue() > 1000)
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() - 1000);
        else { 
            error.SetActive(true);
            Invoke("OffError",4f);
        }
     //   LevelManager.instace.SelectedPlayer.GetComponent<RCC_CarControllerV3>().FillFullTank();
        OutOfFuel.SetActive(false);
    }


    public void OffError() {
        error.SetActive(false);

    }

   public void FullTankWithVideo() { 
        //LevelManager.instace.SelectedPlayer.GetComponent<RCC_CarControllerV3>().FillFullTank();
        OutOfFuel.SetActive(false);

    }

   public void HideGamePlay()
   {
       HHG_GameControl.manager?.ResetJoystick();
       controls.GetComponent<CanvasGroup>().alpha=0;
       controls.SetActive(false);
       Map.SetActive(false);
       TpsControle.SetActive(false);
       HHG_LevelManager.instace.HUDNavigationCanvas.gameObject.SetActive(false);
       HHG_LevelManager.instace.coinBar.SetActive(false);
   }

   public void ShowGamePlay()
   {
       SetTimeScale(1);
       if (HHG_GameManager.Instance.TpsStatus==PlayerStatus.CarDriving)
       {
           controls.SetActive(true);
           controls.GetComponent<CanvasGroup>().alpha=1;
           TpsControle.SetActive(false);
       }
       else
       {
           controls.SetActive(false);
           TpsControle.SetActive(true);
          controls.GetComponent<CanvasGroup>().alpha=0;
         
       }
       Map.SetActive(true);
       HHG_LevelManager.instace.HUDNavigationCanvas.gameObject.SetActive(true);
       HHG_LevelManager.instace.coinBar.SetActive(true);
       HHG_GameControl.manager?.RestoreJoystick();
   }
    public void HideObjectivePannel()
    {
       // AdmobAdsManager.Instance.LoadInterstitialAd();
        ObjectivePannel.SetActive(false);
        SetTimeScale(1);
        ShowGamePlay();
       
    }

    public async void ShowMyAd()
    {
        FindObjectOfType<HHG_AdsCall>().hideBigBanner();
        AdBrakepanel.SetActive(true);
        await Task.Delay(1000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
			
            PrefsManager.SetInterInt(1);
        }
        AdBrakepanel.SetActive(false);
        await Task.Delay(2000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
    }
    public void ShowPause()
    {
        
       
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }
      //  AdmobAdsManager.Instance.ShowInt(ShowPauseNow,true);
      ShowPauseNow();
    }

    public async void ShowPauseNow()
    {
        AudioListener.volume = 0;
        Pause.SetActive(true);
        HideGamePlay();
        SetTimeScale(0);
        await Task.Delay(2000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
    }

    public void Resume()
    {
        SetTimeScale(1);
        Pause.SetActive(false);
        ShowGamePlay();
        AudioListener.volume = 1;
        //AdmobAdsManager.Instance.LoadInterstitialAd();
    }

    public void Restart()
    {
        AudioListener.volume = 0;
        if (HHG_LevelManager.instace.hhgOpenWorldManager.missionon)
        {
            HHG_LevelManager.instace.isPanelOn = true;
            HHG_LevelManager.instace.ResetTimer();
             RestartMission();
        }
        else
        {
            Time.timeScale = 1;
            HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
            Loading.SetActive(true);
            HideGamePlay();
            Loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("HHG_GamePlay");
            HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);

            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
            Invoke(nameof(showInterAd),5f);
       }
    }
    public GameObject AdBrakepanel;
    public async void showInterAd()
    {
        AdBrakepanel.SetActive(true);
        await Task.Delay(1000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
			
            PrefsManager.SetInterInt(1);
        }
        AdBrakepanel.SetActive(false);
    }

    public void Home()
    {
        AudioListener.volume = 0;
        Time.timeScale = 1;
        Loading.SetActive(true);
        HideGamePlay();
        Loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("HHG_MainMenu");
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        if (PrefsManager.GetInterInt()!=5)
        {
            FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
        }
        Invoke(nameof(showInterAd),5f);
    }

    public void LevelCompleteNow()
    {
        HHG_LevelManager.instace.isTrazitionok = false;  
        HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.gameObject.SetActive(false);
        HHG_LevelManager.instace.hhgOpenWorldManager.missionon = false;
        PrefsManager.SetCurrentMission(PrefsManager.GetCurrentMission() + 1);
        Complete.SetActive(true);
        // SetTimeScale(0);
        HideGamePlay();
        if (PrefsManager.GetGameMode() != "free")
        {

            if (PrefsManager.GetLevelMode() == 0)
            {
                Debug.Log("FirstMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetLevelLocking());
                if (PrefsManager.GetCurrentLevel() >= PrefsManager.GetLevelLocking())
                {
                    PrefsManager.SetLevelLocking(PrefsManager.GetLevelLocking() + 1);

                }
                Debug.Log("FirstMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetLevelLocking());
            }
            else if (PrefsManager.GetLevelMode() == 1)
            {
                Debug.Log("SnowMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetSnowLevelLocking());
                if ((PrefsManager.GetCurrentLevel()) >= PrefsManager.GetSnowLevelLocking())
                {
                    PrefsManager.SetSnowLevelLocking(PrefsManager.GetSnowLevelLocking() + 1);

                }
                Debug.Log("SnowMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetSnowLevelLocking());
            }
          //  GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, PrefsManager.GetGameMode(), PrefsManager.GetCurrentLevel());
            //  Data.SendCompleteEvent(PrefsManager.GetCurrentLevel());
           // Admob_LogHelper.MissionOrLevelCompletedEventLog(PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());

        }
        else { 
            //Data.SendCompleteEvent(20);

         //   Admob_LogHelper.MissionOrLevelCompletedEventLog("Free",0);

        }

    }

    public void ShowComplete()
    {
        missionComplet.SetActive(true);
        HHG_SoundManager.Instance?.PlayAudio(HHG_SoundManager.Instance.LevelComplete);
        Invoke(nameof(nowComplet), 3f);
    }

    void nowComplet()
    {
        missionComplet.SetActive(false);
        LevelCompleteNow();
    }
    public void ShowFail()
    {
        Invoke(nameof(callComplet), 3f);
        HHG_LevelManager.instace.isTrazitionok = true;      
        HHG_LevelManager.instace.rcc_camera.cameraMode = RCC_Camera.CameraMode.WHEEL;
        controls.GetComponent<CanvasGroup>().alpha=0;
        Map.SetActive(false);
        HHG_TimeController.Instance.StopTimer();
       
       
        Invoke(nameof(CallMessage), 3f);
        HHG_SoundManager.Instance.PlayAudio(HHG_SoundManager.Instance.levelFail);
        MissionWased.SetActive(true);
        
    }

    void callComplet()
    {
       HideGamePlay();
    }
  void  CallMessage()
    {
        MissionWased.SetActive(false);
        ShowLevelFailNow();
       
    }
    public void ShowLevelFailNow()
    {
        
        Fail.SetActive(true);
        SetTimeScale(1);
        HideGamePlay();
        HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.gameObject.SetActive(false);
        HHG_LevelManager.instace.isTrazitionok = false;
        HHG_LevelManager.instace.hhgOpenWorldManager.missionon = false;
        //  GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, PrefsManager.GetGameMode(), PrefsManager.GetCurrentLevel());
     //  Admob_LogHelper.MissionOrLevelFailEventLog(PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());
    }

    public async void RestartMission()
    {
        AdBrakepanel.SetActive(true);
        await Task.Delay(1000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
			
            PrefsManager.SetInterInt(1);
        }
        AdBrakepanel.SetActive(false);
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        Fail.SetActive(false);
        Complete.SetActive(false);
        HideGamePlay();
        LoadingForMission.SetActive(true);
        HHG_LevelManager.instace.rcc_camera.cameraMode = RCC_Camera.CameraMode.TPS;
        Left.GetComponent<RCC_UIController>().pressing = false;
        handBrake.GetComponent<RCC_UIController>().pressing = false;
        HHG_LevelManager.instace.isPanelOn = true;
        HHG_LevelManager.instace.ResetTimer();
        if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties)
        {
            CheckPointBar.SetActive(false);
            Racebar.SetActive(false);
            LapBar.SetActive(false);
        }    
        if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.IsRace)
        {
            HHG_GameManager.Instance.CurrentCar.GetComponent<CarSpriteController>().spriteRenderer.gameObject.SetActive(false);
            HHG_GameManager.Instance.CurrentCar.GetComponent<CarSpriteController>().enabled = false;
        }
        await Task.Delay(2000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
    }
    public async void Continue()
    {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        HHG_SoundManager.Instance.PlayAudio(HHG_SoundManager.Instance.BgSound);
        AdBrakepanel.SetActive(true);
        await Task.Delay(1000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();

            PrefsManager.SetInterInt(1);
        }
        ExitMissionBtn.SetActive(false);
        AdBrakepanel.SetActive(false);
        Fail.SetActive(false);
        Complete.SetActive(false);
        HHG_LevelManager.instace.rcc_camera.cameraMode = RCC_Camera.CameraMode.TPS;
        Left.GetComponent<RCC_UIController>().pressing = false;
        handBrake.GetComponent<RCC_UIController>().pressing = false;
        ShowGamePlay();
        HHG_LevelManager.instace.isPanelOn = false;
        HHG_LevelManager.instace.ResetTimer();
        CarMobile.SetActive(true);
        Getoutbutton.SetActive(true);
        HHG_GameManager.Instance.TrafficSpawn.GetComponent<TSTrafficSpawner>().trafficCarsParent.gameObject.SetActive(true);
        if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties)
        {
           CheckPointBar.SetActive(false);
           Racebar.SetActive(false);
           LapBar.SetActive(false);
         
        }    
        if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.IsRace)
        {
            HHG_GameManager.Instance.CurrentCar.GetComponent<CarSpriteController>().spriteRenderer.gameObject.SetActive(false);
            HHG_GameManager.Instance.CurrentCar.GetComponent<CarSpriteController>().enabled = false;
        }
        foreach (var speed in HHG_GameManager.Instance.SpeedCheck)
        {
            speed.gameObject.SetActive(true);
        }
        await Task.Delay(2000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
    }

    public void OpenExitMission()
    {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        ExitPanel.SetActive(true);
        HideGamePlay();
        Time.timeScale = 0;
    }
    public void CloseExitMission()
    {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        ExitPanel.SetActive(false);
        ShowGamePlay();
        Time.timeScale = 1;
    }
    public async void ExitMission()
    {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        AdBrakepanel.SetActive(true);
        await Task.Delay(1000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();

            PrefsManager.SetInterInt(1);
        }
        ExitPanel.SetActive(false);
        AdBrakepanel.SetActive(false);
        ExitMissionBtn.SetActive(false);
        HHG_TimeController.Instance.StopTimer();
        Time.timeScale = 1;
        ShowGamePlay();
        HHG_LevelManager.instace.isPanelOn = false;
        HHG_LevelManager.instace.ResetTimer();
        CarMobile.SetActive(true);
        Getoutbutton.SetActive(true);
        HHG_LevelManager.instace.hhgOpenWorldManager.missionon = false;
        HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.gameObject.SetActive(false);
        if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties)
        {
            CheckPointBar.SetActive(false);
            Racebar.SetActive(false);
            LapBar.SetActive(false);
        }    
        if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.IsRace)
        {
            HHG_GameManager.Instance.CurrentCar.GetComponent<CarSpriteController>().spriteRenderer.gameObject.SetActive(false);
            HHG_GameManager.Instance.CurrentCar.GetComponent<CarSpriteController>().enabled = false;
        }
        foreach (var speed in HHG_GameManager.Instance.SpeedCheck)
        {
            speed.gameObject.SetActive(true);
        }
        await Task.Delay(2000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
    }
    public void Next()
    {
        PrefsManager.SetCurrentLevel(PrefsManager.GetCurrentLevel()+1);
        Loading.SetActive(true);
        if (PrefsManager.GetCurrentLevel() >= TotalLevels)
        { 
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex-1);

        }
        else
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

    }
    public void SetTimeScale(float timescale)
    {

        Time.timeScale = timescale;
    }


}
