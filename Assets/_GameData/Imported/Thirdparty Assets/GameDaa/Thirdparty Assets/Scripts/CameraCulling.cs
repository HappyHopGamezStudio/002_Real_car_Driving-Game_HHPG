using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraCulling : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera camera;
    float[] distances = new float[32];
    public float LargeDistance;
    public float MedDistance;
    public float SmallDistance;
    
    
    public float LowLargeDistance;
    public float LowMedDistance;
    public float LowSmallDistance;
    public float LowSmallCameraFar;
    
    public bool updatevalue;
    void Start()
    {
         camera = GetComponent<Camera>();


         if (SystemInfo.systemMemorySize <= 2560 || PrefsManager.GetGameQuality()==2)
         {
             distances[6] = LowLargeDistance;
             distances[7] = LowMedDistance;
             distances[8] = LowSmallDistance;
             camera.farClipPlane = LowSmallCameraFar;
         }
         else
         {
             distances[6] = LargeDistance;
             distances[7] = MedDistance;
             distances[8] = SmallDistance;
         }
         

        camera.layerCullDistances = distances;
    }



    public void Update()
    {
        if (updatevalue)
        {
            distances[6] = LargeDistance;
            distances[7] = MedDistance;
            distances[8] = SmallDistance;
            camera.layerCullDistances = distances;
            updatevalue = false;
        }
    }

}
