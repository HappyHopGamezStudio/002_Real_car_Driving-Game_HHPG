using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform car; // The car to follow
    public Vector3 followOffset = new Vector3(0, 5, -10); // Offset from the car
    private Vector3 originalPosition; // To store the original position of the camera
    private Quaternion originalRotation; // To store the original rotation of the camera
    private bool isFollowing = false; // Whether the camera is currently following the car

    void Start()
    {
        // Store the original position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (isFollowing)
        {
            FollowCar();
        }
    }

    // Method to start following the car
    public void OnEnable()
    {
        isFollowing = true;
    }

    // Method to stop following and return to the original position
    public void OnDisable()
    {
        isFollowing = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    // Logic to follow the car
    private void FollowCar()
    {
        if (car != null)
        {
            transform.position = car.position + followOffset;
            transform.LookAt(car); // Make the camera look at the car
        }
    }
}