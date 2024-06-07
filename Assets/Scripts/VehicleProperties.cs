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
    Policecar1=0,
    Policecar2=1,
    Policecar3=2, 
    Policecar4=3, 
    Policecar5=4,
    Policecar6=5,
    Policecar7=6,
    Policecar8=7,
    Policecar9=8,
    Policecar10=9,
   
}

public class VehicleProperties : MonoBehaviour
{
    public Transform TpsPosition;
    public GameObject ConeEffect;
    public Rigidbody Rb;
    [FormerlySerializedAs("controller")] public RCC_CarControllerV3 CarController;
    public GameObject Exuset;
    public GameObject AllAudioSource;
    
    
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
		TrafficCarAi = false;
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
		transform.GetComponent<Rigidbody>().velocity=Vector3.zero; 
		transform.GetComponent<Rigidbody>().angularVelocity=Vector3.zero; 
		GetComponent<HHG_CarShadow>().enabled = false;
		GetComponent<HHG_CarShadow>().ombrePlane.gameObject.SetActive(false);
		TrafficCarAi = true;

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
				Debug.Log("Grounded Car is in the Air");
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
		Debug.Log("Grounded StartDebugging....");
		yield return new WaitUntil(() => Grounded);
		Debug.Log("Grounded is true "+Grounded);
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
    
    
    string AiCarTag = "TrafficCar";
    string StuntTag = "Stunt";
    string FailTag = "Fail";
    string CallManagerTrue = "Call";





    public async void OnTriggerEnter(Collider other)
    {
	    if (TrafficCarAi)
		    return;
	    if (other.gameObject.tag == "Coin")
	    {
		    HHG_LevelManager.instace.CoinSound.Play();
		    HHG_UiManager.instance.EffectForcoin.SetActive(true);
		    Invoke("OffCoinsEffect", 3f);
		    PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 10);
		    other.gameObject.SetActive(false);
		    await Task.Delay(2000);
		    other.gameObject.SetActive(true);
	    }
    }

 
    /*string AiCarTage = "AiCar";

    public void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == AiCarTage)
        {
            if (collision.gameObject.GetComponentInParent<DamagManager>())
            {
               collision.gameObject.GetComponentInParent<DamagManager>().Damage(Time.timeScale / 1.2f);
            }
        }
    }*/

    public void OffCoinsEffect()
    {
	    HHG_UiManager.instance.EffectForcoin.SetActive(false);
    }
}