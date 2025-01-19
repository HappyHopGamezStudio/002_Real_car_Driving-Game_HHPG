
// CameraRotate
using System.Collections;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform targetObject;
    public Camera CameraObject;
    public Vector3 targetOffset;
  
    public float averageDistance = 5f;

    public float maxDistance = 20f;

    public float minDistance = 0.6f;

    public float xSpeed = 200f;

    public float ySpeed = 200f;

    public int yMinLimit = -80;

    public int yMaxLimit = 80;
    public CanvasGroup _Canvas;
    public float EndLook=60, startLook=50;
 //   public int zoomSpeed = 40;

  //  public float panSpeed = 0.3f;

    public float zoomDampening = 5f;

 //   public float rotateOnOff = 1f;

    public float xDeg;

    //public float yDeg;

    private float currentDistance;

    public float desiredDistance;

    private Quaternion currentRotation;

    Quaternion desiredRotation;

    Quaternion rotation;

    Vector3 position;

    //private float idleTimer;

    private float idleSmooth;

   

 

	public void SetMen()
	{
		Init();
	}

	public void Init()
	{
		if (!targetObject)
		{
			GameObject gameObject = new GameObject("Cam Target");
			gameObject.transform.position = base.transform.position + base.transform.forward * averageDistance;
			targetObject = gameObject.transform;
		}
		currentDistance = averageDistance;
		desiredDistance = averageDistance;
		position = base.transform.position;
		rotation = base.transform.rotation;
		currentRotation = base.transform.rotation;
		desiredRotation = base.transform.rotation;
		position = targetObject.position - (rotation * Vector3.forward * currentDistance + targetOffset);
        SetMianPos();
	}

    public void SetMianPos()
    {
	    isDragging = true;
	    StartCoroutine(SetPos(-130f, 13, 5f));
	    if (HHG_PlayerSelection.instance.CurrentPlayer!=null)
	    {
		    maxDistance = HHG_PlayerSelection.instance.CurrentPlayer.GetComponent<CarAngale>().maxDistance;
		    minDistance = HHG_PlayerSelection.instance.CurrentPlayer.GetComponent<CarAngale>().minDistance;
	    }
        isPanelOn = true;
        OFFanimater();
    }
    public void SetPlatePaintPos()
    {
 
        isDragging = true;
            StartCoroutine(SetPos(10, 15, 2.4f));
            maxDistance = 17;
            minDistance = 17;
            isrimSelect = false;
    }

   public void SetGoPos()
    {
	    isDragging = true;
	    isPanelOn = true;
        StartCoroutine(SetPos(0, 15, 0f));
        maxDistance = 5;
        minDistance = 5;
        isrimSelect = false;
        OFFanimater();
    }

    public void SetRimPos()
    {
	    isDragging = true;
        StartCoroutine(SetPos(-103.18f, 0, 3.4f));
        maxDistance = 15;
        minDistance = 15;
        isrimSelect = true;
    }
    
    IEnumerator SetPos(float  x, float y, float dd)
    {
        int loopCount = 20;
        float xStep = Mathf.Abs(((x - xDeg) / loopCount ));
        float yStep = Mathf.Abs(((y - HHG_PlayerSelection.instance.CurrentPlayer.GetComponent<CarAngale>().yDeg) / loopCount));
        float desiredDistanceStep = Mathf.Abs(((dd - desiredDistance) / loopCount));

        for (int i = 0; i < loopCount; i++)
        {
            yield return null;
            xDeg = Mathf.MoveTowards(xDeg, x, xStep);
            HHG_PlayerSelection.instance.CurrentPlayer.GetComponent<CarAngale>().yDeg = Mathf.MoveTowards(HHG_PlayerSelection.instance.CurrentPlayer.GetComponent<CarAngale>().yDeg, y, yStep);
            desiredDistance = Mathf.MoveTowards(desiredDistance, dd, desiredDistanceStep);
        }
    }

    public  bool isDragging;
    public bool isrimSelect;
    public float AutoSpeed=0.05f;
    private void LateUpdate()
	{
        if (isDragging)
        {
			xDeg += CnControls.CnInputManager.GetAxis("Mouse X") * xSpeed * 0.04f;
            xDeg = ClampAngle(xDeg, -360, 360);
            HHG_PlayerSelection.instance.CurrentPlayer.GetComponent<CarAngale>().yDeg -= CnControls.CnInputManager.GetAxis("Mouse Y") * ySpeed * 0.04f;
            HHG_PlayerSelection.instance.CurrentPlayer.GetComponent<CarAngale>().yDeg = ClampAngle(HHG_PlayerSelection.instance.CurrentPlayer.GetComponent<CarAngale>().yDeg, yMinLimit, yMaxLimit);
			desiredRotation = Quaternion.Euler(HHG_PlayerSelection.instance.CurrentPlayer.GetComponent<CarAngale>().yDeg, xDeg, 0f);
			currentRotation = base.transform.rotation;
			rotation = Quaternion.Lerp(currentRotation, desiredRotation, 0.02f * zoomDampening);
			base.transform.rotation = rotation;
			//idleTimer = 0f;
			idleSmooth = 0f;
		}
		else if(!isrimSelect)
		{
            xDeg +=xSpeed * AutoSpeed * Time.deltaTime;
            HHG_PlayerSelection.instance.CurrentPlayer.GetComponent<CarAngale>(). yDeg = ClampAngle(HHG_PlayerSelection.instance.CurrentPlayer.GetComponent<CarAngale>().yDeg, yMinLimit, yMaxLimit);
			desiredRotation = Quaternion.Euler(HHG_PlayerSelection.instance.CurrentPlayer.GetComponent<CarAngale>().yDeg, xDeg, 0f);
			currentRotation = base.transform.rotation;
			rotation = Quaternion.Lerp(currentRotation, desiredRotation, 0.02f * zoomDampening * 2f);
			base.transform.rotation = rotation;
		}
		//desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * 0.02f * (float)zoomSpeed * Mathf.Abs(desiredDistance);
		desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
		currentDistance = Mathf.Lerp(currentDistance, desiredDistance, 0.02f * zoomDampening);
		position = targetObject.position - (rotation * Vector3.forward * currentDistance + targetOffset);
		base.transform.position = position;
	}

	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

    /*public void OnBeginDrag()
    {
        isDragging = true;
    }*/
    /*public void OnEndrag()
    {
        isDragging = false;
    }*/
    
    public bool isPanelOn = false;
    public float timer = 60f;
   

       
  
     
    
   

    
    void Update()
    {
	    if (DragCheck)
	    {
		    if (CameraObject)
		    {
			    if (CameraObject.fieldOfView > startLook)
			    {
				    CameraObject.fieldOfView -= 10f * Time.deltaTime;
			    }
			    if (_Canvas && _Canvas.alpha > 0)
			    {
				    _Canvas.alpha -= 0.05f;
				    isDragging = true;
			    }
		    }
	    }
	    else
	    {
		    if (CameraObject)
		    {
			    if (CameraObject.fieldOfView < EndLook)
			    {
				    CameraObject.fieldOfView += 10f * Time.deltaTime;
			    }

			    if (_Canvas && _Canvas.alpha < 1)
			    {
				    _Canvas.alpha += 0.05f;
				    isDragging = false;
			    }
		    }
	    }
	    /*if (!isPanelOn)
	    {
		    timer -= Time.deltaTime;
		    if (timer <= 0f)
		    {
			    onanimater();
		    }
	    }*/
    }
    public  void ResetTimer()
    {
	    timer = 5f;
    }
    private bool DragCheck = false;
    
    public void OnBeginDrag(bool DragValue)
    {
	    DragCheck = DragValue;
	    isDragging = DragValue;
	    isPanelOn = DragValue;
    }

    private void onanimater()
    {
	    //GetComponent<Animator>().enabled = true;
    }
    public void OFFanimater()
    {
	   // GetComponent<Animator>().enabled = false;
    }
}
