using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{

    public Transform camObj;
    public Transform target;
    public float speed,MultipleValue=1f;
    public Camera CameraObject;
    public CanvasGroup _Canvas;
    Vector3 prevPos;
    public float EndLook=60, startLook=50;
    private float deltaX;

    public bool ismainmenu=false;
    // Update is called once per frame



    private void Start()
    {
      
    }



    void Update()
    {
        if (ismainmenu)
        {
            MultipleValue = 0;
        }
        else if (!ismainmenu)
        {
            MultipleValue = 1;
        }

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
                }
            }
        }


        camObj.rotation = Quaternion.Slerp(camObj.rotation, target.rotation, 10 * Time.deltaTime);
        if (Input.GetMouseButtonDown(0) && DragCheck) 
        {
         prevPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && DragCheck)
        { 
            deltaX = Input.mousePosition.x - prevPos.x;
            deltaX /= 5;
            target.Rotate(0,  deltaX*MultipleValue, 0*Time.deltaTime* 10f * Time.timeScale);
            prevPos = Input.mousePosition;
        }
        else
        {
            target.Rotate(0, speed * Time.deltaTime, 0);
        }

      
       
    }

    private bool DragCheck = false;
    public void Drag(bool DragValue)
    {
        DragCheck = DragValue;
    }

}
