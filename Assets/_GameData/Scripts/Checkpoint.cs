using System;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class Checkpoint : MonoBehaviour
{

	public Action CheckpointActivated;

	private  void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{
			if (CheckpointActivated != null)
			{
				CheckpointActivated();
				HHG_LevelManager.instace.CoinSound.PlayOneShot(HHG_LevelManager.instace.CheckPointSound);
				HHG_UiManager.instance.CheckpointShot.SetActive(true);
				//ameManager.Instance.CurrentCar.GetComponent<VehicleProperties_EG>().CheckPointEffect.SetActive(true);
				Invoke(nameof(CheckPointEffectOff),1.5f);
				///	other.GetComponentInParent<VehicleProperties_FL>().PlayEffect();
				//UiManagerObject_FL.instance.ShowComplete();
			}
		}
	}

	

	public void CheckPointEffectOff()
	{
		HHG_UiManager.instance.CheckpointShot.SetActive(false);
		//GameManager.Instance.CurrentCar.GetComponent<VehicleProperties_EG>().CheckPointEffect.SetActive(false);
	}
}
