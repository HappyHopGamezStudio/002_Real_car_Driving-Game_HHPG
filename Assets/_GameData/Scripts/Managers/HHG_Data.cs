using UnityEngine;
using System.Collections;
using NiobiumStudios;
using System.Collections.Generic;
using GameAnalyticsSDK;

public class HHG_Data : MonoBehaviour {
    public delegate void RemoveAds();
    public static event RemoveAds OnRemoveAds;


    public delegate void UnlockAllPlayers();
    public static event UnlockAllPlayers OnUnlockAllPlayers;

	public delegate void UnlockPlayers();
	public static event UnlockPlayers OnUnlockPlayersCheck;

	public delegate void UnlockAllMission();
    public static event UnlockAllMission OnUnlockAllMission;



    public static int AdType;
    // Use this for initialization
    void Start () {
		DontDestroyOnLoad (gameObject);
	}
	public static void CoinsAddition(int coins)
	{
		//GameObject.Find ("Canvas").GetComponent<ShowAds>().EnableRewarded();
		Debug.Log (coins + " Coins Added");
	}
	public static void GoldAddition(int coins)
	{
		Debug.Log (coins + " Gold Added");
	}
	public static void UnlockAllPurchased ()
	{
		
		RemoveAdsPurchased ();
		Debug.Log ("Unlock All Purchased");
	}
	public static void RemoveAdsPurchased ()
	{
		PlayerPrefs.SetInt ("removeads", 1);
		PlayerPrefs.Save ();
		
		Debug.Log ("Remove Ads Purchased");
        OnRemoveAds();
    }
    public static void UnlockAllMissiionsPurchased()
    {
        //LevelSelectionScript1.instance.UnlockAllLevels();

       global::Logger.ShowLog("Unlock All Levels");
        PrefsManager.SetLevelLocking(20);
        PlayerPrefs.SetFloat("UnlockAllLevels", 1f);
        OnUnlockAllMission.Invoke();

    }
    public static void UnlockAllVehiclesPurchased()
    {

        print("Data:unlock vehicle");

        PrefsManager.UnLockAllVehicle(5);

        PlayerPrefs.SetFloat("UnlockAllVehicles", 1f);
        OnUnlockAllPlayers();
        OnUnlockPlayersCheck?.Invoke();
    }




    public static void RewardEconomyPakage()
    {

        RemoveAdsPurchased();
        UnlockAllMissiionsPurchased();
        UnlockAllVehiclesPurchased();
       // PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 50000);
        PlayerPrefs.SetFloat("unlockEconomyPackage", 1);
       global::Logger.ShowLog("Reward economy pakage  ");
        OnUnlockPlayersCheck?.Invoke();
    }



    public static void RewardedAdWatched()
    {
       global::Logger.ShowLog("Reward Received");
        if (AdType == 0)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 500);
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_500_Coins");

        }

        if (AdType==1)
        {
	        PrefsManager.SetNosCount(2);
	        FindObjectOfType<RCC_MobileButtons>().FillNos();
	        GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_Nos");

        }
     
        if (AdType == 2) { 
		// MainMenuScript.instance.AddCashOnRewardedVideo();
		PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue()+1000);
		GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_1000_Coins");

		}
	
            if (AdType == 3)
        {
	        if (FindObjectOfType<HHG_NumberCounterAuto>())
	        {
		        
	        }
            FindObjectOfType<HHG_MoneyCounter>().DoubleReward();
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_double_reward");


        }
		if (AdType == 4)
        {
            HHG_UiManager.instance.FullTankWithVideo();
        }

        if (AdType == 5)
        {
	        HHG_PlayerSelection.instance.TestDrive();
	        GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_testDrive");

           // FindObjectOfType<MoneyCounterAdd>().DoubleReward();
            //VehicleSelectionScript.instance.AddCashOnRewardedVideo();
        }
        else if (AdType == 6)
        {
            FindObjectOfType<HHG_StoreManager>().WatchStatus();
        }
        else if (AdType == 7)
        {
	       HHG_GameManager.Instance. CurrentCar.GetComponent<VehicleProperties>().RepairCar();
	       GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","GEt_Car_repaired");

        }
        if (AdType == 8)
        {
          //  FindObjectOfType<TimeController>().TimeReward();
        }
        
        else if (AdType == 9)
        {

           // LevelManger.instance.UpdateHits();
        }
        else if (AdType == 10)
        {

          //  LevelManger.instance.UpdateHumanHits();
        }
        else if (AdType == 14)
        {
            FindObjectOfType<DailyRewardsInterface>().Close2xReward();
        }
        else if (AdType == 15)
        {
          //  FindObjectOfType<TimeController>().TimeRewardFreeMode();
        }
        else if (AdType == 16)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 500);
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_500_Coins");
            FindObjectOfType<HHG_PlayerSelection>().OffNoCash();
        }
        else if (AdType == 19)
        {
	      //  GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_Car_OnVideo_By_mobile");
        }
        //GameObject.Find ("Canvas").GetComponent<ShowAds_GP>().EnableRewarded();
    }

    //public static void SendCompleteEvent(int Level)
    //{
    //    Dictionary<string, object> param = new Dictionary<string, object>();
    //    param.Add("LevelNumber ", Level);
    //    AppMetrica.Instance.ReportEvent("LevelComplete", param);
    //}

    //public static void SendFailEvent(int Level)
    //{
    //    Dictionary<string, object> param = new Dictionary<string, object>();
    //    param.Add("LevelNumber ", Level);
    //    AppMetrica.Instance.ReportEvent("LevelFail", param);
    //}


}
