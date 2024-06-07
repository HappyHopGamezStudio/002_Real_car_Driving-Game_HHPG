using UnityEngine;

public class CameraLerp : MonoBehaviour
{
	public Transform target;
    [Space]
    bool AllowLerp;
    public bool AdoptRotation = true;
    [Space]
    public float initialWait;
    public float StartTime;
    public float EndTime;

    float timeToLerp;
    Vector3 initialPos;
    Quaternion initialRot;
    float t;

    private void Start()
    {
        AllowLerp = false;
        initialPos = gameObject.transform.position;
        initialRot = gameObject.transform.rotation;
        t = 0;

        timeToLerp = EndTime - StartTime;
        Invoke(nameof(TriggerLerp), initialWait);
    }

    void TriggerLerp()
    {
        initialPos = gameObject.transform.position;
        initialRot = gameObject.transform.rotation;
        t = 0;
        AllowLerp = true;
    }

    void Update ()
	{
        if (AllowLerp)
        {
            t += Time.deltaTime / timeToLerp;
            transform.position = Vector3.Lerp(initialPos, target.position, t);
            if (AdoptRotation) { transform.rotation = Quaternion.Lerp(initialRot, target.rotation, t); }
        }

        if (t >= 1)
        {
            AllowLerp = false;
            t = 0;
        }
    }

}