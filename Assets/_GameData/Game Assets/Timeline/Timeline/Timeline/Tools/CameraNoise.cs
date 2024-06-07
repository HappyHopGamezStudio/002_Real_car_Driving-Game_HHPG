using UnityEngine;

public class CameraNoise : MonoBehaviour
{
    public float steadyCamEffect;

    void Start()
    {
        
    }

    private static float noiseTimer;
    private static Vector3 noisePosOffset;
    private static Vector3 noiseRotOffset;
    private static Vector3 noiseTargetPosOffset;
    private static Vector3 noiseTargetRotOffset;
    private static Vector3 noiseCamPosVel;
    private static Vector3 noiseCamRotVel;

    //Apply noise effect (steadycam). This is better looking than using a multi Perlin noise.
    public void ApplyNoise(float magnitude, float weight)
    {
        var posMlt = Mathf.Lerp(0.2f, 0.4f, magnitude);
        var rotMlt = Mathf.Lerp(5, 10f, magnitude);
        var damp = Mathf.Lerp(3, 1, magnitude);
        if (noiseTimer <= 0)
        {
            noiseTimer = Random.Range(0.2f, 0.3f);
            noiseTargetPosOffset = Random.insideUnitSphere * posMlt;
            noiseTargetRotOffset = Random.insideUnitSphere * rotMlt;
        }
        noiseTimer -= Time.deltaTime;

        noisePosOffset = Vector3.SmoothDamp(noisePosOffset, noiseTargetPosOffset, ref noiseCamPosVel, damp);
        noiseRotOffset = Vector3.SmoothDamp(noiseRotOffset, noiseTargetRotOffset, ref noiseCamRotVel, damp);

        //Noise is applied as a local offset to the RenderCamera directly
        transform.localPosition = Vector3.Lerp(Vector3.zero, noisePosOffset, weight);
        transform.localEulerAngles = (Vector3.Lerp(Vector3.zero, noiseRotOffset, weight));
        //transform.SetLocalEulerAngles(Vector3.Lerp(Vector3.zero, noiseRotOffset, weight));
    }
    
    void Update()
    {
        if (steadyCamEffect > 0)
        {
            ApplyNoise(steadyCamEffect, 1.0f);
        }
    }
}
