using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HHG_Filled : MonoBehaviour {
	float stratpoint=0;
	public float endpoint = 50;
	private Image image;
	
	void Start () {
		

		//startSubtracting1 ();
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (Time.timeScale);
		if (stratpoint < endpoint) {
			stratpoint += 60*Time.deltaTime;
			image.fillAmount += 60*Time.deltaTime/100;
		}
	}

	public void OnEnable(){
		image = this.gameObject.transform.GetChild (0).gameObject.GetComponent<Image>();
	}

	public void OnDisable(){
		image.fillAmount = 0;
		stratpoint = 0;
	}


    public void SetEndpoint(int endpoint) {
        stratpoint = 0;
        this.endpoint = endpoint;
        image.fillAmount = 0f;
    }



}
