using UnityEngine;
using System.Collections;

public class NodeVisual : MonoBehaviour
{

    public Transform previousNode;
    public Transform nextNode;

    public float widthDistance = 5.0f; // width distance (Street)

    public Color nodeColor = Color.green;

    [HideInInspector]
    public bool firistNode, lastNode = false;

    void OnDrawGizmos()
    {

        
        Gizmos.color = nodeColor;

        Vector3 direction = transform.TransformDirection(Vector3.left);

        Gizmos.DrawRay(transform.position, direction * widthDistance);
        Gizmos.DrawRay(transform.position, direction * -widthDistance);
        Gizmos.DrawSphere(transform.position, 1);

        if (nextNode)
        {
            Vector3 directionLookAt = transform.position - nextNode.position;
            directionLookAt.y = 0;
            transform.rotation = Quaternion.LookRotation(directionLookAt);
        }
    }

   

}
