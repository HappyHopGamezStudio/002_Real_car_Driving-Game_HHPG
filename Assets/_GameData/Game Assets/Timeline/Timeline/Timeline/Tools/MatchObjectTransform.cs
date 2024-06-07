using UnityEngine;

public class MatchObjectTransform : MonoBehaviour
{
    public float initialWait;
    [Space]
    [Multiline]
    public string Description;
    [Space]
    public Transform actorObject;
    [Space]
    public Transform targetObject;
    [Space]
    public bool matchPosition = true;
    public Vector3 positionOffset;
    public bool matchRotation = true;
    public Vector3 rotationOffset;
    public bool matchScale = false;
    public Vector3 scaleOffset;
    
    void Start()
    {
        //StartCoroutine(MatchTransform(initialWait));
        Invoke(nameof(MatchTransform), initialWait);
    }

    //IEnumerator MatchTransform(float delay)
    public void MatchTransform()
    {
        if (matchPosition)
        {
            actorObject.position = targetObject.position + positionOffset;
        }

        if (matchRotation)
        {
            actorObject.rotation = targetObject.rotation * Quaternion.Euler(rotationOffset);
        }

        if (matchScale)
        {
            actorObject.localScale = targetObject.localScale + scaleOffset;
        }
    }
}