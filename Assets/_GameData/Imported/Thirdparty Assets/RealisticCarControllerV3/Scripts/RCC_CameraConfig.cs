//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using System;
using UnityEngine;
using System.Collections;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Camera/Auto Camera Config")]
public class RCC_CameraConfig : MonoBehaviour {

    public bool automatic = true;
    private Bounds combinedBounds;
    public Transform Target;

    public float distance = 25f;
    public float height = 25f;
    public float TpsPitchangle = 7f;






    public void Start()
    {
        HHG_LevelManager.instace.rcc_camera.TPSDistance =distance;
        HHG_LevelManager.instace.rcc_camera.TPSHeight =height;
        HHG_LevelManager.instace.rcc_camera.TPSPitchAngle =TpsPitchangle;
      
        HHG_LevelManager.instace.rcc_camera.SetTarget(this.gameObject);
        
    }

    public void SetCameraSettings () {

        //HHG_LevelManager.instace.rcc_camera.TPSDistance =distance;
       // HHG_LevelManager.instace.rcc_camera.TPSHeight =height;
       // HHG_LevelManager.instace.rcc_camera.TPSPitchAngle = TpsPitchangle;

    }
    
    public void SetCameraSettingsNow () 
    {
        HHG_LevelManager.instace.rcc_camera.TPSDistance =distance;
        HHG_LevelManager.instace.rcc_camera.TPSHeight =height;
        HHG_LevelManager.instace.rcc_camera.TPSPitchAngle = TpsPitchangle;
       // HHG_LevelManager.instace.rcc_camera.SetTarget(this.gameObject);
    }

    public static float MaxBoundsExtent(Transform obj){
     
        var renderers = obj.GetComponentsInChildren<Renderer>();

        Bounds bounds = new Bounds();
        bool initBounds = false;
        foreach (Renderer r in renderers)
        {
            if (!((r is TrailRenderer)  || (r is ParticleSystemRenderer)))
            {
                if (!initBounds)
                {
                    initBounds = true;
                    bounds = r.bounds;
                }
                else
                {
                    bounds.Encapsulate(r.bounds);
                }
            }
        }
        float max = Mathf.Max(bounds.extents.x, bounds.extents.y, bounds.extents.z);
        return max;
    }

}