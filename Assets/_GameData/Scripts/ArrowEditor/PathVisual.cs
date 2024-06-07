using UnityEngine;
using System.Collections;

public class PathVisual : MonoBehaviour
{
    public Color pathColor = new Color(1, 0.5f, 0);
    public Transform ArrowPrefabs;
    public float PathLength = 0;
    public int segmentsToCreate;
    public float Distance = 5f;

    private NodeVisual nodeComponent;
    Vector3 instantiatePosition;
    float lerpValue;
    float distance;
    Transform Arrow;
    Transform parent;
    Transform nextNode, beforeNode;

    public void GenerateArrows()
    {
        for (int nodeId = 0; nodeId < transform.childCount; nodeId++)
        {
            if (nodeId >= 0 && nodeId < transform.childCount - 1)
            {
               // PathLength += Vector3.Distance(transform.GetChild(nodeId - 1).position, transform.GetChild(nodeId).position);
                //  Debug.Log("NOde Value " + nodeId);
                InstantiateSegments(transform.GetChild(nodeId).position, transform.GetChild(nodeId + 1).position);

            }



        }

    }

    public void GenerateNewPoint(Vector3 pos) {

        GameObject NewPoint = new GameObject(transform.childCount+"");
        NewPoint.transform.position = pos;
        NewPoint.transform.rotation = Quaternion.identity;
        NewPoint.AddComponent<NodeVisual>();
        NewPoint.AddComponent<BoxCollider>();
        NewPoint.transform.SetParent(transform);
    }

    void OnDrawGizmos()
    {
        //int nodeId = 1;
        Gizmos.color = pathColor;
        PathLength = 0;
       // if (DrawArrow) {

            //InstantiateSegments(transform.GetChild(0).position, transform.GetChild(1).position);
      //  }
        for (int nodeId=0; nodeId < transform.childCount; nodeId++ )
        {
            nodeComponent = transform.GetChild(nodeId).GetComponent<NodeVisual>();

            if (nodeId == 1)
            {
                nodeComponent.firistNode = true;
                nodeComponent.lastNode = false;
            }
            else if (nodeId == transform.childCount)
            {
                nodeComponent.firistNode = false;
                nodeComponent.lastNode = true;
            }
            else
            {
                nodeComponent.firistNode = false;
                nodeComponent.lastNode = false;
            }
            //if (nodeId  >= 0 && nodeId < transform.childCount-1)
            //{
            //  //  Debug.Log(Vector3.Distance(transform.GetChild(nodeId-1).position, transform.GetChild(nodeId).position));
            
            //    //  PathLength += Vector3.Distance(transform.GetChild(nodeId - 1).position, transform.GetChild(nodeId).position);

            //    if (DrawArrow) {
            //        Debug.Log("NOde Value "+nodeId);
            //        InstantiateSegments(transform.GetChild(nodeId).position, transform.GetChild(nodeId+1).position);
            //        //Instantiate(ArrowPrefabs, transform.GetChild(nodeId-1).position, transform.GetChild(nodeId - 1).rotation);
            //        if (nodeId == transform.childCount - 2)
            //             DrawArrow = false;
            //    }
            //}

            if (!nodeComponent)
            {
                transform.GetChild(nodeId).gameObject.AddComponent<NodeVisual>();
                nodeComponent.nodeColor = pathColor;
               
            }
            else
            {
                nodeComponent.nodeColor = pathColor;
               
            }
            //Assigning Name of Child
            if (transform.GetChild(nodeId).name != (nodeId+1).ToString())
                transform.GetChild(nodeId).name = (nodeId + 1).ToString();

            if (nodeId > 0)
            {
                 
                 beforeNode = transform.GetChild((nodeId - 1));
                nodeComponent.previousNode = beforeNode;
            }
            if (nodeId < transform.childCount - 1)
            {    nextNode = transform.GetChild(nodeId + 1);
                Gizmos.DrawLine(transform.GetChild(nodeId).position, nextNode.position);
                nodeComponent.nextNode = nextNode;
            }
           
        }
    }

 

    void InstantiateSegments(Vector3 pointA,Vector3 pointB)
    {
        // Debug.Log(" " + pointA + "  " + pointB);
        if (parent == null)
        {
            parent = new GameObject().transform;
            parent.name = "Arrows";

            parent.parent = transform.parent;
        }
        
        lerpValue = 0;
        //Here we calculate how many segments will fit between the two points
        segmentsToCreate = Mathf.RoundToInt(Vector3.Distance(pointA, pointB) / Distance);
        //As we'll be using vector3.lerp we want a value between 0 and 1, and the distance value is the value we have to add
        if (segmentsToCreate > 0)
        {
            distance = 0.9f / segmentsToCreate;
            for (int i = 0; i < segmentsToCreate; i++)
            {
                //We increase our lerpValue
                lerpValue += distance;
                // Debug.Log("Lerp value "+lerpValue);
                //Get the position
                instantiatePosition = Vector3.Lerp(pointA, pointB, lerpValue);
                // Debug.Log("Lerp value " + instantiatePosition);
                //Instantiate the object
                Arrow = Instantiate(ArrowPrefabs, instantiatePosition, transform.rotation);
                Arrow.LookAt(pointB);
                Arrow.transform.name = "ChildArrow";
                Arrow.parent = parent;
            }
        }
        else {
            Debug.Log("Check");
            instantiatePosition = Vector3.Lerp(pointA, pointB, 0.5f);
            Arrow = Instantiate(ArrowPrefabs, instantiatePosition, transform.rotation);

            Arrow.LookAt(pointB);
            Arrow.parent = parent;

        }

    }
}
