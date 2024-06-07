using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHG_CameraRotation : MonoBehaviour
{

    public Transform camObj;
    public Transform target;
    public float speed;
    Vector3 prevPos;
    public  bool isMainMenu;

    // Update is called once per frame
    void Update()
    {
        camObj.rotation = Quaternion.Slerp(camObj.rotation, target.rotation, 10 * Time.deltaTime);
        if (Input.GetMouseButtonDown(0) && !isMainMenu) 
        {
            prevPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && !isMainMenu)
        {
            float deltaX = Input.mousePosition.x - prevPos.x;
            deltaX /= 5;
            target.Rotate(0, deltaX, 0);
            prevPos = Input.mousePosition;
        }
        else
        {
            
            {
                target.Rotate(0, speed * Time.deltaTime, 0);  
            }
            
        }
       
    }


    public void ExitMainMenu(bool value)
    {
        isMainMenu = value;
    }
}
