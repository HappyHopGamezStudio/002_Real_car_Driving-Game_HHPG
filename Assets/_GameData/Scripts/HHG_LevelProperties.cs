using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHG_LevelProperties : MonoBehaviour
{

    public Transform PlayerPosition;
    public GameObject[] LevelObjective, DropPosition;
    public string LevelStatment;
    public float Time;
    public int currentobjective = 0;
    public bool freemode;
    public void Nextobjective()
    {

        currentobjective++;
        if (currentobjective >= LevelObjective.Length)
        {
            HHG_UiManager.instance.ShowComplete();
            //LevelComplete
        }
        else
        {
            // if (freemode && LevelManager_EG.instace.SelectedPlayer.GetComponent<VehicleProperties>().isPessangerinBus)
            // {
            //     foreach (GameObject objective in DropPosition)
            //         objective.SetActive(false);
            //     foreach (GameObject objective in LevelObjective)
            //         objective.SetActive(false);
            //     GameObject gameObjectDrop = DropPosition[Random.Range(0, DropPosition.Length)];
            //     gameObjectDrop.SetActive(true);
            //     currentobjective--;
            //
            //     LevelManager_EG.instace.mapPath.Target = gameObjectDrop.transform.GetChild(0).GetChild(0);
            //
            // }
            // else
            // {
            //     foreach (GameObject objective in DropPosition)
            //         objective.SetActive(false);
            //     foreach (GameObject objective in LevelObjective)
            //         objective.SetActive(false);
            //     LevelObjective[currentobjective].SetActive(true);
            //     LevelManager_EG.instace.mapPath.Target = LevelObjective[currentobjective].transform.GetChild(0).GetChild(0);
            //     if (freemode)
            //         PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 500);
            // }
        //    LevelManager_EG.instace.mapPath.SetDestinationAtFirst(LevelObjective[currentobjective].transform.GetChild(0).GetChild(0));


        }

    }
    void OnEnable()
    {
        if (freemode)
        {
            reshuffle(LevelObjective);
        }

        if (LevelObjective.Length <1)
        {
            return;
        }
        
        LevelObjective[currentobjective].SetActive(true);
        //LevelManager.instace.mapPath.Target = LevelObjective[currentobjective].transform.GetChild(0).GetChild(0);
     //   LevelManager_EG.instace.mapPath.SetDestinationAtFirst(LevelObjective[currentobjective].transform.GetChild(0).GetChild(0));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void reshuffle(GameObject[] texts)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < texts.Length; t++)
        {
            GameObject tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
    }

}
