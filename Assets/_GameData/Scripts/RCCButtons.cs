using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCCButtons : MonoBehaviour {

    public RCC_UIController raceButton;
    public RCC_UIController brakeButton;
    public RCC_UIController leftButton;
    public RCC_UIController rightButton;
    public RCC_UIController nitroButton;

  public  RCC_Settings rcc_Settings;
//    private void Awake()
//    {

//#if UNITY_EDITOR
//        print("chaiging controller type to keyboard");
//        rcc_Settings.controllerType = RCC_Settings.ControllerType.Keyboard;
//#else
//        print("chaiging controller type to Mobile");
//          rcc_Settings.controllerType= RCC_Settings.ControllerType.Mobile;
//#endif

    //}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            raceButton.pressing = true;
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            raceButton.pressing = false;
        }


        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            brakeButton.pressing = true;
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            brakeButton.pressing = false;
        }


        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftButton.pressing = true;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftButton.pressing = false;
        }



        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightButton.pressing = true;
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightButton.pressing = false;
        }

        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     nitroButton.pressing = true;
        // }
        // else if (Input.GetKeyUp(KeyCode.F))
        // {
        //     nitroButton.pressing = false;
        //     
        // }


    }
}
