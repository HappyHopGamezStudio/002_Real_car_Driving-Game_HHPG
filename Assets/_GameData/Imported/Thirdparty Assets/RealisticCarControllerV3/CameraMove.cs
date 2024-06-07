using System;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public float DistanceDefault = 9f;

	public float AngleDefault = 19f;

	public float UpDefault = 3f;

	public float RightDefault;

	public float minAngle = -22f;

	public bool DynamicCam = true;

	public float heightDamping = 0.1f;

	public float rotationDamping = 0.13f;

	private Transform target;

	private float distance;

	private float dezoom = 2.5f;

	private float radiusCam = 0.6f;

	private Transform thisTransform;

	private float currentDistance;

	private Vector3 normalCam;

	private int layerMask = 49664;

	private Quaternion currentRotation;

	private Vector3 direction;

	private Vector3 backwardUp;

	public Vector3 rotateView;

	private bool isRetourAngleNormal = true;

	public bool isRotation = true;

	private Vector3 velocity = Vector3.zero;

	private Transform Playertag;

	private Vector3 targetPosition;

	private float distanceFromheli;

	private RaycastHit hit;

	private int ModeAngle;

	public Texture fadeTexture;

	public float currentHeight;

	private float currentRotationAngle;

	public bool isFollowPitch;

	private Vector3 newDirection;

	private float cameraFreeRefreshDist;

	private Vector3 cameraFreePos = Vector3.zero;

	private bool playZoom;

	private bool playDeZoom;

	private bool isDecroching;

	private Vector3 decrochePos;

	private Vector3 decrocheView;

	private Vector3 decrocheDir;

	private float distDecroching;

	private float timeFade;

	private void Awake()
	{
		Camera.main.nearClipPlane = 0.65f;
	}

	private void Start()
	{
		thisTransform = base.transform;
		distance = DistanceDefault;
		currentDistance = distance;
		Playertag = GameObject.Find("Player").transform;
		target = Playertag;
		backwardUp = new Vector3(0f, Mathf.Sin(AngleDefault * (float)Math.PI / 180f), -1f);
		backwardUp.Normalize();
		rotateView = new Vector3(0f, -110f, 0f);
		targetPosition = target.position + Vector3.up * UpDefault;
		normalCam = targetPosition + Quaternion.Euler(rotateView.x, target.eulerAngles.y + rotateView.y, rotateView.z) * backwardUp * distance;
		currentHeight = normalCam.y;
		currentRotationAngle = target.eulerAngles.y;
	}

	private void LateUpdate()
	{
		// if (Config.Step != 6)
		// {
		// 	switch (ModeAngle)
		// 	{
		// 	}
		// }
	}

	private void FixedUpdate()
	{
		// if (Config.Step == 6)
		// {
		// 	return;
		// }
		if (isDecroching)
		{
			distDecroching += Time.fixedDeltaTime;
			if (distDecroching < 25f)
			{
				decrochePos += decrocheDir * Time.fixedDeltaTime * 0.5f;
			}
			thisTransform.position = decrochePos;
			thisTransform.LookAt(decrocheView);
		}
		else if (ModeAngle == 0)
		{
			cameraClassic();
			cameraClassicRotate();
		}
		if (timeFade > 0f)
		{
			timeFade -= Time.fixedDeltaTime * 1.4f;
			if (timeFade <= 0f)
			{
				timeFade = 0f;
			}
		}
	}

	private void cameraFree()
	{
		cameraFreeRefreshDist = Utils.DistanceFast(target.position, thisTransform.position);
		if (cameraFreeRefreshDist > 260f)
		{
			cameraFreeNewPos();
			cameraFreeRefreshDist = Utils.DistanceFast(target.position, cameraFreePos);
		}
		float value = Mathf.Lerp(60f, 8f, cameraFreeRefreshDist / 180f);
		value = Mathf.Clamp(value, 8f, 60f);
		Camera.main.fieldOfView = value;
		if (value < 10f)
		{
			playDeZoom = true;
		}
		if (value > 24f)
		{
			playZoom = true;
		}
		if (value < 24f && playZoom)
		{
			GetComponent<AudioSource>().Play();
			playZoom = false;
		}
		if (value > 10f && playDeZoom)
		{
			GetComponent<AudioSource>().Play();
			playDeZoom = false;
		}
		thisTransform.position = cameraFreePos;
		thisTransform.LookAt(target.position);
	}

	private void cameraFreeNewPos()
	{
		int num = Utils.nbRandomSens();
		int num2 = Utils.nbRandomSens();
		cameraFreePos = target.position + Vector3.up * 5f + Utils.nbRandom(5) * Vector3.up + target.forward * 5f * num + target.forward * Utils.nbRandom(10) * num + target.right * 5f * num2 + target.right * Utils.nbRandom(10) * num2;
		playZoom = true;
		playDeZoom = false;
	}

	private void cameraInterieur()
	{
		if (isRetourAngleNormal && isRotation)
		{
			if (Mathf.Abs(rotateView.x) < 0.1f && Mathf.Abs(rotateView.y) < 0.1f && Mathf.Abs(rotateView.z) < 0.1f)
			{
				rotateView = Vector3.zero;
				isRotation = false;
			}
			else
			{
				rotateView = Vector3.SmoothDamp(rotateView, Vector3.zero, ref velocity, 0.6f, 80f);
			}
		}
	}

	private void cameraClassicRotate()
	{
		currentRotation = Quaternion.Euler(rotateView.x, currentRotationAngle + rotateView.y, rotateView.z);
		direction = currentRotation * backwardUp;
		normalCam = targetPosition + direction * distance;
		if (DynamicCam)
		{
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, target.eulerAngles.y, rotationDamping);
			currentHeight = Mathf.Lerp(currentHeight, normalCam.y, heightDamping);
		}
		else
		{
			currentRotationAngle = target.eulerAngles.y;
			currentHeight = normalCam.y;
		}
	}

	private void cameraClassic()
	{
		if (isRetourAngleNormal && isRotation)
		{
			if (Mathf.Abs(rotateView.x) < 0.1f && Mathf.Abs(rotateView.y) < 0.1f && Mathf.Abs(rotateView.z) < 0.1f)
			{
				rotateView = Vector3.zero;
				isRotation = false;
			}
			else
			{
				rotateView = Vector3.SmoothDamp(rotateView, Vector3.zero, ref velocity, 0.6f, 80f);
			}
		}
		if (isFollowPitch)
		{
			float num = target.eulerAngles.x + AngleDefault;
			backwardUp = new Vector3(0f, Mathf.Sin(num * ((float)Math.PI / 180f)), -1f);
			backwardUp.Normalize();
		}
		currentRotation = Quaternion.Euler(rotateView.x, currentRotationAngle + rotateView.y, rotateView.z);
		direction = currentRotation * backwardUp;
		targetPosition = target.position + Vector3.up * UpDefault + target.right * RightDefault;
		normalCam = targetPosition + direction * distance;
		Vector3 vector = normalCam;
		vector.y = currentHeight;
		normalCam = vector;
		distanceFromheli = distance;
		newDirection = (normalCam - targetPosition).normalized;
		if (Physics.SphereCast(targetPosition, radiusCam, newDirection, out hit, distanceFromheli, layerMask))
		{
			if (hit.distance > currentDistance + dezoom * Time.deltaTime)
			{
				currentDistance += dezoom * Time.deltaTime;
			}
			else
			{
				currentDistance = hit.distance;
			}
			thisTransform.position = targetPosition + newDirection * currentDistance;
		}
		else if (currentDistance < distanceFromheli)
		{
			currentDistance += dezoom * Time.deltaTime;
			if (currentDistance > distanceFromheli)
			{
				currentDistance = distanceFromheli;
			}
			thisTransform.position = targetPosition + newDirection * currentDistance;
		}
		else
		{
			thisTransform.position = normalCam;
		}
		thisTransform.LookAt(targetPosition);
	}

	public void setRotate(Vector3 rotate)
	{
		rotateView += rotate;
		switch (ModeAngle)
		{
		case 0:
			if (rotateView.x < minAngle)
			{
				rotateView.x = minAngle;
			}
			break;
		}
		isRotation = true;
	}

	public void setResetView(bool b)
	{
		isRetourAngleNormal = b;
	}

	public void ChangeAngleCam(int mode)
	{
		ModeAngle = mode;
		switch (mode)
		{
		case 0:
			Camera.main.nearClipPlane = 0.65f;
			((Behaviour)Camera.main.GetComponent("FlareLayer")).enabled = true;
			Camera.main.fieldOfView = 60f;
			break;
		case 1:
			Camera.main.nearClipPlane = 0.1f;
			((Behaviour)Camera.main.GetComponent("FlareLayer")).enabled = false;
			Camera.main.fieldOfView = 60f;
			break;
		case 2:
			cameraFreeNewPos();
			Camera.main.nearClipPlane = 0.65f;
			((Behaviour)Camera.main.GetComponent("FlareLayer")).enabled = true;
			Camera.main.fieldOfView = 60f;
			break;
		}
	}

	public void SetAngle(float angleY)
	{
		AngleDefault = angleY;
		backwardUp = new Vector3(0f, Mathf.Sin(AngleDefault * (float)Math.PI / 180f), -1f);
		backwardUp.Normalize();
	}

	public void DecrocheAndGo(Vector3 pos, Vector3 view, Vector3 Direction)
	{
		isDecroching = true;
		distDecroching = 0f;
		decrochePos = pos;
		decrocheView = view;
		decrocheDir = Direction;
		timeFade = (float)Math.PI / 2f;
	//	Config.refreshObjectQualityTime = 999f;
	//	Config.Step = 7;
	}

	public void SetFade()
	{
		timeFade = (float)Math.PI / 2f;
	}

	public void StopDecroche(bool fade)
	{
		isDecroching = false;
		if (fade)
		{
			timeFade = (float)Math.PI / 2f;
		}
		//Config.refreshObjectQualityTime = 999f;
		//Config.Step = 5;
	}

	public void OnGUI()
	{
		if (timeFade > 0f)
		{
			GUI.color = new Color(1f, 1f, 1f, Mathf.Sin(timeFade));
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), fadeTexture);
		}
	}




}
