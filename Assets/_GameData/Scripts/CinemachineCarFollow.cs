using UnityEngine;
using Cinemachine;

public class CinemachineCarFollow: MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Drag your Cinemachine camera here.
    public Transform carTransform; // Drag your RCC car or assign it dynamically in code.

    void Start()
    {
        if (virtualCamera != null && carTransform != null)
        {
            // Set the follow and look-at targets to the car's transform
            virtualCamera.Follow = carTransform;
            virtualCamera.LookAt = carTransform;
        }
    }

    public void SetCarTarget(Transform newCarTransform)
    {
        carTransform = newCarTransform;

        if (virtualCamera != null && carTransform != null)
        {
            virtualCamera.Follow = carTransform;
            virtualCamera.LookAt = carTransform;
        }
    }
}