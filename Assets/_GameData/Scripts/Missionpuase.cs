using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missionpuase : MonoBehaviour
{
    private void OnEnable()
    {
        if (HHG_LevelManager.instace.hhgOpenWorldManager.missionon)
        {
            HHG_LevelManager.instace.isPanelOn = true;
        }
        else
        {
            HHG_LevelManager.instace.isPanelOn = true;
        }
    }

    private void OnDisable()
    {
        if (HHG_LevelManager.instace.hhgOpenWorldManager.missionon)
        {
            HHG_LevelManager.instace.isPanelOn = true;
        }
        else
        {
            HHG_LevelManager.instace.isPanelOn = false;
        }
    }
}
