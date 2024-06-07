using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameObject car;  
    public float shakeAmount = 0.1f;
    public float decreaseFactor = 1.0f;
    private Vector3 originalPos;
    private float shakeDuration = 0.0f;
   

    void Start()
    {
        originalPos = transform.localPosition;
    }
    
    void Update()
    {
        if(car!=null)
        {
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }

        if (shakeDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = originalPos;
        }
    }
}