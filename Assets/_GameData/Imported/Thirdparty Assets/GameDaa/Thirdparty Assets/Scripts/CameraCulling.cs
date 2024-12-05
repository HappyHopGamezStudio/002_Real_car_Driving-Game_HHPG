using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraCulling: MonoBehaviour
{
    public Camera mainCamera;
    public CinemachineFreeLook freeLookCamera;
    float[] distances = new float[32];
    
    // High-quality distances
    public float LargeDistance;
    public float MedDistance;
    public float SmallDistance;

    // Low-quality distances
    public float LowLargeDistance;
    public float LowMedDistance;
    public float LowSmallDistance;
    public float LowSmallCameraFar;

    public bool updatevalue;

    void Start()
    {
        // Get the CinemachineFreeLook component and the main Camera
        freeLookCamera = GetComponent<CinemachineFreeLook>();
        mainCamera = Camera.main; // Assumes main camera tag

        if (mainCamera == null)
        {
            Logger.ShowLog("Main camera not found. Make sure your main camera is tagged as 'MainCamera'.");
            return;
        }

        // Apply culling distances based on system memory and game quality
        if (SystemInfo.systemMemorySize <= 2560 || PrefsManager.GetGameQuality() == 2)
        {
            distances[6] = LowLargeDistance;
            distances[7] = LowMedDistance;
            distances[8] = LowSmallDistance;
            mainCamera.farClipPlane = LowSmallCameraFar;
        }
        else
        {
            distances[6] = LargeDistance;
            distances[7] = MedDistance;
            distances[8] = SmallDistance;
        }

        mainCamera.layerCullDistances = distances;
    }

    void Update()
    {
        if (updatevalue)
        {
            // Update high-quality distances in real-time
            distances[6] = LargeDistance;
            distances[7] = MedDistance;
            distances[8] = SmallDistance;
            mainCamera.layerCullDistances = distances;
            updatevalue = false;
        }
    }
}