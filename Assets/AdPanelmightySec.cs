using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Activation;
using System.Threading.Tasks;
using GameAnalyticsSDK;
using HHG_Mediation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdPanelmightySec : MonoBehaviour
{
    private float currentime = 0f;
    private float startingtime = 7f;

    [SerializeField] private Text countdountext;



    // Start is called before the first frame update
    void OnEnable()
    {
        currentime = startingtime;
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
    }



    // Update is called once per frame
    async void Update()
    {
        currentime -= Time.deltaTime;
        countdountext.text = currentime.ToString("<color=white>Advertisement show in  </color><color=Red>0</color><color=white>  Sec </color>");
        if (currentime <= 0)
        {
            currentime = 0;
            transform.gameObject.SetActive(false);
            HHG_UiManager.instance.AdBrakepanel.SetActive(true);
            await Task.Delay(1000);
            if (FindObjectOfType<HHG_AdsCall>())
            {
                FindObjectOfType<HHG_AdsCall>().showInterstitialAD();

                PrefsManager.SetInterInt(1);
            }

            HHG_UiManager.instance.AdBrakepanel.SetActive(false);
        }
    }

     void OnDisable()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
    }
}