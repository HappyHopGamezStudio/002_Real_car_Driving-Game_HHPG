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

    // Use this for initialization
    void OnEnable()
    {
        if (CheckpointsList.Length == 0) return;

        for (int index = 0; index < CheckpointsList.Length; index++)
            CheckpointsList[index].gameObject.SetActive(false);

        CheckpointId = 0;
        SetCurrentCheckpoint(CheckpointsList[CheckpointId]);
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
            HHG_UiManager.instance.displayText.text = CheckpointId + "/" + CheckpointsList.Length + " = " + inverseValue.ToString("F2");
        }

        if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.Islap)
        {
            float reciprocal = 1f /  HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.LapValue;
            // Format and display the reciprocal
            HHG_UiManager.instance.LapText.text = reciprocal.ToString("F2");
        }
    }





    private void CheckpointActivated()
    {
        CheckpointId++;
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