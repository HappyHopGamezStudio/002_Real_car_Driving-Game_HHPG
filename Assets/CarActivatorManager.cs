using System.Collections.Generic;
using UnityEngine;

public class CarActivatorManager : MonoBehaviour
{
    public float detectionRadius = 100f; // Radius to check for cars
    public GameObject[] allCars;       // Array to hold all cars
    private HashSet<GameObject> activeCars = new HashSet<GameObject>(); // Track active cars

    private void Awake()
    {
        detectionRadius = 5000f;
        // Find all cars in the scene
       // allCars = GameObject.FindGameObjectsWithTag("Player"); // Make sure all cars have the "Car" tag
        Invoke(nameof(againSet),3f);
        
    }

   void againSet()
    {
        detectionRadius = 150f;
    }
    private void Update()
    {
        foreach (GameObject car in allCars)
        {
            if (car == null) continue;

            float distance = Vector3.Distance(transform.position, car.transform.position);

            // If car is within range
            if (distance <= detectionRadius)
            {
                if (!activeCars.Contains(car)) // If not already active
                {
                    CarStateHandler stateHandler = car.GetComponent<CarStateHandler>();
                    if (stateHandler != null)
                        stateHandler.RestoreState();

                    car.SetActive(true);
                    activeCars.Add(car);
                }
            }
            // If car is out of range
            else
            {
                if (activeCars.Contains(car)) // If currently active
                {
                    CarStateHandler stateHandler = car.GetComponent<CarStateHandler>();
                    if (stateHandler != null)
                        stateHandler.SaveState();

                    car.SetActive(false);
                    activeCars.Remove(car);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the detection radius in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}