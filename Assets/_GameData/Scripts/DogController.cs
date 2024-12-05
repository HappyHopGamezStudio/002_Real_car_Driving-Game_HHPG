using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DogController : MonoBehaviour
{
    public Transform player;            // Reference to the player
    public Transform ball;              // Reference to the ball
    public float rightSideOffset = 1f;  // Distance to the right side of the player
    public float followSpeed = 3f;      // Speed when the dog is walking beside the player
    public float runSpeed = 6f;         // Speed when the dog is running beside the player
    public AudioClip howlSound;         // Sound for the dog howl when fetching ball
    public Animator dogAnimator;        // Animator to control the dog's animations
    public float playerWalkSpeed = 1f;  // Threshold for player speed to walk
    public float playerRunSpeed = 3f;   // Threshold for player speed to run

    private bool isFetchingBall = false;
    private Vector3 ballPosition;
    private AudioSource audioSource;
    private CharacterController playerController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = player.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isFetchingBall)
        {
            AdjustDogBehaviorBasedOnPlayerSpeed();
            AlignWithPlayerDirection();
          
        }
    }

    // Adjust the dog's behavior based on the player's speed
    void AdjustDogBehaviorBasedOnPlayerSpeed()
    {
        float playerSpeed = new Vector3(playerController.velocity.x, 0, playerController.velocity.z).magnitude;

        if (playerSpeed > playerRunSpeed)
        {
            // Player is running; set dog to run speed
            MoveToRightSideOfPlayer(runSpeed);
            dogAnimator.SetFloat("Speed", 2f); // Run
        }
        else if (playerSpeed > playerWalkSpeed)
        {
            // Player is walking; set dog to walk speed
            MoveToRightSideOfPlayer(followSpeed);
            dogAnimator.SetFloat("Speed", 1f); // Walk
        }
        else
        {
            // Player is idle; set dog to idle
            MoveToRightSideOfPlayer(followSpeed);
            dogAnimator.SetFloat("Speed", 0f); // Idle
        }
    }

    // Make the dog move to the player's right side and follow at the specified speed
    void MoveToRightSideOfPlayer(float speed)
    {
        Vector3 rightSidePosition = player.position + player.right * rightSideOffset;
        transform.position = Vector3.MoveTowards(transform.position, rightSidePosition, speed * Time.deltaTime);
    }

    // Align the dog to face the same direction as the player
    void AlignWithPlayerDirection()
    {
        if (!isFetchingBall)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, Time.deltaTime * followSpeed);
        }
    }

    // Make the dog jump when the player jumps
    public void CheckForJump()
    {
        dogAnimator.SetTrigger("Jump");
    }

    // Throw the ball and make the dog fetch it

    
    public  void ThrowBall()
    {
      //  OnButtonClick();
        trowBall();
        DogButton.gameObject.SetActive(false);
        
    }
    public float cooldownTime = 1f; // Duration for the button to remain disabled

    public Button DogButton;
    
   /* void OnButtonClick()
    {
        // Disable the button immediately when clicked
        DogButton.GetComponent<Button>().interactable = false;
        // Strt the cooldown timer
        StartCoroutine(EnableButtonAfterCooldown());
    }

    // Coroutine to enable the button after the cooldown period
    IEnumerator EnableButtonAfterCooldown()
    {
        // Wait for the specified cooldown time (in seconds)
        yield return new WaitForSeconds(cooldownTime);

        // Re-enable the button
        DogButton.GetComponent<Button>().interactable = true;
       
    }*/
    void trowBall()
    {
        if (isFetchingBall) return; // Prevent multiple fetches at the same time

        // Play howl sound
        audioSource.PlayOneShot(howlSound);

        // Set the ball position and start fetch routine
        ballPosition = ball.position;
        StartCoroutine(FetchBall());
    }
    IEnumerator FetchBall()
    {
        isFetchingBall = true;
        dogAnimator.SetFloat("Speed", 2f); // Running animation for fetch

        // Move the dog towards the current position of the ball
        while (Vector3.Distance(transform.position, ball.position) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, ball.position, runSpeed * Time.deltaTime);
            transform.LookAt(ball.position); // Look at the current position of the ball
            yield return null;
        }

        // Wait briefly at the ball
        yield return new WaitForSeconds(1f);

        // Return to the player and stand on the right side
        while (Vector3.Distance(transform.position, player.position + player.right * rightSideOffset) > 0.5f)
        {
            Vector3 returnPosition = player.position + player.right * rightSideOffset;
            transform.position = Vector3.MoveTowards(transform.position, returnPosition, runSpeed * Time.deltaTime);
            transform.LookAt(player.position);
            yield return null;
        }

        dogAnimator.SetFloat("Speed", 0f); // Idle after fetch
        isFetchingBall = false;
        DogButton.gameObject.SetActive(true);
    }
}
