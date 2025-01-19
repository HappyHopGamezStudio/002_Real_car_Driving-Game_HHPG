using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameAnalyticsSDK;
using HHG_Mediation;
using RGSK;
using SickscoreGames.HUDNavigationSystem;
using UnityEngine;
using Random = ITS.Utils.Random;

public enum PlayerStatus
{
    ThirdPerson,CarDriving,BikeDriving
}
public class HHG_GameManager : MonoBehaviour
{
    public static HHG_GameManager Instance;
    
    public delegate void VehicleInteraction(PlayerStatus Status);
    public static event VehicleInteraction OnVehicleInteraction;


    public PlayerStatus TpsStatus = PlayerStatus.ThirdPerson;
    
    [Header("ThirdPerson Stuff")]
    [Space(5)]

    public GameObject TPSPlayer;
    public GameObject Dog;

    [Space(5)]
    [Header("Car Stuff")]
    public Transform VehicleCamera; 
  //  public Transform bikecamera; 

  public speedcheck[] SpeedCheck;
    public Transform TpsCamera; 
    public GameObject CurrentCar;
    public Transform TrafficSpawn;
   

    public HUDNavigationSystem hud;

    public MapCanvasController MapCanvasController;
    public RacerPointer RacerPointerArrow;
    public GameObject BigMap,rcccanvas,TpsCanvas;
    public Camera BigMapCamera;
    [Space(5)] [Header("RewardedPanel Stuff")]
    public Transform DefaultCarPosition;                           
    public Transform DefaultCarPositionInTps;
    public GameObject DefaultRewardedCar,defultShadow;
    public GameObject[]Cars;

    public bool isgo = false;
    
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hud.PlayerCamera = TpsCamera.GetComponent<Camera>();
        hud.PlayerController = TPSPlayer.transform;
        MapCanvasController.playerTransform= TPSPlayer.transform;
        RacerPointerArrow.target= TPSPlayer.transform;
        Dog.transform.position = TPSPlayer.transform.position;
        /*foreach (var speed in SpeedCheck)
        {
            speed.Player=TPSPlayer.transform; 
        }*/
    }

    public async void OpenBigMap()
    {
        HHG_UiManager.instance.AdBrakepanel.SetActive(true);
        await Task.Delay(1000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
			
            PrefsManager.SetInterInt(1);
        }
        HHG_UiManager.instance.AdBrakepanel.SetActive(false);
        BigMap.SetActive(true);
        BigMapCamera.enabled = true;
        HHG_UiManager.instance.HideGamePlay();
      //  Time.timeScale = 0;
      await Task.Delay(2000);
      if (TpsStatus==PlayerStatus.CarDriving)
      {
          CurrentCar.GetComponent<Rigidbody>().velocity=Vector3.zero; 
          CurrentCar.GetComponent<Rigidbody>().angularVelocity=Vector3.zero; 
      }
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
    }

    public void OffBigMap()
    {
        BigMap.SetActive(false);
        BigMapCamera.enabled = false;
     //   Time.timeScale = 1;
        HHG_UiManager.instance.ShowGamePlay();
        TPSPlayer.GetComponent<Animator>().SetBool("SitBike", false); 
        TPSPlayer.GetComponent<PlayerThrow>().mobile.SetActive(false);// Start the "fa" animation
    }
    public async void GetInVehicle()
    {
        if (CurrentCar == null)
        {
            return;
        }
        CurrentCar.GetComponent<RCC_CarControllerV3>().KillOrStartEngine();
        Time.timeScale = 1;
        HHG_UiManager.instance.blankimage.SetActive(true);
        Invoke("offimage", 0.5f);
        HHG_UiManager.instance.controls.SetActive(true);
        HHG_UiManager.instance.TpsControle.SetActive(false);
        HHG_UiManager.instance.controls.GetComponent<CanvasGroup>().alpha=1;
        CurrentCar.GetComponent<Rigidbody>().angularDrag = 0.05f;
        if (CurrentCar.GetComponent<VehicleProperties>() == null)
        {
            return;
        }
        CurrentCar.GetComponent<VehicleProperties>().enabled = true;
        CurrentCar.GetComponent<DriftPhysics>().enabled = true;
      //  CurrentCar.GetComponent<DriftPhysics>().Awakewhenicall();
        CurrentCar.GetComponent<VehicleProperties>().GetInCarForDrive(); // = true;
        TPSPlayer.SetActive(false);
        Dog.SetActive(false);
        TpsCamera.gameObject.SetActive(false);
        VehicleCamera.gameObject.SetActive(true);
        OnVehicleInteraction?.Invoke(PlayerStatus.CarDriving);
        await Task.Delay(50);
        TpsStatus = PlayerStatus.CarDriving;
        HHG_LevelManager.instace.rcc_camera.SetTarget(CurrentCar);
        hud.PlayerCamera = VehicleCamera.GetComponentInChildren<Camera>();
        hud.PlayerController = CurrentCar.transform;
        MapCanvasController.playerTransform = CurrentCar.transform;
        RacerPointerArrow.target= CurrentCar.transform;
        CurrentCar.GetComponent<HHG_CarShadow>().enabled = true;
        CurrentCar.GetComponent<HHG_CarShadow>().ombrePlane.localScale= CurrentCar.GetComponent<HHG_CarShadow>().newSize;
        CurrentCar.GetComponent<HHG_CarShadow>().ombrePlane.gameObject.SetActive(true);
        CurrentCar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        if (CurrentCar.GetComponent<VehicleProperties>().TrafficCarAi)
        {
            HHG_LevelManager.instace.isPanelOn = true;
        }
        /*foreach (var speed in SpeedCheck)
        {
            speed.Player=CurrentCar.transform; 
        }*/
    }

    public void SartEngein()
    {
        CurrentCar.GetComponent<RCC_CarControllerV3>().KillOrStartEngine();
    }
    
    private void Update()
    {
        
        if (TpsStatus==PlayerStatus.CarDriving)
        {
            TrafficSpawn.position = VehicleCamera.position;
            TrafficSpawn.rotation = VehicleCamera.rotation;
        }
        else
        {
            TrafficSpawn.position = TpsCamera.position;
            TrafficSpawn.rotation = TpsCamera.rotation;
        }
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        
    }
    public void offimage()
    {
        HHG_UiManager.instance.blankimage.SetActive(false);
    }
    public async void GetOutVehicle()
    {
        Time.timeScale = 1;
        CurrentCar.GetComponent<HHG_CarShadow>().ombrePlane.gameObject.SetActive(false);
        CurrentCar.GetComponent<HHG_CarShadow>().enabled = false;
        CurrentCar.GetComponent<RCC_CarControllerV3>().KillOrStartEngine();
        HHG_UiManager.instance.blankimage.SetActive(true);
        Debug.Log("Here");
        HHG_UiManager.instance.controls.SetActive(false);
        HHG_UiManager.instance.controls.GetComponent<CanvasGroup>().alpha=0;
        HHG_UiManager.instance.TpsControle.SetActive(true);
        Invoke("offimage",0.5f);
        TPSPlayer.SetActive(true);
        Dog.SetActive(true);
        TpsCamera.gameObject.SetActive(true);
        VehicleCamera.gameObject.SetActive(false);
        CurrentCar.GetComponent<VehicleProperties>().GetOutVehicle();
        CurrentCar.GetComponent<VehicleProperties>().enabled = false;
        CurrentCar.GetComponent<DriftPhysics>().enabled = false;
        TPSPlayer.transform.position =CurrentCar.GetComponent<VehicleProperties>().TpsPosition.position;
        TPSPlayer.transform.eulerAngles =new Vector3(0,CurrentCar.GetComponent<VehicleProperties>().TpsPosition.rotation.y,0);
        Dog.transform.position = TPSPlayer.transform.position;
        RacerPointerArrow.target= TPSPlayer.transform;
        if (CurrentCar.GetComponent<VehicleProperties>().TrafficCarAi)
        {
            HHG_LevelManager.instace.isPanelOn = false;
        }
        
       // LevelManager.Instance.VehicleCameraNew.GetComponent<RCC_Camera>().RemoveTarget();
      
        OnVehicleInteraction?.Invoke(PlayerStatus.ThirdPerson);
        await Task.Delay(50);
        TpsStatus = PlayerStatus.ThirdPerson;
        hud.PlayerCamera = TpsCamera.GetComponent<Camera>();
        hud.PlayerController = TPSPlayer.transform;
        MapCanvasController.playerTransform = TPSPlayer.transform;
        /*foreach (var speed in SpeedCheck)
        {
            speed.Player=TPSPlayer.transform; 
        }*/
     
    }




    public Camera mainCamera;  
    public void RepairCar()
    {
        CurrentCar.GetComponent<VehicleProperties>().RepairCar();
    }
    public async void ResumwTime()
    {
        HHG_UiManager.instance.AdBrakepanel.SetActive(true);
        await Task.Delay(1000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
			
            PrefsManager.SetInterInt(1);
        }
        HHG_UiManager.instance.AdBrakepanel.SetActive(false);
       
        Time.timeScale = 1;
        mainCamera.gameObject.SetActive(false);
        HHG_LevelManager.instace.rcc_camera.gameObject.SetActive(true);
        HHG_UiManager.instance.ShowGamePlay();
        CurrentCar.GetComponent<VehicleProperties>(). isTimeStopped = false;
        HHG_UiManager.instance.uiPanel.SetActive(false);
        await Task.Delay(2000);
        if (FindObjectOfType<HHG_AdsCall>())
        {
            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
            }
        }
    }
    /*
    public void ResetScene()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }
        Invoke(nameof(LoadInter),2);
        HHG_UiManager.instance.ShowGamePlay(); 
      //  HHG_UiManager.instance.WrackedPanel.SetActive(false);
        Time.timeScale = 1f; 
        Time.fixedDeltaTime = 0.02f; 
        CurrentCar.GetComponent<VehicleProperties>().hitCounter = 0;
    }
    */
     
     #region CarPanel

    public void GetOutVehicleForInstantiate()
    {
        Time.timeScale = 1;
        CurrentCar.GetComponent<VehicleProperties>().IsCarOnintersial = false;
        CurrentCar.GetComponent<RCC_CarControllerV3>().KillOrStartEngine();
        HHG_UiManager.instance.blankimage.SetActive(true);
        Debug.Log("Here");
        Invoke("offimage",0.5f);
        CurrentCar.GetComponent<VehicleProperties>().GetOutVehicle();
        CurrentCar.GetComponent<VehicleProperties>().enabled = false;
        CurrentCar.GetComponent<DriftPhysics>().enabled = false;
        CurrentCar.GetComponent<HHG_CarShadow>().ombrePlane = null;
        CurrentCar.GetComponent<HHG_CarShadow>().enabled = false;
    }

    public void ShowInter()
    {
        if (FindObjectOfType<HHG_AdsCall>())
        {
            FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }
        Invoke(nameof(LoadInter),2);
    }

    public void LoadInter()
    {
        if (PrefsManager.GetInterInt() != 5)
        {
            FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
        }
    }

    

    private int CarPanel;

   

    public void CarInstantiateNow(int lValue)
    {
        PrefsManager.SetCurrentCarOnVideo(lValue);
        PrefsManager.SetCurrentCarShadow(lValue);
        HHG_Data.AdType = 19;
        if (FindObjectOfType<HHG_Admob>())
        {
            FindObjectOfType<HHG_Admob>().showRewardVideo(CarInstantiateDone);
        }
      //  CarInstantiateDone();
        GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "Admob", "Get_Car_OnVideo_By_mobile");
    }

    void CarInstantiateDone()
    {
        if (TpsStatus == PlayerStatus.ThirdPerson)
        {
            DefaultRewardedCar = Instantiate(Cars[PrefsManager.GetCurrentCarOnVideo()], DefaultCarPositionInTps.position, DefaultCarPositionInTps.rotation);
           
            DefaultRewardedCar.GetComponent<VehicleProperties>().enabled = true;
            DefaultRewardedCar.GetComponent<VehicleProperties>().IsCarOnintersial = false;
            DefaultRewardedCar.GetComponent<Rigidbody>().isKinematic = false;
            DefaultRewardedCar.GetComponent<VehicleProperties>().ConeEffect.SetActive(false);
            TPSPlayer.GetComponent<PlayerThrow>().OffMobile();
            DefaultRewardedCar.GetComponent<HHG_CarShadow>().ombrePlane = defultShadow.transform;
        }
        else if (TpsStatus == PlayerStatus.CarDriving)
        {
            GetOutVehicleForInstantiate();
            DefaultCarPosition = CurrentCar.GetComponent<VehicleProperties>().DefaultCarPostion;
            DefaultRewardedCar = Instantiate(Cars[PrefsManager.GetCurrentCarOnVideo()], DefaultCarPosition.position, DefaultCarPosition.rotation);
            CurrentCar = DefaultRewardedCar;
            TPSPlayer.GetComponent<PlayerThrow>().OffMobile();
            DefaultRewardedCar.GetComponent<VehicleProperties>().enabled = true;
            DefaultRewardedCar.GetComponent<VehicleProperties>().IsCarOnintersial = false;
            GetInVehicle();
            CurrentCar.GetComponent<HHG_CarShadow>().ombrePlane = defultShadow.transform;
        }
        Time.timeScale = 1;
    }

    #endregion
}

