using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seeting : MonoBehaviour
{
    [Header("Settingpart")]
    public Slider volume_value;
    public Text PercentageText;

    private bool isStart=false;
    public GameObject Low, High, Med;
    public GameObject musicbuttonON, musicebuttonOff, SounButtonON, SoundButtonOFF;
    // Start is called before the first frame update
    void OnEnable()
    {
        ChangeController(PrefsManager.GetControlls());
        SetGraphicQuality(PrefsManager.GetGameQuality());
        if (PrefsManager.GetMusic()==0)
        {
            musicebuttonOff.SetActive(true);
            musicbuttonON.SetActive(false);
        }
        else
        {
            musicebuttonOff.SetActive(false);
            musicbuttonON.SetActive(true);
        }
        if (PrefsManager.GetSound() == 0)
        {
            SoundButtonOFF.SetActive(true);
            SounButtonON.SetActive(false);
        }
        else
        {
            SoundButtonOFF.SetActive(false);
            SounButtonON.SetActive(true);
        }
        AudioListener.volume =0.7f;
        volume_value.value = PrefsManager.GetMusic();
        PercentageText.text = volume_value.value + "%";
        Time.timeScale = 1;
    }

     public void SetGraphicQuality(int value) {
        PrefsManager.SetGameQuality(value);
        switch (value) { 
            
            case 0:
               
                    
                Low.SetActive(false);
                Med.SetActive(false);
                High.SetActive(true);
                QualitySettings.SetQualityLevel(0);
              
                break;

            case  1:
                Low.SetActive(false);
                Med.SetActive(true);
                High.SetActive(false);
               
                QualitySettings.SetQualityLevel(1);
                break;

            case 2:
                Low.SetActive(true);
                Med.SetActive(false);
                High.SetActive(false);
                    
                QualitySettings.SetQualityLevel(2);
               
                break;
        }
       
     
        if (!isStart)
            ClickSound();
    }
    public void ClickSound() {
        HHG_SoundManager.Instance.PlayOneShotSounds(HHG_SoundManager.Instance.click);

    }

    public void MuteEvent ()
    {
        GameObject.FindGameObjectWithTag ("SoundManager").transform.GetChild (0).gameObject.GetComponent<AudioSource> ().enabled = false;
        PrefsManager.SetSound (0);
    }

    public void UnMuteEvent ()
    {
		
        GameObject.FindGameObjectWithTag ("SoundManager").transform.GetChild (0).gameObject.GetComponent<AudioSource> ().enabled = true;
        HHG_SoundManager.Instance.PlayOneShotSounds (HHG_SoundManager.Instance.click);
        PrefsManager.SetSound (1);
    }

    public void OFFmusic()
    {
        GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<AudioSource> ().enabled = true;
        PrefsManager.SetMusic(1);;
    }
    public void ONmusic()
    {
        GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<AudioSource> ().enabled = false;
        PrefsManager.SetMusic(0);
    }


    public  void SettingVolume(Slider volumeSlider)
    {
      PercentageText.text = volumeSlider.value + "%";
      PrefsManager.SetMusic(volume_value.value);
      HHG_SoundManager.Instance.gameObject.GetComponent<AudioSource>().volume = volumeSlider.value / 100; 
    }

    public GameObject Arrow, Tild, Setring;
    public void ChangeController(int index){

        Arrow.SetActive(false);
        Tild.SetActive(false);
        Setring.SetActive(false);
		
		
        switch(index){

            case 0:
                RCC_Settings.Instance.useAccelerometerForSteering = false;
                RCC_Settings.Instance.useSteeringWheelForSteering = false;
                Arrow.SetActive(true);
                PrefsManager.SetControlls(index);
			
                break;
            case 1:
                RCC_Settings.Instance.useAccelerometerForSteering = false;
                RCC_Settings.Instance.useSteeringWheelForSteering = true;
                Setring.SetActive(true);
                PrefsManager.SetControlls(index);
			
			
			
                break;
            case 2:
                RCC_Settings.Instance.useAccelerometerForSteering = true;
                RCC_Settings.Instance.useSteeringWheelForSteering = false;
                Tild.SetActive(true);
                PrefsManager.SetControlls(index);
                break;

        }

    }

}
