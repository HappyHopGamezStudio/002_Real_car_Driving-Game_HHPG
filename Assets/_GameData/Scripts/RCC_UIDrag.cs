﻿//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Mobile UI Drag used for orbiting RCC Camera.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC UI Drag Handler")]
public class RCC_UIDrag : MonoBehaviour, IDragHandler, IEndDragHandler{

	public bool isPressing = false;
	public RCC_Camera RccCamera;
	public void OnDrag(PointerEventData data)
	{
		isPressing = true;	
		RccCamera.OnDrag (data);
	}

	public void OnEndDrag(PointerEventData data){

		isPressing = false;

	}

}
