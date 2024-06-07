using UnityEngine;

public class HHG_CarShadow: MonoBehaviour
{
    private Transform thisTransform;
    private float distance = 10f;
    private Material material;
    public Transform ombrePlane;
    //	private Transform waterSlip;
    //	private AudioSource audioWater;
    public LayerMask layerMask = 57856;
    private Vector3 newPosition;
    private Color newColor;
    private RaycastHit hit;

    private void Start()
    {
        newPosition = Vector3.zero;
        //ombrePlane = GameObject.Find("CarShadow").transform;
        thisTransform = base.transform;
        material = ombrePlane.GetComponent<Renderer>().material;
        newColor = material.color;
    }

    private bool isHorizontale(Vector3 normal)
    {
        if (normal.y > 0.5f && Mathf.Abs(normal.x) < 0.5f && Mathf.Abs(normal.z) < 0.5f)
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Physics.Raycast(thisTransform.position + Vector3.up, -Vector3.up, out hit, 20f, layerMask))
        {
            distance = hit.distance;
            //Debug.Log("if");
        }
        else
        {
            distance = 20f;
            //Debug.Log("else");
        }
        newPosition = thisTransform.position + Vector3.up;
        newPosition.y -= distance - 0.06f;
        ombrePlane.position = newPosition;
        ombrePlane.transform.up = Utils.SmoothVector(ombrePlane.transform.up, hit.normal, 6f);
        ombrePlane.Rotate(0f, thisTransform.localEulerAngles.y, 0f, Space.Self);
        newColor.a = Mathf.Lerp(0.9f, 0.3f, hit.distance / 20f);
        // material.color = newColor;
        //waterSlip.position = thisTransform.position;
        //waterSlip.rotation = thisTransform.rotation;

    }

}