using UnityEngine;
using System.Collections;

public class SetBehaviourState : MonoBehaviour
{
    //public bool GetBehaviourAutomatically = true;
    public float initialWait;
    public bool SetToState;
    [Space]
    [Multiline]
    public string Description;
    [Space]
    public MonoBehaviour Behaviour;
    
    
    public void Reset()
    {
        //Behaviour = gameObject.GetComponent<Behaviour>();        
    }

    private void Start()
    {
        StartCoroutine(SetBehaviour(SetToState, initialWait));
    }

    IEnumerator SetBehaviour(bool state, float delay)
    {
        yield return new WaitForSeconds(delay);
        Behaviour.enabled = state;
    }
}