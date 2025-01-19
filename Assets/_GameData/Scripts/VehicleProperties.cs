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
	C01=01,
	C02=02,
	C03=03,
	C04=04,
	C05=05,
	C06=06,
	C07=07,
	C08=08,
	C09=09,
	C10=10,
	C11=11,
	C12=12,
	C13=13,
	C14=14,
	C15=15,
	C16=16,
	C17=17,
	C18=18,
	C19=19,
	C20=20,
	C21=21,
	C22=22,
	C23=23,
	C24=24,
	C25=25,
	C26=26,
	C27=27,
	C28=28,
	C29=29,
	C30=30,
	C31=31,
	C32=32,
	C33=33,
	C34=34,
	C35=35,
	C36=36,
	C37=37,
	C38=38,
	C39=39,
	C40=40,
	C41=41,
	C42=42,
	C43=43,
	C44=44,
	C45=45,
	C46=46,
	C47=47,
	C48=48,
	C49=49,
	C50=50,
	C51=51,
	C52=52,
	C53=53,
	C54=54,
	C55=55,
	C56=56,
	C57=57,
	C58=58,
	C59=59,
	C60=60,
	C61=61,
	C62=62,
	C63=63,
	C64=64,
	C65=65,
	C66=66,
	C67=67,
	C68=68,
	C69=69,
	C70=70,
	C71=71,
	C72=72,
	C73=73,
	C74=74,
	C75=75,
	C76=76,
	C77=77,
	C78=78,
	C79=79,
	C80=80,
	C81=81,
	C82=82,
	C83=83,
	C84=84,
	C85=85,
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
		if (DefaultCarPostion==null)
		{
			DefaultCarPostion = transform.Find("DefaultCarPostion").transform;
		}
		
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
		currentHealth = PrefsManager.Gethealth(CarName);
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
	public bool IsCarOnintersial=false;

	public async void GetInCarForDrive()
	{



		TurnONAllLights();

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

		Rb.drag = 0.05f;
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
				if (CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent != null)
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



		Exuset.SetActive(true);

		if (TrafficCarAi)
		{
			HHG_LevelManager.instace.isPanelOn = true;
		}


		UpdateHealthText();
		if (IsCarOnintersial)
		{
			if (FindObjectOfType<HHG_AdsCall>())
			{
				FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
				PrefsManager.SetInterInt(1);
			}

			await Task.Delay(2000);

			if (FindObjectOfType<HHG_AdsCall>())
			{
				if (PrefsManager.GetInterInt() != 5)
				{
					FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
				}
			}
		}



		await Task.Delay(2000);
		if (AllAudioSource != null)
		{
			AllAudioSource.SetActive(true);
		}

		CarController.FrontLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
		CarController.FrontRightWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
		CarController.RearLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
		CarController.RearRightWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
	}



	public async void GetOutVehicle()
	{



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

		PrefsManager.Sethealth(CarName, currentHealth);


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
		Rb.GetComponent<Rigidbody>().velocity=Vector3.zero; 
		Rb.GetComponent<Rigidbody>().angularVelocity=Vector3.zero; 
		if (TrafficCarAi)
		{
			HHG_LevelManager.instace.isPanelOn = false;
		}
		if (IsCarOnintersial)
		{
			if (FindObjectOfType<HHG_AdsCall>())
			{
				FindObjectOfType<HHG_AdsCall>().showInterstitialAD();
				PrefsManager.SetInterInt(1);
			}

			await Task.Delay(2000);

			if (FindObjectOfType<HHG_AdsCall>())
			{
				if (PrefsManager.GetInterInt() != 5)
				{
					FindObjectOfType<HHG_AdsCall>().loadInterstitialAD();
				}
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
	    
	    if (other.gameObject.CompareTag("positionChanger"))
	    {
		    HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.raceDirectionPoint =  HHG_LevelManager.instace.hhgOpenWorldManager.CurrentMissionProperties.Scepos;
	    }
	    
	    else if (other.gameObject.CompareTag(GameConstant.Tag_Coin))
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

	    else  if (other.CompareTag(GameConstant.Tag_ScrrenShot) && !isTimeStopped)
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

	    if (other.gameObject.tag=="Checkpoint")
	    { 
		    HHG_UiManager.instance.MssionPanel.SetActive(true);
		    HHG_LevelManager.instace.Currentmission = other.gameObject.GetComponent<MissionTrigger02>();
		    HHG_LevelManager.instace.hhgOpenWorldManager.CuurentlevelId = other.gameObject.GetComponent<MissionTrigger02>().MissionId;
		    HHG_UiManager.instance.MissionTitle.text = other.gameObject.GetComponent<MissionTrigger02>().MissionTitle;
		    HHG_UiManager.instance.MissionStatment.text = other.gameObject.GetComponent<MissionTrigger02>().MissionStatment;
		    HHG_UiManager.instance.RewardText.text = other.gameObject.GetComponent<MissionTrigger02>().Reaward.ToString("N0");;
		    HHG_UiManager.instance.missionicon.sprite = other.gameObject.GetComponent<MissionTrigger02>().MegaRampData.icone;
	    }
    }

    private void OnTriggerExit(Collider other)
    {
	    if (other.gameObject.tag=="Checkpoint")
	    { 
		    HHG_UiManager.instance.MssionPanel.SetActive(false);
	    }
    }

    private bool ison = true;
    public void OffCoinsEffect()
    {
	    ison = true;
	   // HHG_UiManager.instance.EffectForcoin.SetActive(false);
    }

    /*#region Hitpanel
   
    public float slowMotionFactor = 0.1f; 
    public float hitForceThreshold = 50f; 
    public int continuousHitCount = 5; 
    public float hitResetTime = 3f; 

    public int hitCounter = 0; // Tracks number of hits
    private float lastHitTime; // Tracks time of the last hit



    private void OnCollisionEnter(Collision collision)
    {
	    if (collision.gameObject.CompareTag("TrafficCar") || collision.gameObject.CompareTag("AICar"))
	    {
		    float impactForce = collision.relativeVelocity.magnitude;
        
		    if (impactForce > hitForceThreshold)
		    {
			    hitCounter++;
			    lastHitTime = Time.time; 
            
			    if (hitCounter >= continuousHitCount || Time.time - lastHitTime <= hitResetTime)
			    {
				    TriggerSlowMotion();
			    }
		    }
	    }
      
    }



    private void TriggerSlowMotion()
    {
        HHG_UiManager.instance.HideGamePlay();
        Invoke(nameof(Showpanel),0.2f);
        Time.timeScale = slowMotionFactor; 
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }


    void Showpanel()
    {
	    HHG_UiManager.instance.ShowGamePlay(); 
	   // HHG_UiManager.instance.WrackedPanel.SetActive(false);
	    Time.timeScale = 1f; 
	    Time.fixedDeltaTime = 0.02f; 
	    hitCounter = 0;
    }

    #endregion*/

    #region HeallthWork
    
    
    public float maxHealth = 100f;
    public float currentHealth;




    

    public void ApplyDamage(float damage)
    {
	    currentHealth -= damage;
	    UpdateHealthText();
	    PrefsManager.Sethealth(CarName,currentHealth);
    }

    public void UpdateHealthText()
    {
	    HHG_UiManager.instance.HealthText.text = "" + currentHealth.ToString("F0");
	    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	    HHG_UiManager.instance.FillhealthBar.fillAmount = (float)currentHealth / maxHealth;
	    if (currentHealth <= 100)
	    {
		    HHG_UiManager.instance.FillhealthBar.color = HHG_LevelManager.instace.colers[0];
		    HHG_UiManager.instance.Ripairebutton.SetActive(true);
		    Fire.SetActive(false);
	    }

	    if (currentHealth <= 50)
	    {
		    HHG_UiManager.instance.FillhealthBar.color = HHG_LevelManager.instace.colers[1];
		    Fire.SetActive(false);
	    }

	    if (currentHealth <= 40)
	    {
		    HHG_UiManager.instance.FillhealthBar.color = HHG_LevelManager.instace.colers[2];
		    Fire.SetActive(false);
	    }

	    if (currentHealth <= 30)
	    {
		    Fire.SetActive(true);
		    HHG_UiManager.instance.FillhealthBar.color = HHG_LevelManager.instace.colers[3];
	    }

	    if (currentHealth <= 0)
	    {
		    Fire.SetActive(true);
		    DestroyCar();
		    currentHealth = 0;
	    }

    }

    private void DestroyCar()
    {
	    
	    transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
	    transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	    CarController.engineRunning = false;
	    Invoke("onpanel", 1.8f);
    }

    public async void onpanel()
    {
	    if (this.enabled)
	    {
		    HHG_UiManager.instance.HideGamePlay();
		    await Task . Delay(2000);
		    HHG_UiManager.instance.repairPanel.SetActive(true);
		    Logger.ShowLog("here"+transform.name);
	    }
    }
    public string CarName = "";
    public void RepairCar()
    { 
	    currentHealth = maxHealth;
	    CarController.repairNow = true;
	    PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() - 1000);
	    HHG_LevelManager.instace.coinBar.GetComponentInChildren<Text>().text = PrefsManager.GetCoinsValue().ToString();
	    Fire.SetActive(false);
	    CarController.engineRunning = true;
	    HHG_UiManager.instance.FillhealthBar.color  =  HHG_LevelManager.instace.colers[0];
	    HHG_UiManager.instance.FillhealthBar.fillAmount = 1;
	    HHG_UiManager.instance.ShowGamePlay();
	    PrefsManager.Sethealth(CarName,currentHealth);
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

    System.Collections.IEnumerator  CaptureAndShow()
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
       aoutoSpeedCaputheroff();
    
    }
    

    #endregion

   async void aoutoSpeedCaputheroff()
    {
	    await Task.Delay(2000);
	    HHG_GameManager.Instance?.ResumwTime();
    }
   
   
   
   
   
   
   
   
   
   
   
}