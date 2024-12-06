using UnityEngine;

public class BackgroundTilt : MonoBehaviour
{
    public RectTransform backgroundImage; // Assign your background image RectTransform
    public float tiltSensitivity = 5.0f; // Adjust sensitivity of the tilt effect
    public float maxTilt = 50f; // Maximum allowable tilt in pixels

    private Vector3 originalPosition;

    void Start()
    {
        // Store the original position of the background image
        if (backgroundImage != null)
            originalPosition = backgroundImage.localPosition;
    }

    void Update()
    {
        if (backgroundImage == null) return;

        // Get device tilt using accelerometer
        Vector3 acceleration = Input.acceleration;

        // Calculate the offset based on tilt
        float offsetX = Mathf.Clamp(acceleration.x * tiltSensitivity, -1f, 1f) * maxTilt;
        float offsetY = Mathf.Clamp(acceleration.y * tiltSensitivity, -1f, 1f) * maxTilt;

        // Apply the offset to the background image position
        backgroundImage.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);
    }
}