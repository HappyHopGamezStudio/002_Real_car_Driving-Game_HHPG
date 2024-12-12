using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class DriftPhysics : MonoBehaviour
{
	private Rigidbody thisRigidbody;
	private Transform thisTransform;
	public float Speed;
	public float Speed01;
	private RCC_CarControllerV3 RCCController;
	[Header("DRIFT")]
	private float maxspeedDelayDrift = 0.055f;
	private float accspeedDelayDrift = 0.1f;
	private int sensDrift = -1;
	public int driftPointTotal;
	public bool isDrifting;
	public bool isDriftScoring;
	public float delayDrift;
	public float speedDelayDrift;
	public float driftPoint;
	public int driftFactor;
	public float waitDriftFailed;
	public bool isWrongWay;
	public DriftCanvasManager driftCanvasManager;
	
	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action m_GetCarDamageFunc;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action m_GetDriftPointFunc;
	private Vector3 currentVel;
	private Vector3 currentVelAngle;
	public float MaxSpeed = 0;
		
		
		
		
		
	private void Awake()
	{
		if (driftCanvasManager == null)
		{
			driftCanvasManager = HHG_LevelManager.instace.driftCanvasManagerNow;
		}
		Awakewhenicall();
	}


	private void Start()
	{
		driftCanvasManager.Driftext.text = "DRIFT!";
	}


	public void Awakewhenicall()
	{
		driftCanvasManager.gameObject.SetActive(true);
		thisRigidbody = GetComponent<Rigidbody>();
		RCCController = GetComponent<RCC_CarControllerV3>();
		thisTransform = base.transform;
		{
			RCCController.antiRollFrontHorizontal /= 1.5f;
			RCCController.antiRollRearHorizontal /= 1.5f;
			RCCController.applyCounterSteering = true;
		}
		speedDelayDrift = maxspeedDelayDrift;
	}

	//private float[] TargetValues = { 1, 200, 300, 400, 500, 700 };
	private float[] TargetValuesDevided = { 1, 20, 40, 60, 100, 150,170,180};
	private int DevidedNumber=0;
	private void Update()
	{
		Speed = Vector3.Dot(thisRigidbody.velocity, thisTransform.forward);
		Speed01 = Mathf.Clamp01(Mathf.Abs(Speed / 30f));
		if (!thisRigidbody.useGravity)
		{
			thisRigidbody.AddForce(Vector3.down * 1f, ForceMode.Acceleration);
		}
		if (isDrifting)
		{
			speedDelayDrift += Time.deltaTime*  accspeedDelayDrift * (float)sensDrift;
			speedDelayDrift = Mathf.Clamp(speedDelayDrift, (0f - maxspeedDelayDrift) * 0.48f, maxspeedDelayDrift * 1.1f);
			delayDrift += speedDelayDrift * Time.deltaTime *45f;
			driftCanvasManager.UpdateWheel(delayDrift);
			isDriftScoring = false;
			if (delayDrift <= 0f)
			{
				delayDrift = 0f;
				DriftEnd(true);
			}
			else if (delayDrift >= 1f)
			{
				isDriftScoring = true;
				if (delayDrift >= 1.2f)
				{
					delayDrift = 1.2f;
					speedDelayDrift = 0f;
				}
			}
			if (isDriftScoring && !isWrongWay)
			{
				driftPoint += Time.deltaTime * (float)driftFactor  *25;
				driftCanvasManager.UpdatePoint(driftPoint);
				DevidedNumber = (int)(driftPoint / TargetValuesDevided[driftFactor]);
				//driftCanvasManager.FillBar.fillAmount = (float)(driftPoint *Time.deltaTime*0.1);
				switch (driftFactor)
				{
				case 0:
					driftFactor++;
					break;
				case 1:
					if (driftPoint > 50f)
					{
						driftFactor++;
						driftCanvasManager.Driftext.text = "DRIFT!";
						driftCanvasManager.Driftext.color=Color.white;
						driftCanvasManager.textPoint.color=Color.white;

					}
					break;
				case 2:
					if (driftPoint > 100f)
					{
						driftFactor++;
						driftCanvasManager.Driftext.text = "NICE DRIFT!";
						driftCanvasManager.Driftext.color=Color.yellow;
						driftCanvasManager.textPoint.GetComponent<Animator>().Play(0);
						driftCanvasManager.textPoint.color=Color.cyan;
					}
					break;
				case 3:
					if (driftPoint > 150f)
					{
						driftFactor++;
						driftCanvasManager.Driftext.text = "ASWOME DRIFT!";
						driftCanvasManager.Driftext.color=Color.yellow;
						driftCanvasManager.textPoint.GetComponent<Animator>().Play(0);
						driftCanvasManager.textPoint.color=Color.white;
						
					}
					break;
				case 4:
					if (driftPoint > 200)
					{
						driftFactor++;
						driftCanvasManager.Driftext.text = "BEST DRIFT!";
						driftCanvasManager.Driftext.color=Color.green;
						driftCanvasManager.textPoint.GetComponent<Animator>().Play(0);
						driftCanvasManager.textPoint.color= Color.red;
					}
					break;
				case 5:
					if (driftPoint > 250)
					{
						driftFactor++;
						driftCanvasManager.Driftext.text = "EXTEME DRIFT!";
						driftCanvasManager.Driftext.color=Color.magenta;
						driftCanvasManager.textPoint.GetComponent<Animator>().Play(0);
						driftCanvasManager.textPoint.color=Color.yellow;
					}
					break;
				case 6:
					if (driftPoint > 300)
					{
						driftFactor++;
						driftCanvasManager.Driftext.text = "OUTSTANDING DRIFT!";
						driftCanvasManager.Driftext.color=Color.cyan;
						driftCanvasManager.textPoint.GetComponent<Animator>().Play(0);
						driftCanvasManager.textPoint.color=Color.green;
					}
					break;
				}
				driftCanvasManager.UpdateFactor(driftFactor,DevidedNumber);
				if (PrefsManager.GetPlayerState(Slectedplayer) >= 0)
				{
					SaveValue(driftPoint);
				}

				
			}
		}
		if (waitDriftFailed > 0f)
		{
			waitDriftFailed -= Time.deltaTime;
		}		
	}

	public int Slectedplayer;
	private void DriftEnd(bool isOK)
	{
		driftCanvasManager.UpdatePointEnd(isOK);
		if (!isOK)
		{
			waitDriftFailed = 1f;
		}
		else
		{
			driftPointTotal += Mathf.RoundToInt(driftPoint);
			driftCanvasManager.UpdatePointFreeFlight(Mathf.RoundToInt(driftPoint));
		}
		driftPoint = 0f;
		driftCanvasManager.UpdatePoint(0f);
		
		driftFactor = 0;
		driftCanvasManager.UpdateFactor(0,0);
		speedDelayDrift = maxspeedDelayDrift;
		driftCanvasManager.canvasWheelAll.SetActive(false);
		isDrifting = false;
	}

	public void FixedUpdate()
	{
		currentVel = thisRigidbody.velocity;
		currentVelAngle = thisRigidbody.angularVelocity;
	}

	private void OnCollisionEnter(Collision check)
	{
		if (check.gameObject.CompareTag("RigidKine") || check.gameObject.CompareTag("TrafficCar"))
		{
			try
			{
				check.rigidbody.isKinematic = false;
				if (thisRigidbody!=null)
				{
					thisRigidbody.velocity = currentVel;
					thisRigidbody.angularVelocity = currentVelAngle;

				}
			}
			catch (Exception e)
			{
			//	GameAnalytics.NewErrorEvent(GAErrorSeverity.Error,"Error at OcCollision "+e.ToString());
				throw;
			}
		
		}
   		if (waitDriftFailed <= 0f && !check.gameObject.CompareTag("RigidKine") && !check.gameObject.CompareTag("DriftOK"))
		{
			DriftEnd(false);
		}
	}

	public void UpdateDriftStatus(bool isdrift)
	{
		if (isdrift)
		{
			if (!isDrifting && waitDriftFailed <= 0f)
			{
				isDrifting = true;
				driftCanvasManager.canvasWheelAll.SetActive(true);
				driftCanvasManager.Driftext.text = "DRIFT!";
				driftCanvasManager.Driftext.color=Color.white;
			}
			sensDrift = 1;
		}
		else
		{
			sensDrift = -1;
		}
	}

	private float currentSavedValue;
	private const string PrefKey = "SavedValue";
	public void SaveValue(float newValue)
	{
		 currentSavedValue = PlayerPrefs.GetFloat(PrefKey, 0f);
		if (newValue > currentSavedValue)
		{
			PlayerPrefs.SetFloat(PrefKey, newValue);
			PlayerPrefs.Save(); // Ensure the value is saved to disk
		}
	}
}
