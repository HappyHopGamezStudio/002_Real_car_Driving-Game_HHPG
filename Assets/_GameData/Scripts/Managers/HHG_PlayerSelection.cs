using System;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using HHG_Mediation;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class Specification
{
    public int[] Values;
}

public class HHG_PlayerSelection : MonoBehaviour
{
    public string[] CarNames;
    public Text[] spacificationpaercetage;
    public Text CarName;
    public Specification[] SpecificationValue;
    public GameObject[] sp_bar;
    public int[] Prices;
    int selectedPlayerValue = 0;
    public GameObject[] selectedDogArray;
    public Button[] CarButtons;

    public GameObject nextBtn,
        backBtn,
        dogSelectionCanvas,
        menuCanvas,
        levelSelectionCanvas,
        lockSprite,
        LOADING,
        Play,
        notCash,
        successPannel,
        unlockPlayerButton,
        TestDriveButton,
        MainNextBack,
        SkipButton;

    public Text coinText, coinText2, ls_cointext, PriceText;
    bool isReadyForPurchase;
    int ActivePlayervalue = 1;
    int coinValue;
    public static HHG_PlayerSelection instance;
    public GameObject fakeLoading;



    [Header("CarSoldCutSSceane")] public GameObject Timeline;
    public PlayableDirector Director;
    public GameObject CutSceanCamera;
    
    public GameObject MainCamera;
    public bool IsGrage = false;
    private float savedValue;


    void Awake()
    {
        instance = this;
        Time.timeScale = 1;

       // CarButtons[selectedPlayerValue].transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        MainCamera.GetComponent<CameraRotate>().SetMen();
    }

    public void OnNextPressed()
    {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        if (selectedPlayerValue < selectedDogArray.Length - 1)
        {
            selectedPlayerValue++;
            ShowPlayerNow(selectedPlayerValue);
            CutSceanCamera.GetComponent<LookAtTargetSetter>().SetTarget(CurrentPlayer);
            MainCamera.GetComponent<CameraRotate>().ResetTimer();
           // EffectinGrage.Play();
        }
    }

    public void OnBackPressed()
    {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        if (selectedPlayerValue > 0)
        {
            selectedPlayerValue--;
            ShowPlayerNow(selectedPlayerValue);
            CutSceanCamera.GetComponent<LookAtTargetSetter>().SetTarget(CurrentPlayer);

            MainCamera.GetComponent<CameraRotate>().ResetTimer();
           // EffectinGrage.Play();
        }

    }
    public void OnButtonClick(int selectedPlayerValue)
    {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        if (selectedPlayerValue >= 0 && selectedPlayerValue < selectedDogArray.Length)
        {
            for (int i = 0; i < CarButtons.Length; i++)
            {
                CarButtons[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            CarButtons[selectedPlayerValue].transform.GetChild(0).gameObject.SetActive(true);
            ShowPlayerNow(selectedPlayerValue);
           
        }
        else
        {
            Debug.LogError("Player index out of bounds");
        }
    }

   public GameObject CurrentPlayer = null;

    public void ShowPlayerNow(int val)
    {

        ActivePlayervalue = val;
        PriceText.text = Prices[val] + "";
        if (PrefsManager.GetPlayerState(val) == 0 && val != 0)
        {
            PriceText.transform.parent.gameObject.SetActive(true);
            lockSprite.SetActive(true);
            Play.SetActive(false);
            unlockPlayerButton.SetActive(true);
            TestDriveButton.SetActive(true);
            for (int i = 0; i < CarButtons.Length; i++)
            {
                if (PrefsManager.GetPlayerState(i) == 1)
                {
                    CarButtons[i].transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
        else
        {
            lockSprite.SetActive(false);
            PriceText.transform.parent.gameObject.SetActive(false);
            
            Play.SetActive(true);
            unlockPlayerButton.SetActive(false);
            TestDriveButton.SetActive(false);
            for (int i = 0; i < CarButtons.Length; i++)
            {
                if (PrefsManager.GetPlayerState(i) == 1)
                {
                    CarButtons[i].transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }

        for (int i = 0; i < selectedDogArray.Length; i++)
        {
            selectedDogArray[i].SetActive(false);
        }

        PrefsManager.SetSelectedPlayerValue(val);
        CurrentPlayer = selectedDogArray[val];
        CurrentPlayer.SetActive(true);
        for (int i = 0; i < sp_bar.Length; i++)
        {
            sp_bar[i].GetComponent<HHG_Filled>().SetEndpoint(SpecificationValue[val].Values[i]);
            spacificationpaercetage[i].text = SpecificationValue[val].Values[i] + "%";
        }

        CarName.text = CarNames[val];
        if (ActivePlayervalue == selectedDogArray.Length - 1)
        {
            nextBtn.SetActive(false);
            backBtn.SetActive(true);
        }
        else if (ActivePlayervalue == 0)
        {
            nextBtn.SetActive(true);
            backBtn.SetActive(false);
        }
        else
        {
            nextBtn.SetActive(true);
            backBtn.SetActive(true);
        }

        selectedPlayerValue = ActivePlayervalue;
        MainCamera.GetComponent<CameraRotate>().SetMianPos();
    }

    public void TestDrive()
    {
        lockSprite.SetActive(false);
        PriceText.transform.parent.gameObject.SetActive(false);
        Play.SetActive(true);
        unlockPlayerButton.SetActive(false);
        TestDriveButton.SetActive(false);
        PrefsManager.SetSelectedPlayerValue(selectedPlayerValue);
        SelectDogPlay();
    }

    public void Ps_PlayEvent()
    {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        //AdmobAdsManager.Instance.ShowInt(GoToLevelSelect, false);
        GoToLevelSelect();
    }

    public void GoToLevelSelect()
    {
        // CurrentEnv.SetActive(false);
        for (int i = 0; i < selectedDogArray.Length; i++)
        {
           // selectedDogArray[i].SetActive(false);
        }

        ls_cointext.text = PrefsManager.GetCoinsValue().ToString();
        SelectDogPlay();
    }


    public void SelectDogPlay()
    {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        fakeLoading.SetActive(true);
        levelSelectionCanvas.SetActive(true);
    }

    public void ForTutorialPlay()
    {
        levelSelectionCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        Debug.Log("Enable Here");
        LOADING.SetActive(true);
        PrefsManager.SetGameMode("challange");
        PrefsManager.SetCurrentLevel(1);
        PrefsManager.SetLevelMode(0);

    }
    public void RedirectToDogSelection()
    {
        //  AdmobAdsManager.Instance.LoadInterstitialAd();
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
     //   fakeLoading.SetActive(true);
       MainCamera.GetComponent<CameraRotate>().SetMianPos();
        dogSelectionCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        ShowPlayerNow(PrefsManager.GetLastJeepUnlock());
        MainNextBack.SetActive(true);
        selectedPlayerValue = PrefsManager.GetLastJeepUnlock();
        for (int i = 0; i < CarButtons.Length; i++)
        {
            if (PrefsManager.GetPlayerState(i) == 1)
            {
                CarButtons[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        IsGrage = true;
    }

    public void BackToMainCanvas()
    {
        //  AdmobAdsManager.Instance.LoadInterstitialAd();
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        ShowPlayerNow(PrefsManager.GetLastJeepUnlock());
        MainCamera.GetComponent<CameraRotate>().SetMianPos();
      //  fakeLoading.SetActive(true);
        dogSelectionCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        Debug.Log("Enable Here");
       
        IsGrage = false;
    }

    public void BackFromLevelScreen()
    {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        fakeLoading.SetActive(true);
        levelSelectionCanvas.SetActive(false);
        dogSelectionCanvas.SetActive(true);
        ShowPlayerNow(PrefsManager.GetSelectedPlayerValue());
        MainNextBack.SetActive(true);
        selectedPlayerValue = PrefsManager.GetSelectedPlayerValue();
    }

    public void SetLevelValue(int lValue)
    {
        //AdmobAdsManager.Instance.ShowInt(LoadGamePlay,false);
        LoadGamePlay();
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        PrefsManager.SetCurrentLevel(lValue);
    }

    
    
    
    public void LoadGamePlay()
    {
        LOADING.SetActive(true);
        LOADING.GetComponentInChildren<bl_SceneLoader>().LoadLevel("HHG_GamePlay");
        Debug.Log("Called it");
    }

    public void UnlockSelectedDog()
    {
        UnlockDog(Prices[ActivePlayervalue]);
    }

    public void UnlockDog(int dogVal)
    {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        if (PrefsManager.GetCoinsValue() >= dogVal)
        {
            PrefsManager.SetPlayerState(ActivePlayervalue, 1);
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() - dogVal);
            Success_purchase();
            PrefsManager.SetLastJeepUnlock(ActivePlayervalue);
            Play.SetActive(true);
            unlockPlayerButton.SetActive(false);
            TestDriveButton.SetActive(false);
            CarButtons[ActivePlayervalue].transform.GetChild(2).GetComponent<Image>().gameObject.SetActive(false);
        }
        else
        {
            notCash.SetActive(true);
        }
    }

    public void OffNoCash()
    {
        notCash.SetActive(false);
    }

    public void Offsuccess()
    {
        successPannel.SetActive(false);
        stoppanel=false;
    }
    
    public void Success_purchase()
    {
        Time.timeScale = 1f;
        if (IsGrage)
        {
            dogSelectionCanvas.gameObject.SetActive(false);
        }
        else
        {
            menuCanvas.gameObject.SetActive(false);
        }
     
        CutSceanCamera.SetActive(true);
        MainCamera.SetActive(false);
        SkipButton.SetActive(false);
        Timeline.SetActive(true);
        Director.Play();
        stoppanel=true;
        Invoke("HideTimeline", (float)Director.duration - 0.9f);
        GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<AudioSource> ().enabled = false;
        GameObject.FindGameObjectWithTag ("SoundManager").transform.GetChild (0).gameObject.GetComponent<AudioSource> ().enabled = false;
    }



    public async void PlayCutScne()
    {
        Time.timeScale = 1f;
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
			
            PrefsManager.SetInterInt(1);
        }
        if (IsGrage)
        {
            dogSelectionCanvas.gameObject.SetActive(false);
        }
        else
        {
            menuCanvas.gameObject.SetActive(false);
        }
        CutSceanCamera.SetActive(true);
        MainCamera.SetActive(false);
        Timeline.SetActive(true);
        Director.Play();
        GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<AudioSource> ().enabled = false;
        GameObject.FindGameObjectWithTag ("SoundManager").transform.GetChild (0).gameObject.GetComponent<AudioSource> ().enabled = false;
        Invoke("StopCutScene", (float)Director.duration - 0.9f);
        await Task.Delay(1000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
    }

    public async void StopCutScene()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
			
            PrefsManager.SetInterInt(1);
        }
        if (IsGrage)
        {
            dogSelectionCanvas.gameObject.SetActive(true);
        }
        else
        {
            menuCanvas.gameObject.SetActive(true);
        }
        CutSceanCamera.SetActive(false);
        MainCamera.SetActive(true);
        Timeline.SetActive(false);
        GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<AudioSource> ().enabled = true;
        GameObject.FindGameObjectWithTag ("SoundManager").transform.GetChild (0).gameObject.GetComponent<AudioSource> ().enabled = true;
        await Task.Delay(1000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
    }

    public bool stoppanel=false;

    public void HideTimeline()
    {
        if (IsGrage)
        {
            dogSelectionCanvas.gameObject.SetActive(true);
        }
        else
        {
            menuCanvas.gameObject.SetActive(true);
        }

        if (stoppanel)
        {
            successPannel.SetActive(true);
        }
        else
        {
            successPannel.SetActive(false);
        }

        CutSceanCamera.SetActive(false);
        MainCamera.SetActive(true);
        Timeline.SetActive(false);
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>().enabled = true;
        GameObject.FindGameObjectWithTag("SoundManager").transform.GetChild(0).gameObject.GetComponent<AudioSource>()
            .enabled = true;
        Invoke("Offsuccess", 3f);
        lockSprite.SetActive(false);
        unlockPlayerButton.SetActive(false);
        Play.SetActive(true);
        SkipButton.SetActive(true);
        PriceText.transform.parent.gameObject.SetActive(false);
    }
}
