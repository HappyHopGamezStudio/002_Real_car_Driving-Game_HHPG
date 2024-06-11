using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public enum ControlMode { simple = 1, touch = 2 }
public class HHG_GameControl: MonoBehaviour 
{

    public static HHG_GameControl manager;
    public GameObject getInVehicle;
    public static float accelFwd,accelBack;
    public static float steerAmount;
    
    [SerializeField]
    public static bool shift;
    public static bool brake;
    public static bool driving;
    public static bool jump;


    public ControlMode controlMode = ControlMode.simple;
    
  //  public BikeCamera vehicleCamera;
    private float drivingTimer=0.0f;
    public void VehicleAccelForward(float amount) { accelFwd = amount;}
    public void VehicleAccelBack(float amount) { accelBack = amount; }
    public void VehicleSteer(float amount) { steerAmount = amount; }
    public void VehicleHandBrake(bool HBrakeing) { brake = HBrakeing; }
    public void VehicleShift(bool Shifting) { shift = Shifting; }
    public void GetInVehicle() { if (drivingTimer == 0) { driving = true; drivingTimer = 3.0f; } }
    public void GetOutVehicle() { if (drivingTimer == 0) { driving = false; drivingTimer = 3.0f; } }
    public void Jumping() { jump = true; }
    public float restTime = 0.0f;  
    void Awake()
    {
        manager = this;
#if  UNITY_EDITOR
        controlMode = ControlMode.simple;
#else
         controlMode = ControlMode.touch;
#endif

    }
   
    void Update()
    {
        drivingTimer = Mathf.MoveTowards(drivingTimer,0.0f,Time.deltaTime);
    }
    public void CameraSwitch()
    {
      //  vehicleCamera.Switch++;
       // if (vehicleCamera.Switch > vehicleCamera.cameraSwitchView.Count) { vehicleCamera.Switch = 0; }
    }
    
    public void KICK()
    {
     //   LevelManager_EG.instace.TPSPlayer.GetComponent<Animator>().Play("kick");
       // FindObjectOfType<BallController>().isKicked = true;
    }
}