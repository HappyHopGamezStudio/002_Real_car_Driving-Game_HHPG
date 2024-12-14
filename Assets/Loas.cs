using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loas : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AICar")
        {
            if (!HHG_LevelManager.instace.hhgOpenWorldManager.IsComplte)
            {
                transform.gameObject.SetActive(false);
                Invoke(nameof(OnMe), 1f);
                HHG_UiManager.instance.ShowFail();
                if (HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.IsRace)
                {
                    foreach (var AllCheck in HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties
                                 .checkpointController.CheckpointsList)
                    {
                        AllCheck.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnMe()
   {
       transform.gameObject.SetActive(true);
   }
}
