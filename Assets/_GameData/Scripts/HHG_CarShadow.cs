using System;
using UnityEngine;

public class HHG_CarShadow: MonoBehaviour
{
    private Transform thisTransform;
    private float distance = 10f;
    private Material material;
    public Transform ombrePlane;
    public LayerMask layerMask = 57856;
    private Vector3 newPosition;
    private Color newColor;
    private RaycastHit hit;
    
    private void Start()
    {
        newPosition = Vector3.zero;
        thisTransform = base.transform;
        material = ombrePlane.GetComponent<Renderer>().material;
        newColor = material.color;
    }

   

    public float setpos = 0.07f;
    private void Update()
    {
        if (Physics.Raycast(thisTransform.position + Vector3.up, -Vector3.up, out hit, 20f, layerMask))
        {
            distance = hit.distance;
        }
        else
        {
            distance = 20f;
        }
        newPosition = thisTransform.position + Vector3.up;
        newPosition.y -= distance - setpos;
        ombrePlane.position = newPosition;
        ombrePlane.transform.up = Utils.SmoothVector(ombrePlane.transform.up, hit.normal, 6f);
        ombrePlane.Rotate(0f, thisTransform.localEulerAngles.y, 0f, Space.Self);
        newColor.a = Mathf.Lerp(0.9f, 0.3f, hit.distance / 20f);
    }

    private void OnDisable()
    {
        ombrePlane.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ombrePlane.gameObject.SetActive(true);
    }
}