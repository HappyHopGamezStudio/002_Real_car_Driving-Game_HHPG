using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class _LevelProperties : MonoBehaviour
{

    // Start is called before the first frame update
    public Transform PlayerPosition;
    public Transform TpsPosition;
    public GameObject[] LevelObjective;
    public string LevelStatment;
    public float Times;
    public int LevelReward;
    public int currentobjective = 0;
    public bool isCutScene, isSetPosition;
    public GameObject Timeline;
    public PlayableDirector Director;
    public CheckpointController checkpointController;
    public bool IsCheckpoint, IsRace, Islap = false;
    public int LapValue = 0;


    async void Start()
    {
        if (isCutScene)
        {
            HHG_TimeController.Instance.TimerObject.SetActive(false);
            HHG_UiManager.instance.HideGamePlay();
//            HHG_UiManager.instance.CutScene.SetActive(true);
            HHG_LevelManager.instace.HUDNavigationCanvas.gameObject.SetActive(false);
            HHG_LevelManager.instace.coinBar.gameObject.SetActive(false);
            HHG_LevelManager.instace.JemBar.gameObject.SetActive(false);
            Timeline.SetActive(true);
            if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.ThirdPerson)
            {
                HHG_GameManager.Instance.TpsCamera.gameObject.SetActive(false);
            }
            else
            {
                HHG_LevelManager.instace.rcc_camera.gameObject.SetActive(false);
            }

            Time.timeScale = 1f;
            Director.Play();
            Invoke("HideTimeline", (float)Director.duration - 0.9f);
            HHG_UiManager.instance.HideGamePlay();
            if (checkpointController != null)
            {
                checkpointController.enabled = false;
                await Task.Delay(1000);
                HHG_GameManager.Instance.TrafficSpawn.GetComponent<TSTrafficSpawner>().trafficCarsParent.gameObject
                    .SetActive(false);
            }

            HHG_TimeController.Instance.UpdateTimer(Times);
            HHG_TimeController.Instance.TimerObject.SetActive(false);
        }
        else
        {
            if (LevelStatment != "")
            {
                // HHG_UiManager.instance.OpenWoldData.CallManager.GetComponent<CounDownFormission>().statemanent(LevelStatment);
                HHG_UiManager.instance.ObjectiveText.text = LevelStatment;
            }

            HHG_TimeController.Instance.UpdateTimer(Times);
            HHG_UiManager.instance.ShowObjective(HHG_LevelManager.instace.currentHhgLevelProperties.LevelStatment);

            if (checkpointController != null)
            {
                checkpointController.enabled = true;
                /*await Task.Delay(1000);
                HHG_GameManager.Instance.TrafficSpawn.GetComponent<TSTrafficSpawner>().trafficCarsParent.gameObject.SetActive(false);*/

            }
        }

    }



    public bool freemode = false;

    public void Nextobjective()
    {
        currentobjective++;
        if (currentobjective >= LevelObjective.Length)
        {
            HHG_UiManager.instance.ShowComplete();
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + LevelReward);
        }
        else
        {
            if (freemode)
            {
                foreach (GameObject objective in LevelObjective)
                    objective.SetActive(false);
                currentobjective--;
            }
            else
            {
                foreach (GameObject objective in LevelObjective)
                    objective.SetActive(false);
                LevelObjective[currentobjective].SetActive(true);
                //  if (GT_GameManager.Instance.mapPath != null)
                //    GT_GameManager.Instance.mapPath.Target = LevelObjective[currentobjective].transform.GetChild(0);
                if (freemode)
                    PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 500);
            }
            // HHG_UiManager.instace.mapPath.SetDestinationAtFirst(LevelObjective[currentobjective].transform.GetChild(0));
        }
    }



    void OnEnable()
    {

        //CutSceneStart();
        currentobjective = 0;

        if (LevelObjective.Length > 0) LevelObjective[currentobjective].SetActive(true);



        if (Islap)
        {
            HHG_UiManager.instance.CheckPointBar.SetActive(false);
            HHG_UiManager.instance. Racebar.SetActive(false);
            HHG_UiManager.instance.LapBar.SetActive(true);
        }
        else if (IsRace)
        {
            HHG_UiManager.instance. CheckPointBar.SetActive(false);
            HHG_UiManager.instance. Racebar.SetActive(true);
            HHG_UiManager.instance.LapBar.SetActive(false);
        }
        else if (IsCheckpoint)
        {
            HHG_UiManager.instance.CheckPointBar.SetActive(true);
            HHG_UiManager.instance.Racebar.SetActive(false);
            HHG_UiManager.instance.LapBar.SetActive(false);
        }
        HHG_UiManager.instance.CarMobile.SetActive(false);
        HHG_UiManager.instance.Getoutbutton.SetActive(false);
    }


    public void HideTimeline()
    {
        Logger.ShowLog("HideTimeLine Ok");
        //  HHG_UiManager.instance.CutScene.SetActive(false);
        HHG_UiManager.instance.ShowGamePlay();
        Timeline.SetActive(false);

        HHG_TimeController.Instance.TimerObject.SetActive(true);
        HHG_LevelManager.instace.HUDNavigationCanvas.gameObject.SetActive(true);
        HHG_LevelManager.instace.coinBar.gameObject.SetActive(true);
        HHG_LevelManager.instace.JemBar.gameObject.SetActive(true);
        if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.ThirdPerson)
        {
            HHG_GameManager.Instance.TpsCamera.gameObject.SetActive(true);
        }
        else
        {
            HHG_LevelManager.instace.rcc_camera.gameObject.SetActive(true);
        }

        HHG_UiManager.instance.ShowObjective(HHG_LevelManager.instace.currentHhgLevelProperties.LevelStatment);
        if (checkpointController != null)
        {
            checkpointController.enabled = true;
        }
    }
}
