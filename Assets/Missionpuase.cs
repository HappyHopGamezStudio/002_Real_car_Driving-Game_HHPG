using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missionpuase : MonoBehaviour
{
    private void OnEnable()
    {
       HHG_LevelManager.instace.isPanelOn = true;
    }

    private void OnDisable()
    {
        HHG_LevelManager.instace. isPanelOn = false;
    }
}
