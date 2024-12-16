using System;
using System.Collections;
using UnityEngine;

public class BrakeEmissionController : MonoBehaviour
{
    public RCC_CarControllerV3 carController; // Reference to the RCC car controller
    public Material emissionMaterial;        // Reference to the material with emission property
    public float checkInterval = 0.1f;       // Time interval to check input states

    private bool isEmitting = false;         // Tracks the current emission state

    private void Awake()
    {
        if (carController == null)
        {
            carController = GetComponent<RCC_CarControllerV3>();
        }

        if (emissionMaterial == null)
        {
            Debug.LogWarning("Emission material is not assigned.");
        }
    }

    private void OnEnable()
    {
        // Start the coroutine to monitor brake and handbrake input
        StartCoroutine(CheckBrakeInput());
    }

    private void OnDisable()
    {
        // Stop the coroutine when the object is disabled
        StopCoroutine(CheckBrakeInput());
    }

    private IEnumerator CheckBrakeInput()
    {
        while (true)
        {
            // Determine if emission should be enabled
            bool shouldEmit = carController.brakeInput > 0 || carController.handbrakeInput > 0;

            // Update emission state only if it changes
            if (shouldEmit != isEmitting)
            {
                isEmitting = shouldEmit;
                SetEmission(isEmitting);
            }

            // Wait for the next check
            yield return new WaitForSeconds(checkInterval);
        }
    }

    private void SetEmission(bool enable)
    {
        if (emissionMaterial == null) return;

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