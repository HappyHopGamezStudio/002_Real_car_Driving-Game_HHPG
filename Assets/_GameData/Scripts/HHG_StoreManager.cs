using System;
using HHG_Mediation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class HHG_StoreManager : MonoBehaviour
{
    public int[] numberOfvideo;
    public int[] reward;
    public int[] InAppreward;

    public Text[] watchvideoCount;
   // public Text[] TotalWatchVideoText;
    public Text[] rewardText;
   // public Text money, cashtext;
    public GameObject erroPanel;
    int CurrentVideoNo, nextvideoNo;
    public static HHG_StoreManager instance;
    public Text money;
    void Start()
    {
       // money.text = PrefsManager.GetCoin() + "";
        nextvideoNo = 1;
        instance = this;
        for (int i = 0; i < numberOfvideo.Length; i++)
        {
            watchvideoCount[i].text = PlayerPrefs.GetInt("GetWatchVideo" + nextvideoNo) + " / " + numberOfvideo[i];
            //TotalWatchVideoText[i].text = " / " + numberOfvideo[i];
            nextvideoNo++;
        }
        for (int i = 0; i <= reward.Length - 1; i++)
        {
            rewardText[i].text = reward[i] + "";
        }
    }


    public void WatchStatus()
    {
        int count = PlayerPrefs.GetInt("GetWatchVideo" + CurrentVideoNo);
        count += 1;
        PlayerPrefs.SetInt("GetWatchVideo" + CurrentVideoNo, count);
        print("Watch Status Called");

        if (PlayerPrefs.GetInt("GetWatchVideo" + CurrentVideoNo) == numberOfvideo[CurrentVideoNo])
        {
            int money = PrefsManager.GetCoinsValue();
            money += reward[CurrentVideoNo];
            PrefsManager.SetCoinsValue(money);

            PlayerPrefs.SetInt("GetWatchVideo" + CurrentVideoNo, 0);
        }
        watchvideoCount[CurrentVideoNo].text = PlayerPrefs.GetInt("GetWatchVideo" + CurrentVideoNo)  +" / " + numberOfvideo[CurrentVideoNo];
     //   money.text = PrefsManager.GetCoin() + "";
     //   cashtext.text = PrefsManager.GetCoin() + "";
    }


    public void InAppBuutonClick(int id)
    {
        InApp(id, InAppreward[id]);
    }

    public void RemoveAds()
    {
       // MyIAPManager_GF.instance.RemoveAds();
    }
    public void UnlockAllVehicles()
    {
       // MyIAPManager_GF.instance.UnlockAllVehicles();
    }
    public void UnlockAllVehiclesAndLevels()
    {
      //  MyIAPManager_GF.instance.UnlockAllVehicles();

    }
    public void InApp(int id, int coins)
    {
       // MyIAPManager_GF.instance.CashInApp(id, coins);
    }
    public void UpdateCashText(int newCoins)
    {
        int cash = PrefsManager.GetCoinsValue();
        cash += newCoins;
        PrefsManager.SetCoinsValue(cash);
       // money.text = cash + "";
    }

    private void Update()
    {
        money.text = PrefsManager.GetCoinsValue() + "";
    }
    public void ShowRewardedAd(int no)
    {
        CurrentVideoNo = no;
        HHG_Data.AdType = 6;
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showRewardADsBoth(GiverewaredNow);
        }
    }
    public void GiverewaredNow()
    {
        HHG_Data.RewardedAdWatched();
        Debug.Log("RewardGiven");
    }
    
}
