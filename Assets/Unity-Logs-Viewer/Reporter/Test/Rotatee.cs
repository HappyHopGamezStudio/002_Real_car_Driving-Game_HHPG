﻿using UnityEngine;
using System.Collections;

public class Rotatee : MonoBehaviour
{
	Vector3 angle;

	void Start()
	{
		angle = transform.eulerAngles;
	}

	void Update()
	{
		angle.x += Time.deltaTime * 100;
		transform.eulerAngles = angle;
	}

}
