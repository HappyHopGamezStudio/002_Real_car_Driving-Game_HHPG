//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Dashboard Inputs")]
public class RCC_DashboardInputs : MonoBehaviour {

	public RCC_CarControllerV3 currentCarController;
	public Image ImageRmp;
	public GameObject RPMNeedle;
	public GameObject KMHNeedle;
	

	private float RPMNeedleRotation = 0f;
	private float KMHNeedleRotation = 0f;
	

	public float RPM;
	public float KMH;
	internal int direction = 1;
	internal float Gear;
	internal bool NGear = false;

	
	internal RCC_CarControllerV3.IndicatorsOn indicators;

	void Update(){

		if(RCC_Settings.Instance.uiType == RCC_Settings.UIType.None){
			gameObject.SetActive(false);
			enabled = false;
			return;
		}

		GetValues();

	}
	
	public void GetVehicle(RCC_CarControllerV3 rcc){

		currentCarController = rcc;
		RCC_UIDashboardButton[] buttons = GameObject.FindObjectsOfType<RCC_UIDashboardButton>();

		foreach(RCC_UIDashboardButton button in buttons)
			button.Check();

	}

	void GetValues(){

		if(!currentCarController)
			return;

		if(!currentCarController.canControl || currentCarController.AIController){
			return;
		}

		
		
		RPM = currentCarController.engineRPM;
		KMH = currentCarController.speed;
		direction = currentCarController.direction;
		Gear = currentCarController.currentGear;

		NGear = currentCarController.changingGear;
		
		
		indicators = currentCarController.indicatorsOn;

		if(RPMNeedle)
		{
			if (KMH>=120)
			{
				ImageRmp.color=Color.red;
				HHG_LevelManager.instace.rcc_camera.transform.GetChild(1).gameObject.SetActive(true);
				RPMNeedleRotation = (currentCarController.engineRPM / 50f);
				RPMNeedle.transform.eulerAngles = new Vector3(RPMNeedle.transform.eulerAngles.x ,RPMNeedle.transform.eulerAngles.y, -RPMNeedleRotation);
				ImageRmp.fillAmount = (RPMNeedleRotation / 180f);
			//	GT_LevelManager.instace.rcc_camera.thisCam.fieldOfView += 10f * Time.deltaTime;
				
			}
			else
			{
				ImageRmp.color=Color.cyan;
				HHG_LevelManager.instace.rcc_camera.transform.GetChild(1).gameObject.SetActive(false);
				RPMNeedleRotation = (currentCarController.engineRPM / 50f);
				RPMNeedle.transform.eulerAngles = new Vector3(RPMNeedle.transform.eulerAngles.x ,RPMNeedle.transform.eulerAngles.y, -RPMNeedleRotation);
				ImageRmp.fillAmount = (RPMNeedleRotation / 180f);
			//	GT_LevelManager.instace.rcc_camera.thisCam.fieldOfView = 60f* Time.deltaTime;
				
			}
		}
		if(KMHNeedle){
			if(RCC_Settings.Instance.units == RCC_Settings.Units.KMH)
				KMHNeedleRotation = (currentCarController.speed);
			else
				KMHNeedleRotation = (currentCarController.speed * 0.62f);
			KMHNeedle.transform.eulerAngles = new Vector3(KMHNeedle.transform.eulerAngles.x ,KMHNeedle.transform.eulerAngles.y, -KMHNeedleRotation);
		}
	}
}



