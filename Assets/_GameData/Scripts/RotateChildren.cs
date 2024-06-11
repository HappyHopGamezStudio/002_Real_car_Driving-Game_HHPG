using UnityEngine;

public class RotateChildren : MonoBehaviour
{
    // Rotation speed in degrees per second
    public float rotationSpeed = 30f;

    void Update()
    {
        // Rotate each child object around the Y-axis
        foreach (Transform child in transform)
        {
            child.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}