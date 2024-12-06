using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SickscoreGames.HUDNavigationSystem;
using UnityEngine;
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
    

    public Transform TpsCamera; 
    public GameObject CurrentCar;
    public Transform TrafficSpawn;
    public Transform Dust;

    public HUDNavigationSystem hud;

    public MapCanvasController MapCanvasController;
    public GameObject BigMap;
  
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hud.PlayerCamera = TpsCamera.GetComponent<Camera>();
        hud.PlayerController = TPSPlayer.transform;
        MapCanvasController.playerTransform= TPSPlayer.transform;
        Dog.transform.position = TPSPlayer.transform.position;

    }

    public void OpenBigMap()
    {
        BigMap.SetActive(true);
        HHG_UiManager.instance.HideGamePlay();
    }

    public void OffBigMap()
    {
        BigMap.SetActive(false);
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
            Dust.position = VehicleCamera.position;
            TrafficSpawn.rotation = VehicleCamera.rotation;
        }
        else
        {
            TrafficSpawn.position = TpsCamera.position;
            Dust.position = TpsCamera.position;
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
        CurrentCar.GetComponent<RCC_CarControllerV3>().KillOrStartEngine();
        HHG_UiManager.instance.blankimage.SetActive(true);
        Debug.Log("Here");
        HHG_UiManager.instance.controls.SetActive(false);
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
       
       // LevelManager.Instance.VehicleCameraNew.GetComponent<RCC_Camera>().RemoveTarget();
      
        OnVehicleInteraction?.Invoke(PlayerStatus.ThirdPerson);
        await Task.Delay(50);
        TpsStatus = PlayerStatus.ThirdPerson;
        hud.PlayerCamera = TpsCamera.GetComponent<Camera>();
        hud.PlayerController = TPSPlayer.transform;
        MapCanvasController.playerTransform = TPSPlayer.transform;
       
     
    }




    public Camera mainCamera;  
    public void RepairCar()
    {
       CurrentCar.GetComponent<VehicleProperties>().RepairCar();
    }
    public void ResumwTime()
    {
        Time.timeScale = 1;
        mainCamera.gameObject.SetActive(false);
        HHG_LevelManager.instace.rcc_camera.gameObject.SetActive(true);
        HHG_UiManager.instance.ShowGamePlay();
        CurrentCar.GetComponent<VehicleProperties>(). isTimeStopped = false;
        HHG_UiManager.instance.uiPanel.SetActive(false);
        
    }
    
     
    
}

