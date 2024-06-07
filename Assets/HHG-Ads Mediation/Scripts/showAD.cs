using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HHG_Mediation;

public class showAD : MonoBehaviour
{
  
    public void showBanner()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showBanner1();
            FindObjectOfType<HHG_AdsCall>().showBanner2();
        }
    }

    public void loadInter()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
        }
    }

    public void showInt()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
        }
    }

    public void showReward()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showRewardADsBoth(rewardDone);
        }
    }

    public GameObject reward;

     void rewardDone()
    {
        reward.SetActive(true);
    }

    public void bigBanner()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showBigBannerAD(GoogleMobileAds.Api.AdPosition.Center);
        }
    }
}
