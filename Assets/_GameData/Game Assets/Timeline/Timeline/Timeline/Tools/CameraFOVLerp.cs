using UnityEngine;

public class CameraFOVLerp : MonoBehaviour
{
    public Camera Cam;
    [Space]
    public float targetFOV;
    [Space]
    public float initialWait;
    public float timeToLerp;

    float initialFov;
    float finalFov;
    float currentFov;
    float t;
    bool AllowLerp;

    private void Start()
    {
        AllowLerp = false;
        Invoke(nameof(TriggerLerp), initialWait);
    }

    void TriggerLerp()
    {
        initialFov = Cam.fieldOfView;
        finalFov = targetFOV;
        t = 0;
        AllowLerp = true;
    }

    void Update ()
	{
        if (AllowLerp)
        {
            t += Time.deltaTime / timeToLerp;
            currentFov = Mathf.Lerp(initialFov, finalFov, t);

            Cam.fieldOfView = currentFov;
        }

        if (t >= 1)
        {
            AllowLerp = false;
            t = 0;
        }
    }

}