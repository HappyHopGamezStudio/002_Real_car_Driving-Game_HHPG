using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class SpeedChangeAIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum AiType
    {
        Agrasive
        ,Normal
        ,Slow 
    }

public AiType CurrentAiType =AiType.Agrasive;
public float Distance=0;
public Transform Player;
private RCC_CarControllerV3 _controller;
private float Speed = 0;




    void Start()
    {
        CurrentAiType =AiType.Slow;
        if (Player==null)
        {
            if (HHG_GameManager.Instance.CurrentCar==null)
            {
                Player =  HHG_LevelManager.instace.SelectedPlayer.transform;
            }
            else
            {
                Player = HHG_GameManager.Instance.CurrentCar.transform;
            }
        }

        if (_controller==null)
        {
            _controller= GetComponent<RCC_CarControllerV3>();
        }
    }
    public Transform startPosition;
    private void OnEnable()
    {
        ChangeAIType();
        GetComponent<RCC_CarControllerV3>().KillOrStartEngine();
        GetComponent<RCC_CarControllerV3>().engineRunning=true; 
        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;
    }

    private void OnDisable()
    {
        GetComponent<RCC_CarControllerV3>().KillEngine();
        GetComponent<RCC_CarControllerV3>().engineRunning=false; 
    }


    private bool ischangeAI = false;
    public void ChangeAIType()
    {
        ischangeAI = true;
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(transform.position, Player.position);
        if (!ischangeAI)
        {
            return;
        }
        if (Distance < 90 )
        {
            if (CurrentAiType!=AiType.Agrasive)
            {
                CurrentAiType = AiType.Agrasive;
                AggrasiveAI();
            }
        }else if (Distance > 90 && Distance < 150)
        {
            if (CurrentAiType!=AiType.Normal)
            {
                CurrentAiType = AiType.Normal;
                NormalAI();
            }
        }
        else
        {
            if (CurrentAiType!=AiType.Agrasive )
            {
                CurrentAiType = AiType.Agrasive;
                AggrasiveAI();
            }
        }

    }

    public void NormalAI()
    {
        _controller._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
        _controller.maxspeed = 180;
        _controller.engineTorque = 7000;
        _controller.minEngineRPM = 8000;
       // Debug.LogError("NormalAi");
    }
    public void AggrasiveAI()
    {
        _controller._wheelTypeChoise = RCC_CarControllerV3.WheelType.AWD;
        _controller.maxspeed = 250;
        _controller.engineTorque = 10000;
        _controller.minEngineRPM = 10000;
      // Debug.LogError("AggrasiveAI");
    }
    /*public void SlowAI()
    {
        _controller._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
        _controller.maxspeed = 25;
        _controller.engineTorque = 3000;
        _controller.minEngineRPM = 2000;
        Debug.LogError("SlowAI");
    }*/
}
