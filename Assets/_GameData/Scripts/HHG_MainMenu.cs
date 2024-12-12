using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using HHG_Mediation;
using UnityEngine.SceneManagement;
using NiobiumStudios;

public class HHG_MainMenu : MonoBehaviour
{
	public GameObject ExitDialog, settingPannel, Modes,adclose, rewarded;
    public Slider volume_value;
    public Text PercentageText;
    public Scrollbar bar;
    public Text CoinText,CoinOnSpin;
    public static HHG_MainMenu instance;

    public GameObject lMark, MMark, HMark;

    public GameObject arrow,tild,steering;
 
    public GameObject ModeSelection;
    private bool isStart=false;
    
    public GameObject musicbuttonON, musicebuttonOff, SounButtonON, SoundButtonOFF;

    // Use this for initialization
    void Start ()
	{
      
        instance = this;
		HHG_SoundManager.Instance.PlayAudio(HHG_SoundManager.Instance.menuSound);


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
        
        
        
        GameObject.FindGameObjectWithTag ("SoundManager").transform.GetChild (0).gameObject.GetComponent<AudioSource> ().enabled = true;
        GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<AudioSource> ().enabled = true;
        
        
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
    
       

        AudioListener.volume = PrefsManager.GetMusic();
        volume_value.value = PrefsManager.GetMusic();

        Time.timeScale = 1;
       
       
       

        isStart = true;
        currentNumberQuality = PrefsManager.GetGameQuality();
        QualityClick(currentNumberQuality);
      //  SetGraphicQuality(PrefsManager.GetGameQuality());

        currentNumber = PrefsManager.GetControlls();


        SetControlls(currentNumber);

        ShowPrivacyDialog();

        if (PrefsManager.GetComeForModeSelection() == 1)
        {
            ModeSelection.SetActive(true);
            PrefsManager.SetComeForModeSelection(0);
        }

      
        //UnlockModes();
    }

    /// <summary>
    /// //////Handle PrivacyPolicy Dialog
    /// </summary>
    /// 
    public void ShowPrivacyDialog() {
      //  Debug.Log("Privacy Policy is "+PrefsManager.GetPrivacyPolicy());
        //if (PrefsManager.GetPrivacyPolicy() == 0)
        //{
        //    PrivacyDialog.SetActive(true);
        //}
        //else {
        //    PrivacyDialog.SetActive(false);
        //}
    }

	public void Event_Privacy()
	{
		HHG_SoundManager.Instance.PlayAudio(HHG_SoundManager.Instance.menuSound);
		//Debug.Log("Privacy Policy is " + PrefsManager.GetPrivacyPolicy());
		//if (PrefsManager.GetPrivacyPolicy() == 0)
		//{
		//	PrivacyDialog.SetActive(true);
		//	PrivacyDialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
		//	PrivacyDialog.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
		//}
		//else
		//{
		//	PrivacyDialog.SetActive(true);
		//	PrivacyDialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
		//	PrivacyDialog.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
		//}
	}
	public void Event_VisitWebsite()
	{
		HHG_SoundManager.Instance.PlayAudio(HHG_SoundManager.Instance.menuSound);
		//Application.OpenURL("https://gamefitstudio.blogspot.com/2019/06/the-gamefit-will-strictly-comply-with.html");

	}

		public void AcceptPolicy()
    {

        PrefsManager.SetPrivacyPolicy(1);
       
        //PrivacyDialog.SetActive(false);
        //if (PrefsManager.GetFirstTimeGame() == 0)
        //{
        //    dailyreward.SetActive(true);
        //    PrefsManager.SetFirstTimeGame(1);
        //} 

    }
    public void PlayOneShot()
    {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
    }

    public void PlayButtonClick()
    {

        PlayOneShot();
        ModeSelection.SetActive(true);

    }

    public void ShowAdd()
    {
        //AdmobAdsManager.Instance.ShowInt(ShowAddNow,false);
        ShowAddNow();
    }

    public void ShowAddNow()
    {
        
    }






    /// <summary>
    /// /////////main menu 
    /// </summary>




    public void QuitBtnEvent ()
	{
		ExitDialog.SetActive (true);

		HHG_SoundManager.Instance.PlayOneShotSounds (HHG_SoundManager.Instance.click);

	}

	public void Yes(){
		Application.Quit ();
	}
	public void No(){
		ExitDialog.SetActive (false);
		HHG_SoundManager.Instance.PlayOneShotSounds (HHG_SoundManager.Instance.click);

	}

	public void MoreGamesEvent ()
	{
		HHG_SoundManager.Instance.PlayOneShotSounds (HHG_SoundManager.Instance.click);
		Application.OpenURL("https://play.google.com/store/apps/developer?id=Happie+Hop+Gamez");
    }

	public void RateUsButtonEvent ()
	{
		HHG_SoundManager.Instance.PlayOneShotSounds (HHG_SoundManager.Instance.click);
		Application.OpenURL ("https://play.google.com/store/apps/details?id=" + Application.identifier);
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
	public void ShowSettingsPanel ()
	{
		settingPannel.SetActive (true);
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
    }

    public void BackfromSettingPannel()
    {
        settingPannel.SetActive(false);
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
    }

    public void ShowModeSelection() {
        Modes.SetActive(true);
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
       // UnlockModes();
    }

    public void SettingVolume(Slider volumeSlider)
    {
        PercentageText.text = volumeSlider.value + "";
        PrefsManager.SetSound(volume_value.value);
        AudioListener.volume = volumeSlider.value / 100f;
    }

    public void ClickSound() {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);

    }



    int currentNumberQuality = 0;
    public void QualityClick (int number)
    {

        currentNumberQuality += number;

        if (currentNumberQuality < 0)
            currentNumberQuality = 2;
        if (currentNumberQuality > 2)
            currentNumberQuality = 0;
        SetGraphicQuality(currentNumberQuality);
    }


    public void SetGraphicQuality(int value) {
        PrefsManager.SetGameQuality(value);
        switch (value) { 
        
        
        
           case 0:
               
                    
                    lMark.SetActive(false);
                    MMark.SetActive(false);
                    HMark.SetActive(true);
                    QualitySettings.SetQualityLevel(0);
              
                break;

            case  1:
                lMark.SetActive(false);
                MMark.SetActive(true);
                HMark.SetActive(false);
               
                QualitySettings.SetQualityLevel(1);
                break;

            case 2:
                lMark.SetActive(true);
                MMark.SetActive(false);
                HMark.SetActive(false);
                    
                QualitySettings.SetQualityLevel(2);
               
                break;
        }
       
     
        if (!isStart)
            ClickSound();
    }

    public void CloseAds(GameObject objectHide) {
        objectHide.SetActive(false);
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
    }


    public void RewardedToUser()
    {

        rewarded.transform.GetChild(0).GetComponent<Text>().text = "You are rewarded 50 Coins.";
        rewarded.SetActive(true);
        PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 50);

        Invoke("offitagain", 2f);
    }


    public void offitagain()
    {
        rewarded.SetActive(false);
    }


    public void ButtonSound() {

        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);

    }


    int currentNumber=0;
    public void ControlClick(int number) {

        currentNumber += number;

        if (currentNumber < 0)
            currentNumber = 2;
        if (currentNumber > 2)
            currentNumber = 0;
        SetControlls(currentNumber);
    }


    public void SetControlls(int value)
    {
        if (!isStart)
            ClickSound();
        if (value == 0)
        {
            arrow.SetActive(true);
            steering.SetActive(false);
            tild.SetActive(false);
        }
        if (value == 1)
        {
            arrow.SetActive(false);
            steering.SetActive(true);
            tild.SetActive(false);
        }

        if (value == 2)
        {
            arrow.SetActive(false);
            steering.SetActive(false);
            tild.SetActive(true);
        }

        isStart = false;
        PrefsManager.SetControlls(value);

    }

  
    

  

    private void OnEnable()
    {

        DailyRewards.onClaimPrize += Claim;
     
    }

    private void OnDisable()
    {
        DailyRewards.onClaimPrize -= Claim;
    }


    public void Claim(int Day)
    {
        Debug.Log("Day=" + Day);
        if (Day == 1)
        {

            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 1000);
        }
        if (Day == 2)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 2000);
        }
        if (Day == 3)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 5000);
          //  PrefsManager.SetPlayerState(5,1);
        }
        if (Day == 4)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 10000);
            //PrefsManager.SetUnlockCoinMode(1);
        }
        if (Day == 5)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 15000);
           // PrefsManager.SetPlayerState(4,1);
        }
        if (Day == 6)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 25000);

        }
        if (Day == 7)
        {
            PrefsManager.SetPlayerState(3,1);
            //PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 30000);
        }
    }



//    public void FreeMode()
//    {
//        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
//        Modes.SetActive(false);
//        PrefsManager.SetGameMode("free");
//        DogSelectionManager.instance.RedirectToDogSelection();
//        //loading.SetActive(true);
//        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
//        // loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("GameplayFinal");
//
//    }
    public void FreeMode(int number)
    {
        PrefsManager.SetGameMode("Free");
        HHG_PlayerSelection.instance.RedirectToDogSelection();
        
//        PrefsManager.SetWeather(number);
//        Play();

    }



    public void ChallangeMode(int modselect)
    {

        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
        ModeSelected(modselect);
        


    }

    public void ModeSelected(int modselect)
    {
      //  SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        Modes.SetActive(false);
        PrefsManager.SetGameMode("challange");
        PrefsManager.SetLevelMode(modselect);
        if (modselect == 1)
        {
           
            HHG_PlayerSelection.instance.dogSelectionCanvas.SetActive(true);
            HHG_PlayerSelection.instance.menuCanvas.SetActive(false);
            HHG_PlayerSelection.instance.SelectDogPlay();
        }
        else { 
        HHG_PlayerSelection.instance.RedirectToDogSelection();

        }

    }

}
