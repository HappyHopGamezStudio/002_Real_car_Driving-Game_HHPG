using UnityEngine;

public class HotAirBalloonController : MonoBehaviour
{
    public float minX; // Minimum X position of the movement area
    public float maxX; // Maximum X position of the movement area
    public float minZ; // Minimum Z position of the movement area
    public float maxZ; // Maximum Z position of the movement area
    public float height = 50f; // Fixed height for the balloons
    public float speed = 5f; // Movement speed
    public float directionChangeInterval = 3f; // Time interval to change direction

    private Vector3 targetPosition;

    void Start()
    {
        // Set an initial random target position within the area
        SetNewTargetPosition();
        // Maintain the balloon at the specified height
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }

    void Update()
    {
        // Move the balloon towards the target position
        MoveTowardsTarget();

        // If the balloon is close to the target, pick a new target position
        if (Vector3.Distance(transform.position, targetPosition) < 1f)
        {
            SetNewTargetPosition();
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, height, transform.position.z); // Lock the Y-axis
    }

    void SetNewTargetPosition()
    {
        // Pick a new random target position within the bounds
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        targetPosition = new Vector3(randomX, height, randomZ);
    }
}