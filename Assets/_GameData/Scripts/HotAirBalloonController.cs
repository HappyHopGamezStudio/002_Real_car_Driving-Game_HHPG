using System.Collections;
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
        // Set the initial height and pick the first target position
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        SetNewTargetPosition();

        // Start the movement coroutine
        StartCoroutine(MoveBalloon());
    }

    private IEnumerator MoveBalloon()
    {
        while (true)
        {
            // Move the balloon towards the target position
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                MoveTowardsTarget();
                yield return null; // Wait for the next frame
            }

            // Wait for the interval before changing direction
            yield return new WaitForSeconds(directionChangeInterval);

            // Set a new target position
            SetNewTargetPosition();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Lock the Y-axis to maintain constant height
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }

    private void SetNewTargetPosition()
    {
        // Pick a new random target position within the bounds
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        targetPosition = new Vector3(randomX, height, randomZ);
    }
}