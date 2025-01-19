using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

//using System.Security.Policy;

public class CheckpointController : MonoBehaviour
{

    public Checkpoint[] CheckpointsList;
    //  public LookAtTargetController Arrow;

    private Checkpoint CurrentCheckpoint;
    private int CheckpointId;

    private void Awake()
    {
        if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.Islap)
        {
           CuurentLap= HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.CurrentLapValue;
           TotalLap= HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.LapValue;
        }
    }

    // Use this for initialization
    void OnEnable()
    {
        if (CheckpointsList.Length == 0) return;

        for (int index = 0; index < CheckpointsList.Length; index++)
            CheckpointsList[index].gameObject.SetActive(false);

        CheckpointId = 0;
        SetCurrentCheckpoint(CheckpointsList[CheckpointId]);
        if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.Islap)
        {
            CuurentLap= HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.CurrentLapValue;
            TotalLap= HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.LapValue;
        }
    }


    private void SetCurrentCheckpoint(Checkpoint checkpoint)
    {
        if (CurrentCheckpoint != null)
        {
            CurrentCheckpoint.gameObject.SetActive(false);
            CurrentCheckpoint.CheckpointActivated -= CheckpointActivated;
        }

        CurrentCheckpoint = checkpoint;
        CurrentCheckpoint.CheckpointActivated += CheckpointActivated;
        //Arrow.Target = CurrentCheckpoint.transform;
        CurrentCheckpoint.gameObject.SetActive(true);
       
        if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.IsCheckpoint)
        {
            float inverseValue = CheckpointId > 0 ? 1f / CheckpointId : 0f;
            HHG_UiManager.instance.displayText.text = CheckpointId + "/" + CheckpointsList.Length;
        }
        else if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.Islap)
        {
            HHG_UiManager.instance.LapText.text= CuurentLap + "/" + TotalLap;
            // Format and display the reciprocal
        }
    }

    void OffAlert()
    {
        HHG_UiManager.instance.LapAlert.SetActive(false);
    }

    public int CuurentLap = 0;   
    public int TotalLap = 2; 

    private void CheckpointActivated()
    {
        CheckpointId++;
        if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.Islap)
        {
            if (CuurentLap<TotalLap)
            {
                if (CheckpointId >= 7)
                {
                    CheckpointId = 0;
                    CuurentLap++;
                    HHG_UiManager.instance.LapAlert.SetActive(true);
                    Invoke(nameof(OffAlert),2f);
                } 
            }
        }

        if (CheckpointId >= CheckpointsList.Length)
        {
            CurrentCheckpoint.gameObject.SetActive(false);
            CurrentCheckpoint.CheckpointActivated -= CheckpointActivated;
            if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.IsCheckpoint)
            {
                HHG_LevelManager.instace.isTrazitionok = true;
                Invoke(nameof(callComplet), 3f);
                HHG_LevelManager.instace.rcc_camera.cameraMode = RCC_Camera.CameraMode.WHEEL;
                HHG_UiManager.instance.controls.GetComponent<CanvasGroup>().alpha=0;
                HHG_TimeController.Instance.StopTimer();
                if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties)
                {
                    HHG_UiManager.instance.CheckPointBar.SetActive(false);
                    HHG_UiManager.instance.Racebar.SetActive(false);
                    HHG_UiManager.instance.LapBar.SetActive(false);
                }   
            }

            else  if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.Islap)
            {
                if (CuurentLap >= TotalLap)
                {
                    HHG_LevelManager.instace.isTrazitionok = true;      
                    Invoke(nameof(callComplet), 3f);
                    HHG_LevelManager.instace.rcc_camera.cameraMode = RCC_Camera.CameraMode.WHEEL;
                    HHG_UiManager.instance.controls.GetComponent<CanvasGroup>().alpha=0;
                    HHG_TimeController.Instance.StopTimer();
                    if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties)
                    {
                        HHG_UiManager.instance.CheckPointBar.SetActive(false);
                        HHG_UiManager.instance.Racebar.SetActive(false);
                        HHG_UiManager.instance.LapBar.SetActive(false);
                    }    
                }
            }
            else if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.IsRace)
            {

                HHG_LevelManager.instace.isTrazitionok = true;
                HHG_LevelManager.instace.hhgOpenWorldManager.IsComplte = true;  
                Invoke(nameof(callComplet), 3f);
                HHG_LevelManager.instace.rcc_camera.cameraMode = RCC_Camera.CameraMode.WHEEL;
                HHG_UiManager.instance.controls.GetComponent<CanvasGroup>().alpha = 0;
                HHG_TimeController.Instance.StopTimer();
                if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties)
                {
                    HHG_UiManager.instance.CheckPointBar.SetActive(false);
                    HHG_UiManager.instance.Racebar.SetActive(false);
                    HHG_UiManager.instance.LapBar.SetActive(false);
                }
            }

            // Arrow.gameObject.SetActive(false);
            return;
        }

        SetCurrentCheckpoint(CheckpointsList[CheckpointId]);
    }

    void callComplet()
    {
        HHG_UiManager.instance.HideGamePlay();
        HHG_UiManager.instance.ShowComplete();
    }
}