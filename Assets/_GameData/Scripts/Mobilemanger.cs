using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;


public class Mobilemanger : MonoBehaviour
{

    public GameObject Mainbar, Profile, mission, Musicplayer,mobile;
    public bool IsManBar, Isprofilebar, missionBar, musicBar/*MissionCall*/ = false;
    // Start is called before the first frame update
   
    void Awake()
    {
        UpdateUI(); // Update UI with the first song
        /*Mainbar.SetActive(true);
        Profile.SetActive(false);
        mission.SetActive(false);
        Musicplayer.SetActive(false);
        CallPanel.SetActive(false);
        IsManBar = true;
        Isprofilebar = false;
        missionBar = false;
        musicBar = false;*/
    }

    public void OnMission()
    {
        Mainbar.SetActive(false);
        Profile.SetActive(false);
        mission.SetActive(true);
        Musicplayer.SetActive(false);
       // CallPanel.SetActive(false);
        IsManBar = false;
        Isprofilebar = false;
        missionBar = true;
        musicBar = false;
      //  MissionCall = false;
    }
    /*public void CallMe()
    {
        
        Mainbar.SetActive(false);
        Profile.SetActive(false);
        mission.SetActive(false);
        CallPanel.SetActive(true);
        Musicplayer.SetActive(false);
        IsManBar = false;
        Isprofilebar = false;
        missionBar = false;
        MissionCall = true;
        musicBar = false;
    }*/

    public void OnProFile()
    {
        Mainbar.SetActive(false);
        Profile.SetActive(true);
        mission.SetActive(false);
        Musicplayer.SetActive(false);
       // CallPanel.SetActive(false);
        IsManBar = false;
        Isprofilebar = true;
        missionBar = false;
        musicBar = false;
       // MissionCall = false;
    }

    public void OnMusicBar()
    {
        Mainbar.SetActive(false);
        Profile.SetActive(false);
      //  CallPanel.SetActive(false);
        mission.SetActive(false);
        Musicplayer.SetActive(true);
        IsManBar = false;
        Isprofilebar = false;
        missionBar = false;
        musicBar = true;
       // MissionCall = true;
    }

    public void OnMMainBar()
    {
        Mainbar.SetActive(true);
        Profile.SetActive(false);
        mission.SetActive(false);
      //  CallPanel.SetActive(false);
        Musicplayer.SetActive(false);
        IsManBar = true;
        Isprofilebar = false;
        missionBar = false;
        musicBar = false;
      //  MissionCall = false;
    }

    public void OnBack()
    {
        Mainbar.SetActive(true);
        Profile.SetActive(false);
        mission.SetActive(false);
       // CallPanel.SetActive(false);
        Musicplayer.SetActive(false);
        
        if (IsManBar)
        {
            HHG_GameManager.Instance.TPSPlayer.GetComponent<PlayerThrow>().OffMobile();
            HHG_UiManager.instance.ShowGamePlay();
        }
        IsManBar = true;
        Isprofilebar = false;
        missionBar = false;
        musicBar = false;
     //   MissionCall = false;
    }

    public void OnMap()
    {
        mobile.SetActive(false);
        HHG_GameManager.Instance.OpenBigMap();
        HHG_UiManager.instance.HideGamePlay();
    }
    public void RateUsButtonEvent ()
    {
        HHG_SoundManager.Instance.PlayOneShotSounds (HHG_SoundManager.Instance.click);
        Application.OpenURL ("https://play.google.com/store/apps/details?id=" + Application.identifier);
    }
    public AudioClip[] songs; // Array of songs
    public Image songImage; // UI Image for song
    public Text songDetailsText; // Text for song details
    public Sprite[] songImages; // Array of song cover images
    public string[] songDetails; // Array of song descriptions
    public Animator textAnimator;
    private int currentSongIndex = 0; // Index of the current song


    // Play the next song
    public void PlayNext()
    {
        currentSongIndex = (currentSongIndex + 1) % songs.Length; // Loop back to the first song
        PlaySong();
    }

    // Play the previous song
    public void PlayPrevious()
    {
        currentSongIndex = (currentSongIndex - 1 + songs.Length) % songs.Length; // Loop back to the last song
        PlaySong();
    }

    // Play or resume the current song
    public void PlaySong()
    {
        HHG_SoundManager.Instance.PlayAudio(songs[currentSongIndex]);
        HHG_SoundManager.Instance.PlayBgSound();
        UpdateUI();
        textAnimator.enabled = true;
        textAnimator.Play(0);
    }

    // Pause the current song
    public void PauseSong()
    {
        
        HHG_SoundManager.Instance.PusaeBgSound();
      
        textAnimator.enabled = false;
    }

    // Update the UI for the current song
    private void UpdateUI()
    {
        songImage.sprite = songImages[currentSongIndex];
        songDetailsText.text = songDetails[currentSongIndex];
    }

    #region MisionWork

    public void AcceptCall()
    {
        Time.timeScale = 1f;
        HHG_UiManager.instance.CallPanel.SetActive(false);
        HHG_LevelManager.instace.TpsPlayer.GetComponent<PlayerThrow>().OffMobile();
        HHG_UiManager.instance.HideGamePlay();
        HHG_UiManager.instance.LoadingForMission.SetActive(true);
    }
    public void Rejectcall()
    {
        Time.timeScale = 1f;
        HHG_UiManager.instance.CallPanel.SetActive(false);
        HHG_LevelManager.instace.TpsPlayer.GetComponent<PlayerThrow>().OffMobile();
        HHG_LevelManager.instace.isPanelOn = false;
        HHG_LevelManager.instace.ResetTimer();
    }
    #endregion
}
