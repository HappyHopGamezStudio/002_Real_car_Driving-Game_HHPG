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
        Dog.transform.eulerAngles =new Vector3(0,CurrentCar.GetComponent<VehicleProperties>().TpsPosition.rotation.y,0);
       // LevelManager.Instance.VehicleCameraNew.GetComponent<RCC_Camera>().RemoveTarget();
      
        OnVehicleInteraction?.Invoke(PlayerStatus.ThirdPerson);
        await Task.Delay(50);
        TpsStatus = PlayerStatus.ThirdPerson;
        hud.PlayerCamera = TpsCamera.GetComponent<Camera>();
        hud.PlayerController = TPSPlayer.transform;
        MapCanvasController.playerTransform = TPSPlayer.transform;
       
     
    }

    /*#region MyRegion

    public async void sitonbike()
    {
        Time.timeScale = 1;
        Invoke("offimage",0.5f);
        UiManagerObject.instance.blankimage.SetActive(true);
        OnVehicleInteraction?.Invoke(PlayerStatus.BikeDriving);
        await Task.Delay(50);
        TpsStatus = PlayerStatus.BikeDriving;
        if (CurrentCar==null)
        {
            return;
        }
        CurrentCar.GetComponent<BikeControl>().enabled = true;
        UiManagerObject.instance.panels.TpsControle.SetActive(false);
        UiManagerObject.instance.panels.bikeControle.SetActive(true);
        CurrentCar.GetComponent<Rigidbody>().isKinematic = false;
        TpsCamera.gameObject.SetActive(false);
        TPSPlayer.gameObject.SetActive(false);
        LevelManager.instace.bikecamera.gameObject.SetActive(true);
        CurrentCar.GetComponent<BikeControl>().activeControl = true;
        CurrentCar.GetComponent<BikeControl>().bikeSetting.bikerMan.gameObject.SetActive(true);
        GameControl.manager.getoutBike.SetActive(true);
        Siran = false;
        mutebikesounds();
        CurrentCar.GetComponent<BikeControl>().bikeLights.brakeLights[0].gameObject.SetActive(true);
        CurrentCar.GetComponent<BikeControl>().bikeParticles.brakeParticlePrefab.SetActive(true);
        CurrentCar.GetComponent<BikeControl>().ConeEffect.SetActive(true);
        CurrentCar.GetComponent<BikeControl>().bikeParticles.brakeParticlePrefab.GetComponentInChildren<ParticleSystem>().Play();
        CurrentCar.GetComponent<BikeControl>().ConeEffect.SetActive(false);
        CurrentCar.GetComponent<BikeCheckonGround>().enabled = true;
        CurrentCar.GetComponent<PoliceLights>().enabled = true;
        LevelManager.instace.bikecamera.target = CurrentCar.transform;
        hud.PlayerCamera = bikecamera.GetComponent<Camera>();
        hud.PlayerController = CurrentCar.transform;



    }

    public void offimage()
    {
        UiManagerObject.instance.blankimage.SetActive(false);
    }
    public async void Levaeonbike()
    {
        Time.timeScale = 1;
        UiManagerObject.instance.blankimage.SetActive(true);
        Invoke("offimage",0.5f);
        OnVehicleInteraction?.Invoke(PlayerStatus.ThirdPerson);
        await Task.Delay(50);
        TpsStatus = PlayerStatus.ThirdPerson;
        UiManagerObject.instance.panels.TpsControle.SetActive(true);
        UiManagerObject.instance.panels.bikeControle.SetActive(false);
        TpsCamera.gameObject.SetActive(true);
        TPSPlayer.gameObject.SetActive(true);
        LevelManager.instace.bikecamera.gameObject.SetActive(false);
        CurrentCar.GetComponent<BikeControl>().activeControl = false;
        CurrentCar.GetComponent<BikeControl>().bikeSetting.bikerMan.gameObject.SetActive(false);
        TPSPlayer.transform.position =CurrentCar.GetComponent<BikeControl>().TpsPosition.position; 
        TPSPlayer.transform.eulerAngles =new Vector3(0,CurrentCar.GetComponent<BikeControl>().TpsPosition.rotation.y,0);
        CurrentCar.GetComponent<BikeControl>().bikeLights.brakeLights[0].gameObject.SetActive(false);
        CurrentCar.GetComponent<BikeControl>().ConeEffect.SetActive(true);
        Siran = true;
        mutebikesounds();
        CurrentCar.GetComponent<PoliceLights>().activeLight = false;
        CurrentCar.GetComponent<PoliceLights>().enabled = false;
        CurrentCar.GetComponent<BikeCheckonGround>().forgrounded();
   
        CurrentCar.GetComponent<BikeControl>().bikeParticles//bikeparticals
            .brakeParticlePrefab.GetComponentInChildren<ParticleSystem>().Stop();
       
        CurrentCar.GetComponent<BikeControl>().enabled = false;
        hud.PlayerCamera = TpsCamera.GetComponent<Camera>();
        hud.PlayerController = TPSPlayer.transform;
       
    }
    public void RestBike()
    {
        {
            if (CurrentCar==null)
            {
                return;
            }
            CurrentCar.transform.position += new Vector3(0, 10f, 0);
            CurrentCar.transform.eulerAngles += new Vector3(0, transform.eulerAngles.y, 0);
            GameControl.manager.restTime = 2.0f;
        }
    }

    public bool Siran = false;
    public void mutebikesounds()
    {
        if (Siran)
        {
            CurrentCar.GetComponent<BikeControl>().bikeSounds.IdleEngine.enabled = false;
            CurrentCar.GetComponent<BikeControl>().bikeSounds.HighEngine.enabled = false;
            CurrentCar.GetComponent<BikeControl>().bikeSounds.LowEngine.enabled = false;
        }
        else
        { 
            CurrentCar.GetComponent<BikeControl>().bikeSounds.IdleEngine.enabled = true;
            CurrentCar.GetComponent<BikeControl>().bikeSounds.HighEngine.enabled = true;
            CurrentCar.GetComponent<BikeControl>().bikeSounds.LowEngine.enabled = true;
        }
    }
    
    #endregion*/


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
