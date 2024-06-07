using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBanner : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnEnable() {

       // AdmobAdsManager.Instance.ShowMBanner();
       // AdmobAdsManager.Instance.HideFirstSmallBannerEvent();
       // AdmobAdsManager.Instance.HideSecondSmallBannerEvent();
    }

    public void OnDisable() {

     //   if (AdmobAdsManager.Instance)
        {
         //   AdmobAdsManager.Instance.HideMediumBannerEvent();
            
        }
       // AdCalls.instance.HideBanner();
    }

}
