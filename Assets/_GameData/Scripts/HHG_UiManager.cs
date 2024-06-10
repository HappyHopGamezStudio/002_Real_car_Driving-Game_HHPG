using System.Collections;
using System.Collections.Generic;
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
    public GameObject /*EffectForcoin*/blankimage;
    public Image []NosFiller;
    public GameObject []NosButton; 
    public Text []NosCountText;
    [Header("Settingpart")]
    public Slider volume_value;
    public Text PercentageText;

    private bool isStart=false;
    public GameObject Low, High, Med;
    public GameObject musicbuttonON, musicebuttonOff, SounButtonON, SoundButtonOFF;
    
    
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
    
    void Awake()
    {

        instance = this;
        HHG_SoundManager.Instance.PlayAudio(HHG_SoundManager.Instance.BgSound);
        Time.timeScale = 1f;
        HHG_LevelManager.instace.SelectedPlayer.GetComponent<VehicleProperties>().UpdateHealthText();
        FillhealthBar.color  = HHG_LevelManager.instace.SelectedPlayer.GetComponent<VehicleProperties>().colers[0];
        Ripairebutton.SetActive(false);
        uiPanel.SetActive(false);
    }

    public void OnspeedCaputer()
    {
        ToSlowPanel.SetActive(true);
        Invoke("OFFspeedCaputer",3f);
    }
    public void OFFspeedCaputer()
    {
        ToSlowPanel.SetActive(false);
    }
    void Start()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());
        if (FindObjectOfType<HHG_AdsCall>())
       {
           FindObjectOfType<HHG_AdsCall>().showBanner1();
           FindObjectOfType<HHG_AdsCall>().showBanner2();
           FindObjectOfType<HHG_AdsCall>().hideBigBanner();
           if (PrefsManager.GetInterInt()!=5)
           {
               FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
           }
       }
       
       
       if (PrefsManager.GetMusic()==0)
       {
           musicebuttonOff.SetActive(true);
           musicbuttonON.SetActive(false);
       }
       else
       {
           musicebuttonOff.SetActive(false);
           musicbuttonON.SetActive(true);
       }
       if (PrefsManager.GetSound() == 0)
       {
           SoundButtonOFF.SetActive(true);
           SounButtonON.SetActive(false);
       }
       else
       {
           SoundButtonOFF.SetActive(false);
           SounButtonON.SetActive(true);
       }
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
    public void SetGraphicQuality(int value) {
        PrefsManager.SetGameQuality(value);
        switch (value) { 
            
            case 0:
               
                    
                Low.SetActive(false);
                Med.SetActive(false);
                High.SetActive(true);
                QualitySettings.SetQualityLevel(0);
              
                break;

            case  1:
                Low.SetActive(false);
                Med.SetActive(true);
                High.SetActive(false);
               
                QualitySettings.SetQualityLevel(1);
                break;

            case 2:
                Low.SetActive(true);
                Med.SetActive(false);
                High.SetActive(false);
                    
                QualitySettings.SetQualityLevel(2);
               
                break;
        }
       
     
        if (!isStart)
            ClickSound();
    }
    public void ClickSound() {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);

    }

    public void MuteEvent ()
    {
        GameObject.FindGameObjectWithTag ("SoundManager").transform.GetChild (0).gameObject.GetComponent<AudioSource> ().enabled = false;
        PrefsManager.SetSound (0);
    }

    public void UnMuteEvent ()
    {
		
        GameObject.FindGameObjectWithTag ("SoundManager").transform.GetChild (0).gameObject.GetComponent<AudioSource> ().enabled = true;
        HHG_SoundManager.Instance.PlayOneShotSounds (HHG_SoundManager.Instance.click);
        PrefsManager.SetSound (1);
    }

    public void OFFmusic()
    {
        GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<AudioSource> ().enabled = true;
        PrefsManager.SetMusic(1);;
    }
    public void ONmusic()
    {
        GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<AudioSource> ().enabled = false;
        PrefsManager.SetMusic(0);
    }
    private void Update()
    {

        if (volume_value != null)
        {
            volume_value.onValueChanged.AddListener(SettingVolume);
            SettingVolume(volume_value.value);
        }
    }

    public  void SettingVolume(float value)
    {
        // Calculate the percentage
        int percentage = Mathf.RoundToInt(value * 100);
        
        // Update the text
        PercentageText.text = percentage + "%";
        PrefsManager.SetSound(volume_value.value);
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
        controls.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void ShowGamePlay()
    {
        controls.GetComponent<CanvasGroup>().alpha = 1;

    }
    public void HideObjectivePannel()
    {
       // AdmobAdsManager.Instance.LoadInterstitialAd();
        ObjectivePannel.SetActive(false);
        SetTimeScale(1);
        ShowGamePlay();
       
    }

    public void ShowPause()
    {
        
       
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
      //  AdmobAdsManager.Instance.ShowInt(ShowPauseNow,true);
      ShowPauseNow();
    }

    public async void ShowPauseNow()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }
        Pause.SetActive(true);
        HHG_LevelManager.instace.rcc_camera.GetComponentInChildren<Camera>().GetComponent<AudioListener>().enabled = false;
        SetTimeScale(0);
        HideGamePlay();
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
        Pause.SetActive(false);
        SetTimeScale(1);
        ShowGamePlay();
        HHG_LevelManager.instace.rcc_camera.GetComponentInChildren<Camera>().GetComponent<AudioListener>().enabled = true;
        //AdmobAdsManager.Instance.LoadInterstitialAd();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        Loading.SetActive(true);
        Loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("HHG_GamePlay");
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);

        if (PrefsManager.GetInterInt()!=5)
        {
            FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
        }
        Invoke(nameof(showInterAd),5f);
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
        Time.timeScale = 1;
        Loading.SetActive(true);
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
   //   AdmobAdsManager.Instance.ShowInt(LevelCompleteNow,true);
      LevelCompleteNow();
        HHG_SoundManager.Instance?.PlayAudio(HHG_SoundManager.Instance.LevelComplete);

    }

    public void ShowFail()
    {
       
        HHG_SoundManager.Instance.PlayAudio(HHG_SoundManager.Instance.levelFail);
      //  Data.SendFailEvent(PrefsManager.GetCurrentLevel());
          //AdmobAdsManager.Instance.ShowInt(ShowLevelFailNow,true);
          ShowLevelFailNow();
    }

    public void ShowLevelFailNow()
    {
        
        Fail.SetActive(true);
        SetTimeScale(0);
        HideGamePlay();
      //  GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, PrefsManager.GetGameMode(), PrefsManager.GetCurrentLevel());
     //  Admob_LogHelper.MissionOrLevelFailEventLog(PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());
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
