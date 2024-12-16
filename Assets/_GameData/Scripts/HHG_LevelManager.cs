using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  SickscoreGames.HUDNavigationSystem;
using UnityEngine.Serialization;

public class HHG_LevelManager : MonoBehaviour
{
    public GameObject[] Levels;
    public GameObject[] Players;
    public int[] Reward;

    [FormerlySerializedAs("CurrentLevelProperties")]
    public HHG_LevelProperties currentHhgLevelProperties;

    public DrawMapPath mapPath;
    public GameObject SelectedPlayer, FreeMode, coinBar, JemBar;
    public static HHG_LevelManager instace;
    public LineRenderer Line;
    public int CurrentLevel, coinsCounter;
    public HUDNavigationSystem system;
    public RCC_Camera rcc_camera;

    public RCC_DashboardInputs Canvas;

    // public PlayerCamera_New Tpscamera;
    public HHG_OpenWorldManager hhgOpenWorldManager;
    public GameObject TpsPlayer;
    public GameObject Tpscamera;
    public DriftCanvasManager driftCanvasManagerNow;

    public Vector3 LastPosition;
    public Quaternion LastRotion;

    public HUDNavigationCanvas HUDNavigationCanvas;
    public GameObject destroyedCarPrefab;
    public AudioClip Coinsound;
    public AudioClip CheckPointSound;
    public AudioSource CoinSound;
    public Color[] colers;

    void Awake()
    {
        instace = this;

        if (PrefsManager.GetGameMode() != "free")
        {
            if (PrefsManager.GetLevelMode() == 0)
            {
                SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
                CurrentLevel = PrefsManager.GetCurrentLevel() - 1;
                currentHhgLevelProperties = Levels[CurrentLevel].GetComponent<HHG_LevelProperties>();
                SelectedPlayer.transform.position = currentHhgLevelProperties.PlayerPosition.position;
                SelectedPlayer.transform.rotation = currentHhgLevelProperties.PlayerPosition.rotation;
                currentHhgLevelProperties.gameObject.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1;
            FreeMode.SetActive(true);   
            JemBar.GetComponentInChildren<Text>().text = "" + PrefsManager.GetJEMValue();
            coinBar.SetActive(true);
            JemBar.SetActive(true);
            SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
         
            TpsPlayer.transform.position = hhgOpenWorldManager.TpsPosition.position;
            TpsPlayer.transform.rotation = hhgOpenWorldManager.TpsPosition.rotation;

            

            SelectedPlayer.transform.position =hhgOpenWorldManager.carPostion.position;
            SelectedPlayer.transform.rotation =hhgOpenWorldManager.carPostion.rotation;
        }

        SelectedPlayer.SetActive(true);

        SelectedPlayer.GetComponent<RCC_CarControllerV3>().KillOrStartEngine();
        SelectedPlayer.GetComponent<Rigidbody>().isKinematic = false;
        SelectedPlayer.GetComponent<HHG_CarShadow>().enabled = true;
        SelectedPlayer.GetComponent<VehicleProperties>().ConeEffect.SetActive(false);
        SelectedPlayer.GetComponent<VehicleProperties>().IsCarOnintersial = false;
        SelectedPlayer.GetComponent<Rigidbody>().constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezePositionX |
            RigidbodyConstraints.FreezePositionZ;
    }

    public void SetTransform(Transform Carposition, Transform tpsPosition )
    {
        if (HHG_GameManager.Instance.TpsStatus==PlayerStatus.CarDriving)
        {

            HHG_GameManager.Instance.CurrentCar.transform.position = Carposition.position;
            HHG_GameManager.Instance.CurrentCar.transform.rotation = Carposition.rotation;

        } 
        if (HHG_GameManager.Instance.TpsStatus==PlayerStatus.ThirdPerson)
        {
            
            SelectedPlayer.transform.position = Carposition.position;
            SelectedPlayer.transform.rotation = Carposition.rotation;
            
            
            TpsPlayer.transform.position = tpsPosition.position;
            TpsPlayer.transform.rotation = tpsPosition.rotation;
           

           
            
            HHG_GameManager.Instance. Dog.transform.position = tpsPosition.transform.position;
        }
    }


    public IEnumerator Start()
    {
        if (PrefsManager.GetLevelMode() != 1)
        {
            yield return new WaitForSeconds(0.5f);
            HHG_UiManager.instance.ShowObjective(currentHhgLevelProperties.LevelStatment);
        }
    }

    public void TaskCompleted()
    {
        currentHhgLevelProperties.Nextobjective();
    }

    private bool isUp = false;

    public void Respawn()
    {
        if (!isUp)
        {
            HHG_GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().ResetCarNow();
            HHG_GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().velocity = Vector3.zero;
            HHG_GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            isUp = true;
            Invoke(nameof(ofool), 2f);
        }

    }

    void ofool()
    {
        isUp = false;
    }

    public Material CarEffect;
    public float multiplaxer;
    private float offset;
    [Header("ForMissionCall")] public bool isPanelOn = false;
    public float timer = 60f;
    public Mobilemanger Mobilemanger;
    void Update()
    {
        offset += Time.deltaTime * multiplaxer;
        CarEffect.mainTextureOffset = new Vector2(0, offset);
        
        if (!isPanelOn)
        {
           
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                TpsPlayer?.GetComponent<PlayerThrow>().CallMission();
                timer = 60f;
            }
        }

        if (!isTrazitionok) return;
        
        if (isTrazitionok)
        {
            HHG_UiManager.instance.Left.GetComponent<RCC_UIController>().pressing = true;
            HHG_UiManager.instance.handBrake.GetComponent<RCC_UIController>().pressing = true;
        }
    }
    public bool isTrazitionok = false;
    public void ResetTimer()
    {
        timer = 70f;
    }

    public void StopMission()
    {
        isPanelOn = true;
    }
    public void PlayMission()
    {
        isPanelOn = false;
    }
}


