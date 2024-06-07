using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HHG_LvlLocking : MonoBehaviour
{
	public int ID;

	private int LockingValue = 0;

	// Use this for initialization
	void OnEnable()
	{

		if (PrefsManager.GetLevelMode() == 0)
		{
			LockingValue = PrefsManager.GetLevelLocking();
		//	Debug.Log("levelLocking of level"+ LockingValue);
		}

		
		else if (PrefsManager.GetLevelMode() == 1)
		{
			LockingValue = PrefsManager.GetSnowLevelLocking();
//			Debug.Log("levelLocking of level"+ LockingValue);

		}

		else if (PrefsManager.GetLevelMode() == 2)
		{
			LockingValue = PrefsManager.GetDesertLevelLocking();
		//	Debug.Log("levelLocking of level"+ LockingValue);
		}

	




if (LockingValue >= ID)
		{
			transform.GetChild (1).gameObject.SetActive (false);
			gameObject.GetComponent<Button> ().interactable = true;
			GetComponent<Button> ().enabled = true;
			
		} 
//		else {
//			transform.GetChild (0).gameObject.SetActive (true);
//			GetComponent<Button> ().enabled = false;
//		}
	
	}
	

}
