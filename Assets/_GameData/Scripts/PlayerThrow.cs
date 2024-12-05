using UnityEngine;
using StarterAssets;
using System.Collections;

public class PlayerThrow : MonoBehaviour
{
    public GameObject ballPrefab; // Assign the ball prefab in the inspector
    public float throwForce = 15f; // Adjust the throw force as needed
    public float throwDelay = 2f; // The delay before the ball is thrown
    public Transform throwPoint; 
    private Animator _animator;
    private StarterAssetsInputs _input;
    public DogController DogController;
   
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (_input.Throw) // Check if the throw input was triggered
        {
            _input.Throw = false; // Reset the input
            StartCoroutine(ThrowBallWithDelay()); // Start the throw coroutine
            // Start the throw coroutine
        }
    }

    public void SitOnBike()
    {
        _animator.SetBool("SitBike", true); // Start the "SitBike" animation
    }

    private void PlayThrowAnimation()
    {
        _animator.SetTrigger("Throw"); // Trigger the throw animation
    }

    private IEnumerator ThrowBallWithDelay()
    {
        PlayThrowAnimation(); // Play the throw animation immediately

        // Wait for the specified delay time
        yield return new WaitForSeconds(throwDelay);

        // Now, instantiate the ball and throw it after the delay
        GameObject thrownBall = Instantiate(ballPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = thrownBall.GetComponent<Rigidbody>();
        DogController.ball = thrownBall.transform;
        // Add force to throw the ball in the player's forward direction
        Vector3 throwDirection = transform.forward;
        rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
       Invoke("callFacteh",1f);
       

    }

    void callFacteh()
    {
        DogController.ThrowBall();
    }
}