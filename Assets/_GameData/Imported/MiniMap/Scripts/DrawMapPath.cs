using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrawMapPath : MonoBehaviour
{
    NavMeshPath path;
    public Transform Target;
    public LineRenderer line;
    public NavMeshAgent agent;
    NavMeshHit hit;
    int RoadArea;
    public Vector3[] aa;
    public int totalPositions;
    public static DrawMapPath _instance;
    bool isTaxi;
    public float waitTime = 2f;
    float counter;

    private void Awake()
    {
        _instance = this;
        isTaxi = false;
    }
    public void ResetTarget()
    {
        Target = this.transform;
        isTaxi = true;
    }
   
    public void SetDestinationAtFirst(Transform startTrans)
    {
        if (line==null)
        {
           // line = GameManager.Instance.Line;
        }
        path = agent.path;
        NavMesh.CalculatePath(startTrans.position, Target.position, RoadArea, path); //Saves the path in the path variable.
        line.positionCount = path.corners.Length;
        line.SetPositions(path.corners);
    }
    private void Update()
    {
        counter++;

        if (Target)
        {
            if (counter > waitTime)
            {
                counter = 0;
                RoadArea = 1 << NavMesh.GetAreaFromName("Road");
                if (NavMesh.SamplePosition(transform.position, out hit, 2.0f, RoadArea))
                {
                    //Debug.Log("If "+Target.name);
                    path = agent.path;
                    NavMesh.CalculatePath(transform.position, Target.position, RoadArea, path); //Saves the path in the path variable.

                    if (line==null)
                    {
                       return; 
                    }
                    line.positionCount = path.corners.Length;
                    line.SetPositions(path.corners);

                    if (path.corners.Length > 0)
                    {
                        totalPositions = path.corners.Length;
                        aa = path.corners;
                    }
                }
                else
                {
                   // Debug.Log("else " + Target.name);
                    line.positionCount = totalPositions;
                    line.SetPositions(aa);
                }
            }
        }
    }
}
