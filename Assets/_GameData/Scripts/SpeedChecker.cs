/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGames_Mediation;
using UnityEngine;
using UnityEngine.UI;

public class SpeedChecker : MonoBehaviour
{
    public async void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag=="Player"))
        { 
            if (GameManager.Instance.CurrentCar.GetComponent<Playerhitcar>().PlayerCar && LevelManager_EG.instace.RcccaCanvas.GetComponent<RCC_DashboardInputs>().KMH >= 120)
            {
                UiManagerObject_EG.instance.SpeedText.text = GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().speed.ToString("(00)  BEST PERFORMANCE");
                UiManagerObject_EG.instance.SpeedText.color = Color.green;
                UiManagerObject_EG.instance.ToslowPenel.SetActive(true);
                Invoke("offPAnel", 3f);
            }
            else if (GameManager.Instance.CurrentCar.GetComponent<Playerhitcar>().PlayerCar && LevelManager_EG.instace.RcccaCanvas.GetComponent<RCC_DashboardInputs>().KMH < 120)
            {
                UiManagerObject_EG.instance.SpeedText.text = GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().speed.ToString("(00) TO SLOW");
                UiManagerObject_EG.instance.SpeedText.color = Color.red;
                UiManagerObject_EG.instance.ToslowPenel.SetActive(true);
                Invoke("offPAnel", 3f);
            }
            /*if (LevelManager_EG.instace.RcccaCanvas.GetComponent<RCC_DashboardInputs>().KMH >= 150)
            {
                await Task.Delay(1500);
                UiManagerObject_EG.instance.RewadPanel.SetActive(true);
                Time.timeScale = 0.2f;
            }
            else
            {
                if (GameManager.Instance.CurrentCar.GetComponent<Playerhitcar>().PlayerCar)
                {
                    UiManagerObject_EG.instance.SpeedText.text = GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().speed.ToString("00 TO SLOW");
                    UiManagerObject_EG.instance.ToslowPenel.SetActive(true);
                    Invoke("offPAnel", 3f);
                }
            }#1#
        } 
    }

    public void offPAnel()
    {
        UiManagerObject_EG.instance.ToslowPenel.SetActive(false);
    }
}
*/
