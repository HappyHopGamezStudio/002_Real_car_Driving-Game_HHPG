using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HHG_Mediation;
using ITS.AI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum  CarNames
{
	Supra_01=0,
	GT_02=1,
	NFT_03=2, 
	NFT_04=3, 
	Doge_05=4,
	RR_06=5,
	Nisan_07=6,
	Speed_08=7,
	GV_09=8,
	Mhri_10=9,
	CrolaType_11=10,
	GV_Sexy_12=11,
	Green3i_13=12,
	Open_eys_14=13,
	Paerot_15=14,
	Etron_16=15,
	Bugati_Cirun_17=16,
	Baba_car_18=17,
	Taxi_Car=18,
	White_Vagem=19,
	Green_GVagan=20,
	Green_Car=21,
	Blue_Car=22,
	Ambulance=23,
	Prado_Car=24,
}

public class VehicleProperties : MonoBehaviour
{
	public CarNames Names;
    public Transform TpsPosition;
    public GameObject ConeEffect;
    public Rigidbody Rb;
    public RCC_CarControllerV3 CarController;
    public GameObject Exuset;
    public GameObject AllAudioSource;

    public GameObject Fire;
    public Transform DefaultCarPostion;
    private RCC_Light[] carLights;
   
    
    
	private void Awake()
	{
		if (Rb==null)
		{
			Rb = GetComponent<Rigidbody>();
		}
		
		if (CarController==null)
		{
			CarController = GetComponent<RCC_CarControllerV3>();
		}
		if (mainCamera == null)
		{
			mainCamera = HHG_GameManager.Instance.mainCamera;
		}
		/*if (DefaultCarPostion==null)
		{
			DefaultCarPostion = transform.Find("DefaultCarPostion").transform;
		}*/
		
		carLights = GetComponentsInChildren<RCC_Light>();

		// Check if the car has lights
		if (carLights.Length > 0)
		{
			// Turn off all lights
			TurnOffAllLights();
		}
		else
		{
			Debug.LogWarning("No lights found on this car!");
		}
		CarName = Names.ToString();
	}
	void TurnOffAllLights()
	{
		foreach (var light in carLights)
		{
			light.enabled = false;
			light.gameObject.SetActive(false);
		}
	}
	void TurnONAllLights()
	{
		foreach (var light in carLights)
		{
			light.enabled = true;
			light.gameObject.SetActive(true);
		}
	}
	private void Start()
	{
		AllAudioSource = transform.Find("All Audio Sources").gameObject;
		
	}

	

	public bool TrafficCarAi=false;
	public async void GetInCarForDrive()
	{
		
		if (FindObjectOfType<HHG_AdsCall>())
		{
			FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
			PrefsManager.SetInterInt(1);
		}

		TurnONAllLights();
		currentHealth = PrefsManager.Gethealth(CarName);
		StopCoroutine(CheckisGrounded());
		if (CarController.chassis)
		{
			CarController.chassis.GetComponent<RCC_Chassis>().enabled = true;
		}
		CarController.enabled = true;
		transform.name = "!!!!!!!!!!!!!!MYCAR!!!!!!!!!!!!!!!!";
		GetComponent<RCC_CameraConfig>()?.SetCameraSettingsNow();
		if (!TrafficCarAi)
		{
			ConeEffect.SetActive(false);
		}
		if (AllAudioSource != null)
		{
			AllAudioSource.SetActive(false);
		}
		else
		{
			AllAudioSource = transform.Find("All Audio Sources").gameObject;
			AllAudioSource?.SetActive(false);
		}
		Rb.drag=0.05f;
		if (Rb)
		{
			Rb.constraints = RigidbodyConstraints.None;
			Rb.isKinematic = false;
			Rb.useGravity = true;
		}
		GetComponent<RCC_CameraConfig>().enabled = true;
		
		if (GetComponent<TSSimpleCar>())
		{
			if (CarController.chassis)
			{
				if (CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent!=null)
				{
					CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent?.SetActive(true);
				}
			}
			GetComponent<TSSimpleCar>().enabled = false;
			GetComponent<TSTrafficAI>().enabled = false;
			GetComponent<TSAntiRollBar>().enabled = false;
			GetComponent<TSAntiRollBar>().enabled = false;
			GetComponent<ChangeWheelTrafficToPlayer>().ChangeToPlayer();
		}
		
		CarController.FrontLeftWheelCollider.enabled = true;
		CarController.FrontRightWheelCollider.enabled = true;
		CarController.RearLeftWheelCollider.enabled = true;
		CarController.RearRightWheelCollider.enabled = true;
		
		
		CarController.FrontLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
		CarController.FrontRightWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
		CarController.RearLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
		CarController.RearRightWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
		Exuset.SetActive(true);
		
		
		GetComponent<HHG_CarShadow>().enabled = true; 
		GetComponent<HHG_CarShadow>().ombrePlane.gameObject.SetActive(true);
		
		UpdateHealthText();
		
		await Task.Delay(2000);
		if (AllAudioSource != null)
		{
			AllAudioSource.SetActive(true);
		}
		if (FindObjectOfType<HHG_AdsCall>())
		{
			if (PrefsManager.GetInterInt()!=5)
			{
				FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
			}
		}
	}



	public async void GetOutVehicle()
	{
		if (FindObjectOfType<HHG_AdsCall>())
		{
			FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
			PrefsManager.SetInterInt(1);
		}
		
		GetComponent<HHG_CarShadow>().enabled = false;
		GetComponent<HHG_CarShadow>().ombrePlane.gameObject.SetActive(false);
		
		TurnOffAllLights();
		if (AllAudioSource != null)
		{
			AllAudioSource.SetActive(false);
		}
		else
		{
			AllAudioSource = transform.Find("All Audio Sources").gameObject;
			AllAudioSource?.SetActive(false);
		}
		if (CarController.chassis)
		{
			CarController.chassis.GetComponent<RCC_Chassis>().enabled = false;
		}
		transform.GetComponent<Rigidbody>().velocity=Vector3.zero; 
		transform.GetComponent<Rigidbody>().angularVelocity=Vector3.zero; 
		CarController.FrontLeftWheelCollider.enabled = false;
		CarController.FrontRightWheelCollider.enabled = false;
		CarController.RearLeftWheelCollider.enabled = false;
		CarController.RearRightWheelCollider.enabled = false;

		CarController.FrontLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(false);
		CarController.FrontRightWheelCollider.transform.GetChild(0).gameObject.SetActive(false);
		CarController.RearLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(false);
		CarController.RearRightWheelCollider.transform.GetChild(0).gameObject.SetActive(false);
		Exuset.SetActive(false);
		CarController.enabled = false;
		GetComponent<RCC_CameraConfig>().enabled = false;
		
		PrefsManager.Sethealth(CarName,currentHealth);
		
		
		if (GetComponent<TSSimpleCar>())
		{
			if (CarController.chassis)
			{
				if (CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent != null)
				{
					CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent?.SetActive(false);
				}
			}
			GetComponent<TSSimpleCar>().enabled = true;
			GetComponent<TSAntiRollBar>().enabled = true;
			GetComponent<TSAntiRollBar>().enabled = true;
			GetComponent<TSTrafficAI>().enabled = true;
			GetComponent<ChangeWheelTrafficToPlayer>().ChangeToAI();
			
			UpdateHealthText();
			
			enabled = false;
		}
		else if (!TrafficCarAi)
		{
			ConeEffect.SetActive(true);
			if (Grounded)
			{
				Rb.angularDrag = 0;
				Rb.drag = 50f;
				Rb.isKinematic = true;
				enabled = false;
			}
			else
			{
				Logger.ShowLog("Grounded Car is in the Air");
				Rb.isKinematic = true;
				await Task.Delay(500);
				Rb.isKinematic = false;
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
				Rb.angularDrag = 0.05f;
				Rb.drag = 5f;
				StartCoroutine(CheckisGrounded());
			}
		}
		await Task.Delay(2000);
		if (FindObjectOfType<HHG_AdsCall>())
		{
			if (PrefsManager.GetInterInt()!=5)
			{
				FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
			}
		}
	}


	public IEnumerator CheckisGrounded()
	{
		Logger.ShowLog("Grounded StartDebugging....");
		yield return new WaitUntil(() => Grounded);
		Logger.ShowLog("Grounded is true "+Grounded);
		yield return new WaitForSeconds(1f);
		Rb.isKinematic = true;
		enabled = false;
	}
	
	public bool Grounded = false;
	public float groundCheckDistance=1.1f;
	RaycastHit hit;
	public LayerMask LayerMask;


    private void Update()
    {

        Debug.DrawRay(transform.position, -transform.up * groundCheckDistance, Color.green);
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance, LayerMask))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }



    }


    public async void OnTriggerEnter(Collider other)
    {
	    if (TrafficCarAi)
		    return;
	    if (other.gameObject.CompareTag(GameConstant.Tag_Coin))
	    {
		    HHG_LevelManager.instace.CoinSound.PlayOneShot(HHG_LevelManager.instace.Coinsound);
		    //  HHG_UiManager.instance.EffectForcoin.SetActive(true);
		    Invoke("OffCoinsEffect", 3f);
		    PrefsManager.SetJEMValue(PrefsManager.GetJEMValue() + 1);
		    HHG_LevelManager.instace.JemBar.GetComponentInChildren<Text>().text = PrefsManager.GetJEMValue().ToString();
		    other.gameObject.SetActive(false);
		    await Task.Delay(2000);
		    other.gameObject.SetActive(true);
	    }

	    if (other.CompareTag(GameConstant.Tag_ScrrenShot) && !isTimeStopped)
	    {
		    if (HHG_LevelManager.instace.Canvas.GetComponent<RCC_DashboardInputs>().KMH >=
		        other.GetComponent<speedcheck>().meterspeed)
		    {

			    other.gameObject.SetActive(false);
			    HHG_UiManager.instance.rewradMoneyText.text = 1000 + "";
			    HHG_UiManager.instance.SpeedCaputer[0].text = CarController.speed.ToString("00");
			    HHG_UiManager.instance.SpeedCaputer[1].text = CarController.speed.ToString("00");


			    StopTimeAndCapture();

		    }
		    else
		    {
			    if (ison)
			    {
				    HHG_UiManager.instance.SpeedCaputer[0].text = CarController.speed.ToString("00");
				    HHG_UiManager.instance.SpeedCaputer[1].text = CarController.speed.ToString("00");
				    HHG_UiManager.instance.SpeedCaputer[3].text = other.GetComponent<speedcheck>().meterspeed.ToString();
				    HHG_UiManager.instance.OnspeedCaputer();
				    ison = false;
				    Invoke("OffCoinsEffect", 3f);
			    }
		    }
	    }
    }

    private bool ison = true;
    public void OffCoinsEffect()
    {
	    ison = true;
	   // HHG_UiManager.instance.EffectForcoin.SetActive(false);
    }



    #region HeallthWork
    
    
    public float maxHealth = 100f;
    public float currentHealth;




    

    public void ApplyDamage(float damage)
    {
	    currentHealth -= damage;
	    UpdateHealthText();
    }

    public void UpdateHealthText()
    {
	    HHG_UiManager.instance.HealthText.text = "" + currentHealth.ToString("F0");
	    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	    HHG_UiManager.instance.FillhealthBar.fillAmount = (float)currentHealth / maxHealth;
	    if (currentHealth <= 100)
	    {
		    HHG_UiManager.instance.FillhealthBar.color  = HHG_LevelManager.instace.colers[0];
		    HHG_UiManager.instance.Ripairebutton.SetActive(true);
		    Fire.SetActive(false);
	    }
	    if (currentHealth <= 50)
	    {
		    HHG_UiManager.instance.FillhealthBar.color  =  HHG_LevelManager.instace.colers[1];
		    Fire.SetActive(false);
	    }
	    if (currentHealth <= 40)
	    {
		    HHG_UiManager.instance.FillhealthBar.color  =  HHG_LevelManager.instace.colers[2];
		    Fire.SetActive(false);
	    }
	    if (currentHealth <= 30)
	    {
		    HHG_UiManager.instance.FillhealthBar.color  =  HHG_LevelManager.instace.colers[3];
		    Fire.SetActive(true);
	    }
	    if (currentHealth <= 0)
	    {
		    currentHealth = 0;
		    Fire.SetActive(true);
		    DestroyCar();
	    }
	    
    }

    private void DestroyCar()
    {
	    transform.GetComponent<Rigidbody>().velocity=Vector3.zero; 
	    transform.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;

	    CarController.engineRunning = false;
	    Invoke("onpanel",3f);
    }

    public void onpanel()
    {
	    if (enabled)
	    {
		    HHG_UiManager.instance.repairPanel.SetActive(true);
		    Debug.Log("here"+transform.name);
		    HHG_UiManager.instance.HideGamePlay();
	    }
    }
    public string CarName = "";
    public void RepairCar()
    { 
	    currentHealth = maxHealth;
	    CarController.repairNow = true;
	    PrefsManager.SetJEMValue(PrefsManager.GetJEMValue() - 1000);
	    HHG_LevelManager.instace.coinBar.GetComponentInChildren<Text>().text = PrefsManager.GetCoinsValue().ToString();
	    Fire.SetActive(false);
	    CarController.engineRunning = true;
	    HHG_UiManager.instance.FillhealthBar.color  =  HHG_LevelManager.instace.colers[0];
	    HHG_UiManager.instance.FillhealthBar.fillAmount = 1;
	    HHG_UiManager.instance.ShowGamePlay();
	    PrefsManager.Sethealth(CarName,currentHealth);
	  //  PlayerPrefs.SetFloat("CarHealth", currentHealth); 
	    UpdateHealthText();
	    HHG_UiManager.instance.repairPanel.SetActive(false);
	    HHG_UiManager.instance.Ripairebutton.SetActive(false);
    }

    #endregion


    #region Car Speed Work 

    

    private float originalFOV;
    public bool isTimeStopped = false;

    public Camera mainCamera;

  
    void StopTimeAndCapture()
    {
     
        HHG_LevelManager.instace.rcc_camera.gameObject.SetActive(false);
        originalFOV = mainCamera.fieldOfView;
        HHG_UiManager.instance.HideGamePlay();
        isTimeStopped = true;
        mainCamera.gameObject.SetActive(true);
        // Position the capture camera
        mainCamera.transform.position = transform.position + transform.forward * 7 + Vector3.up * 2f;
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
    

    #endregion

    
}