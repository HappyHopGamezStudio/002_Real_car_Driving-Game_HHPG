using System;
using UnityEngine;
using UnityEngine.UI;

public class CarSS : MonoBehaviour
{
    public Camera mainCamera;  // Assign the camera used for capturing the picture

    private float originalFOV;
    private bool isTimeStopped = false;
    public float transitionSpeed = 0.1f;
   // public bool isTransitioning = false;
    
    private void Awake()
    {
       // reward = HHG_LevelManager.instace.Reward[PrefsManager.GetSelectedPlayerValue()];
       // PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + reward + (HHG_LevelManager.instace.coinsCounter));
    }

    void Start()
    {
        HHG_UiManager.instance.uiPanel.SetActive(false); // Ensure the panel is initially hidden
        HHG_UiManager.instance.resumeButton.onClick.AddListener(ResumeTime);
       // mainCamera = HHG_LevelManager.instace.rcc_camera.GetComponentInChildren<Camera>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SS") && !isTimeStopped)
        {
            if (HHG_LevelManager.instace.Canvas.GetComponent<RCC_DashboardInputs>().KMH >= 80)
            {
                other.gameObject.SetActive(false);
                HHG_UiManager.instance.rewradMoneyText.text = 1000 + "";
                HHG_UiManager.instance.SpeedCaputer[0].text=HHG_GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().speed.ToString("00");
                HHG_UiManager.instance.SpeedCaputer[1].text=HHG_GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().speed.ToString("00");
                StopTimeAndCapture();
            }
            else
            { 
                HHG_UiManager.instance.SpeedCaputer[0].text=HHG_GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().speed.ToString("00");
                HHG_UiManager.instance.SpeedCaputer[1].text=HHG_GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().speed.ToString("00");
                HHG_UiManager.instance.OnspeedCaputer();
            }
        }
    }

    void StopTimeAndCapture()
    {
     
        HHG_LevelManager.instace.rcc_camera.gameObject.SetActive(false);
        originalFOV = mainCamera.fieldOfView;
        HHG_UiManager.instance.HideGamePlay();
        isTimeStopped = true;
        mainCamera.gameObject.SetActive(true);
        // Position the capture camera
        mainCamera.transform.position = transform.position + transform.forward * 7 + Vector3.up * 1.5f;
        mainCamera.transform.LookAt(transform);
        
        StartCoroutine(CaptureAndShow());
        Time.timeScale = 0;
    }

    System.Collections.IEnumerator CaptureAndShow()
    {
       // isTransitioning = true;
        yield return new WaitForEndOfFrame();
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        mainCamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        mainCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();
        mainCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        // Show the captured image in the UI
       HHG_UiManager.instance.capturedImage.sprite = Sprite.Create(screenShot, new Rect(0, 0, screenShot.width, screenShot.height), new Vector2(0.5f, 0.5f));
       HHG_UiManager.instance.uiPanel.SetActive(true);
    }

    void ResumeTime()
    {
        Time.timeScale = 1;
        mainCamera.fieldOfView = originalFOV;
        mainCamera.gameObject.SetActive(false);
        HHG_LevelManager.instace.rcc_camera.gameObject.SetActive(true);
        HHG_UiManager.instance.ShowGamePlay();
        isTimeStopped = false;
       // isTransitioning = false;
        HHG_UiManager.instance.uiPanel.SetActive(false);
    }
    
    /*void Update()
    {
        if (isTransitioning)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 65,  Time.unscaledDeltaTime * transitionSpeed);
        }
    }*/
}
