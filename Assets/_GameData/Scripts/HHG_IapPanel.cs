﻿using System.Collections;
using System.Collections.Generic;
using HHG_Mediation;
using UnityEngine;

public class HHG_IapPanel : MonoBehaviour
{
    
    public void ShowRewardedAd(int AdType) 
    {

        HHG_Data.AdType = AdType;
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
    public void LoadInter()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
    }

    public void ShowInter()
    {
        LoadInter();
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }
        LoadInter();
    }
    
    public void HideMediumBanner()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().hideBigBanner();
        }
    }
}