﻿using UnityEngine;
using System.Collections;
using ITS.AI;

public class PedestrianController : MonoBehaviour {

	protected Animator animator;
	
	private float speed = 0;
    public float speedMultiplier = 1.5f;
	public float direction = 0;
	private Locomotion locomotion = null;
	private Rigidbody body;
	// Use this for initialization
	void Start () 
	{
		body = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		locomotion = new Locomotion(animator);
		TSTrafficAI ai = GetComponent<TSTrafficAI>();
		if (ai == null)
		{
			ai = GetComponentInParent<TSTrafficAI>();
		}
		ai.OnUpdateAI = OnAIUpdate;
		ai.UpdateCarSpeed = UpdateSpeed;
	}


	Vector3 UpdateSpeed()
	{
        return transform.InverseTransformDirection(body.velocity);
	}
	
	void OnAIUpdate(float steering, float brake, float throttle, bool isUpSideDown ){
		speed = Mathf.Clamp01(throttle - brake) * speedMultiplier;
		direction = steering;
		
	}


	void Update () 
	{
	
		if (animator)
		{
			if (body.constraints != RigidbodyConstraints.FreezeRotation)body.constraints = RigidbodyConstraints.FreezeRotation;
			locomotion.Do(speed , 45* direction);
		}		
	}
}
