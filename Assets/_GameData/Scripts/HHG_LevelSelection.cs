using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HHG_Mediation;
using UnityEngine;using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HHG_LevelSelection : MonoBehaviour {
	public GameObject[] Levels;
	public GameObject[] LevelContent;


	public GameObject Modes, errorOnMode, Desertmode, snowmode,loading;
	public Text alertobject;
	// Use this for initialization
	void Start () {

		fade.SetActive(false);

		//Levels[PrefsManager.GetLevelMode()].SetActive(true);
		

	}

	private void OnEnable()
	{
		

		HHG_Data.OnUnlockAllMission += UnlockAllLevels;
		// UnlockModes();
		Modes.SetActive(true);
		Levels[0].SetActive(false);
 
    }


	public void UnlockAllLevels() {

		for (int i = 0; i < LevelContent.Length; i++)
		{
			LevelContent[i].transform.GetChild(1).gameObject.SetActive(false);
			LevelContent[i].GetComponent<Button>().interactable = true;

		}
		//UnlockModes();
	}


   

	public void OnDisable()
	{


		HHG_Data.OnUnlockAllMission -= UnlockAllLevels;
	  
	}

	public GameObject fade;
	public void FreeMode()
	{ 
		HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
	//AdmobAdsManager.Instance.ShowInt(LoadGameNow,false);
	HHG_PlayerSelection.instance.dogSelectionCanvas.SetActive(false);
	HHG_PlayerSelection.instance.levelSelectionCanvas.SetActive(false);
	fade.SetActive(true);
	CameraRotate.instance.SetGoPos();
	gotoloading();

	}

	public Animator Shater;



	public void gotoloading()
	{
		Shater.enabled = true;
		Invoke("playeloading",5f);
	}



	public void playeloading()
	{
		loading.SetActive(true);
		fade.SetActive(false);
		PrefsManager.SetGameMode("free");
		loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("HHG_GamePlay");
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
	
	public void LoadGameNow()
	{
		Modes.SetActive(false);
		
	}

	public void ChallangeMode(int modselect)
	{
        //AdmobAdsManager.Instance.LoadInterstitialAd();
		HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
		if (modselect == 1)
		{
			ModeSelected(modselect);
			//if (PrefsManager.GetLevelLocking() > 10)
			//{
			//    ModeSelected(modselect);
			//}
			//else
			//{
			//    errorOnMode.SetActive(true);
			//    alertobject.text = "Please clear Mountain mode First.";
			//}
		}
		if (modselect == 2)
		{
			
            ModeSelected(modselect);
            //if (PrefsManager.GetLevelLocking() > 20)
            //{
            //	ModeSelected(modselect);
            //}
            //else
            //{
            //	errorOnMode.SetActive(true);

            //	alertobject.text = "Plese clear desert mode First.";
            //}
        }

		if (modselect == 0)
		{
			ModeSelected(modselect);
		}


	}

	public void ModeSelected(int modselect)
	{
		HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);
		Modes.SetActive(false);
		PrefsManager.SetGameMode("challange");
		PrefsManager.SetLevelMode(modselect);


		Levels[0].SetActive(false);
		Levels[1].SetActive(false);
		Levels[PrefsManager.GetLevelMode()].SetActive(true);

	}

	public void UnlockModes()
	{

		if (PrefsManager.GetLevelLocking() > 10)
		{
			Desertmode.transform.GetChild(0).gameObject.SetActive(false);
		}
		if (PrefsManager.GetLevelLocking() > 20)
		{
			snowmode.transform.GetChild(0).gameObject.SetActive(false);
		}

	}




}
