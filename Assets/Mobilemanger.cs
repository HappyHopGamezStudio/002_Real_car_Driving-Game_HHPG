using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobilemanger : MonoBehaviour
{

    public GameObject Mainbar, Profile, mission, Musicplayer,mobile;
    public bool IsManBar, Isprofilebar, missionBar, musicBar = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        Mainbar.SetActive(true);
        Profile.SetActive(false);
        mission.SetActive(false);
        Musicplayer.SetActive(false);
        IsManBar = true;
        Isprofilebar = false;
        missionBar = false;
        musicBar = false;
    }

    public void OnMission()
    {
        Mainbar.SetActive(false);
        Profile.SetActive(false);
        mission.SetActive(true);
        Musicplayer.SetActive(false);
        IsManBar = false;
        Isprofilebar = false;
        missionBar = true;
        musicBar = false;
    }

    public void OnProFile()
    {
        Mainbar.SetActive(false);
        Profile.SetActive(true);
        mission.SetActive(false);
        Musicplayer.SetActive(false);
        IsManBar = false;
        Isprofilebar = true;
        missionBar = false;
        musicBar = false;
    }

    public void OnMusicBar()
    {
        Mainbar.SetActive(false);
        Profile.SetActive(false);
        mission.SetActive(false);
        Musicplayer.SetActive(true);
        IsManBar = false;
        Isprofilebar = false;
        missionBar = false;
        musicBar = true;
    }

    public void OnMMainBar()
    {
        Mainbar.SetActive(true);
        Profile.SetActive(false);
        mission.SetActive(false);
        Musicplayer.SetActive(false);
        IsManBar = true;
        Isprofilebar = false;
        missionBar = false;
        musicBar = false;
    }

    public void OnBack()
    {
        Mainbar.SetActive(true);
        Profile.SetActive(false);
        mission.SetActive(false);
        Musicplayer.SetActive(false);
    }

    public void OnMap()
    {
        mobile.SetActive(false);
        HHG_GameManager.Instance.OpenBigMap();
        HHG_UiManager.instance.HideGamePlay();
    }
}
