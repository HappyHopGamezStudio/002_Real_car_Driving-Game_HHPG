using UnityEngine;
using System.Collections;
using StarterAssets;
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
    public RectTransform joystickHandle;

    private static Vector3 initialHandlePosition;

    // Declare static delegates and events
    public delegate void JoystickAction();
    public static event JoystickAction OnJoystickReset;
    public static event JoystickAction OnJoystickRestore;

    private void Start()
    {
        // Store the initial position of the joystick
        if (joystickHandle != null)
            initialHandlePosition = joystickHandle.anchoredPosition;

        // Subscribe internal methods to events
        OnJoystickReset += ResetJoystick;
        OnJoystickRestore += RestoreJoystick;
    }


    public void ResetJoystick()
    {
     
        
        HHG_GameManager.Instance.TPSPlayer.GetComponent<ThirdPersonController>().MoveSpeed  =  0f;
        if (joystickHandle != null)
            joystickHandle.anchoredPosition = initialHandlePosition;
     
       global::Logger.ShowLog("Joystick Reset to 0");
    }

    public void RestoreJoystick()
    {
        HHG_GameManager.Instance.TPSPlayer.GetComponent<ThirdPersonController>().MoveSpeed  = 4f;
        if (joystickHandle != null)
            joystickHandle.anchoredPosition = initialHandlePosition;

       global::Logger.ShowLog("Joystick Restored to Normal");
    }
}