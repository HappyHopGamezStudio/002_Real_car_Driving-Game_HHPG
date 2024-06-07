using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu_Script_Only : MonoBehaviour
{
   
	public static Main_Menu_Script_Only Main_INS;


	public GameObject Loading_Picture_Show;



	public void Start()
	{
		AudioListener.pause = false;
		Main_INS = this;
	}

}
