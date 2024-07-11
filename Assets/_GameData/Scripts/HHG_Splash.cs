using System.Collections;
using System.Collections.Generic;
using HHG_Mediation;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HHG_Splash : MonoBehaviour
{
    // Start is called before the first frame update
    public bl_SceneLoader bl_SceneLoader;


    void Awake() {
        GameAnalyticsSDK.GameAnalytics.Initialize();
    }

    public void Loadappopenad()
    {
        if (FindObjectOfType<HHG_appOpenHandler>())
        {
            FindObjectOfType<HHG_appOpenHandler>().LoadAppOpenAd();
        }
    }
    void Start()
    {
        StartCoroutine("changeScene");
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Invoke("Loadappopenad",6);
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(0.4f);
        //AdmobAdsManager.Instance.LoadInterstitialAd();
        if (FindObjectOfType<HHG_AdsCall>())
        {
            Debug.Log("Find Ads Called Done loading");
            FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            PrefsManager.SetInterInt(5);
        }
        bl_SceneLoader.LoadLevel("HHG_MainMenu");
    }
}
