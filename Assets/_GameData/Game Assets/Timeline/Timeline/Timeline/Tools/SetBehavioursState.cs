using UnityEngine;
using System.Collections;

public class SetBehavioursState : MonoBehaviour
{
    //public bool GetBehaviourAutomatically = true;
    public float initialWait;
    public bool SetToState;
    [Space]
    [Multiline]
    public string Description;
    [Space]
    public MonoBehaviour[] Behaviours;
    
    
    public void Reset()
    {
        //if (GetBehaviourAutomatically)
        //{
        //    int childCount = transform.childCount;

        //    Transform[] AllChilds = new Transform[childCount];
        //    Behaviours = new MonoBehaviour[childCount];

        //    for (int i = 0; i < childCount; i++)
        //    {
        //        AllChilds[i] = transform.GetChild(i);
        //        Behaviours[i] = AllChilds[i].GetComponent<MonoBehaviour>();
        //    }

        //    //Behaviours = transform.GetComponentsInChildren<Behaviour>();
        //}
    }

    private void Start()
    {
        StartCoroutine(SetBehaviours(SetToState, initialWait));
    }

    IEnumerator SetBehaviours(bool state, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (MonoBehaviour behaviour in Behaviours)
        {
            behaviour.enabled = state;
        }
    }

    public void SetBehavioursMethod(bool state)
    {
        foreach (MonoBehaviour behaviour in Behaviours)
        {
            behaviour.enabled = state;
        }
    }
}