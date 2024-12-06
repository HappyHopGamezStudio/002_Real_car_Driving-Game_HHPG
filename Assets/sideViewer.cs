using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;

public class sideViewer : MonoBehaviour
{
    public Camera StuntCamera;

    // Start is called before the first frame update
    void Start()
    {
        StuntCamera.gameObject.SetActive(false);
        lookAtConstraint = StuntCamera.GetComponent<LookAtConstraint>();
    }

    LookAtConstraint lookAtConstraint;
    public Transform newSource;

    public void AddSource()
    {
        if (lookAtConstraint != null && newSource != null)
        {
            ConstraintSource source = new ConstraintSource
            {
                sourceTransform = newSource,
                weight = 1.0f // Full influence by default
            };

            lookAtConstraint.AddSource(source);
            Debug.Log("Source added: " + newSource.name);
        }
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.CarDriving)
            {
                if (HHG_LevelManager.instace.Canvas.GetComponent<RCC_DashboardInputs>().KMH >= 150f)
                {
                    newSource = other.gameObject.transform;
                    StuntCamera.GetComponent<CameraFollow>().car=other.transform;
                    AddSource();
                    StuntCamera.gameObject.SetActive(true);
                    HHG_UiManager.instance.HideGamePlay();
                    HHG_LevelManager.instace.rcc_camera.gameObject.SetActive(false);
                    Invoke(nameof(BackToNormal), 4f);
                }
            }
        }
    }

    void BackToNormal()
    {
        StuntCamera.gameObject.SetActive(false);
        HHG_UiManager.instance.ShowGamePlay();
        HHG_LevelManager.instace.rcc_camera.gameObject.SetActive(true);
    }
}
