using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Logger : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
       // Debug.unityLogger.logEnabled = false;
    }

   // [Conditional("UNITY_EDITOR")]
    public static void ShowLog(string Log, bool isError = false)
    {
        if (isError)
        {
            Debug.LogError(Log);
        }
        else
        {
            Debug.Log(Log);
        }
    }
    
   
}
