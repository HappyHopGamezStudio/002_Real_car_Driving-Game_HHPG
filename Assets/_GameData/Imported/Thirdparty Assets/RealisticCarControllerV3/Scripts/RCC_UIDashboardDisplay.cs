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

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Dashboard Displayer")]
[RequireComponent (typeof(RCC_DashboardInputs))]
public class RCC_UIDashboardDisplay : MonoBehaviour {

	private RCC_DashboardInputs inputs;
	
	public Text RPMLabel;
	public Text KMHLabel;
	public Text GearLabel;

	void Start () {
		
		inputs = GetComponent<RCC_DashboardInputs>();
		StartCoroutine("LateDisplay");
		
	}

	void OnEnable(){

		StopAllCoroutines();
		StartCoroutine("LateDisplay");

	}
	
	IEnumerator LateDisplay () {

		while(true){

			yield return new WaitForSeconds(.04f);
		
			if(RPMLabel){
				RPMLabel.text = inputs.RPM.ToString("0");
			}
			
			if(KMHLabel){
				if(RCC_Settings.Instance.units == RCC_Settings.Units.KMH)
					KMHLabel.text = inputs.KMH.ToString("000");
				else
					KMHLabel.text = (inputs.KMH * 0.62f).ToString("0") + "\nMPH";
			}

			if(GearLabel){
				if(!inputs.NGear)
					GearLabel.text = inputs.direction == 1 ? (inputs.Gear + 1).ToString("0") : "R";
				else
					GearLabel.text = "N";
			}
			
		}

	}

}
