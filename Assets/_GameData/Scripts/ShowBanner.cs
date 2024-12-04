using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoogleMobileAds.Api;
using HHG_Mediation;
using UnityEngine;
using UnityEngine.UI;

public class ShowBanner : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnEnable()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().hideBanner1();
            FindObjectOfType<HHG_AdsCall>().showBigBannerAD(AdPosition.BottomLeft);
        }
    }

    public void OnDisable()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showBanner1();
            FindObjectOfType<HHG_AdsCall>().hideBigBanner();
        }
    }
}
