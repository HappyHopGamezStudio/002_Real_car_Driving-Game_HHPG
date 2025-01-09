using UnityEngine;

public class CarStateHandler : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;
    private bool isSaved = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SaveState()
    {
        if (rb != null)
        {
            savedVelocity = rb.velocity;
            savedAngularVelocity = rb.angularVelocity;
            isSaved = true;
        }
    }

    public void RestoreState()
    {
        if (rb != null && isSaved)
        {
            rb.velocity = savedVelocity;
            rb.angularVelocity = savedAngularVelocity;
        }
    }
}