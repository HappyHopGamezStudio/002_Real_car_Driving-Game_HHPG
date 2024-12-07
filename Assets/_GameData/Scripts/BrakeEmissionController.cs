using System;
using UnityEngine;

public class BrakeEmissionController : MonoBehaviour
{
    public RCC_CarControllerV3 carController; // Reference to the RCC car controller
    public Material emissionMaterial; // Reference to the material with emission property
    public float emissionIntensity = 1.0f; // Intensity of the emission when braking

    private bool isBraking = false;

    private void Awake()
    {
        if (carController==null)
        {
            carController = transform.GetComponent<RCC_CarControllerV3>();
        }
    }

    void Update()
    {
        if (carController.brakeInput > 0)
        {
            isBraking = true;
            SetEmission(true);
        }
        else  if (carController.handbrakeInput > 0)
        {
            isBraking = true;
            SetEmission(true);
        }
        else
        {
            if (isBraking)
            {
                isBraking = false;
                SetEmission(false);
            }
        }
    }

    void SetEmission(bool enable)
    {
        if (carController.enabled)
        {
           
            if (emissionMaterial != null)
            {
                if (enable)
                {
                    emissionMaterial.EnableKeyword("_EMISSION");
                    emissionMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
                }
                else
                {
                    emissionMaterial.DisableKeyword("_EMISSION");
                    emissionMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
                }
            }
        }
    }
}