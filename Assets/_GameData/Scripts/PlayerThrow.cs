using UnityEngine;
using StarterAssets;
using System.Collections;
using System.Threading.Tasks;
using HHG_Mediation;

public class PlayerThrow : MonoBehaviour
{
    public GameObject ballPrefab; // Assign the ball prefab in the inspector
    public float throwForce = 15f; // Adjust the throw force as needed
    public float throwDelay = 2f; // The delay before the ball is thrown
    public Transform throwPoint; 
    private Animator _animator;
    private StarterAssetsInputs _input;
    public DogController DogController;
    public GameObject mobile,uimobile;
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
        if (_input.SitBike) // Check if the throw input was triggered
        {
            _input.SitBike = false; // Reset the input
            SitOnBike();
            // Start the throw coroutine
        }
       
    }

    public  void SitOnBike()
    {
        if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.ThirdPerson)
        {
            _animator.SetBool("SitBike", true);
            mobile.SetActive(true);
            uimobile.SetActive(true);
        }
        else
        {
            uimobile.SetActive(true);
            if (HHG_GameManager.Instance.CurrentCar != null)
            {
                HHG_GameManager.Instance.CurrentCar.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                HHG_GameManager.Instance.CurrentCar.transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
        if (PrefsManager.GetCurrentMission() >= HHG_LevelManager.instace.hhgOpenWorldManager.TotalMisson)
        { 
            PrefsManager.SetCurrentMission(0);
        }
        HHG_LevelManager.instace.isPanelOn = true;  
        HHG_UiManager.instance?.HideGamePlay();
        FindObjectOfType<HHG_AdsCall>().hideBigBanner();
    }
    public  void CallMission()
    {
        if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.ThirdPerson)
        {
            if (uimobile.activeSelf)
            {
                OffMobile();
            }
            _animator.SetBool("SitBike", true);
            mobile.SetActive(true);
            HHG_UiManager.instance?.CallPanel.SetActive(true);
        }
        else
        {
            if (uimobile.activeSelf)
            {
                OffMobile();
            }
            Time.timeScale = 0.1f;
            HHG_UiManager.instance?.CallPanel.SetActive(true);
        }
        if (PrefsManager.GetCurrentMission() >= HHG_LevelManager.instace.hhgOpenWorldManager.TotalMisson)
        { 
            PrefsManager.SetCurrentMission(0);
        }
        HHG_LevelManager.instace.isPanelOn = true;  
        HHG_UiManager.instance?.HideGamePlay();
    }

    public void OffMobile()
    {
        Time.timeScale = 1;
        if (HHG_GameManager.Instance.TpsStatus==PlayerStatus.ThirdPerson)
        {
            _animator.SetBool("SitBike", false); 
            mobile.SetActive(false);
            uimobile.SetActive(false);  
        }
        else
        {
            uimobile.SetActive(false);
        }
        HHG_UiManager.instance?.ShowGamePlay();
        FindObjectOfType<HHG_AdsCall>().hideBigBanner();
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
       // ballPrefab.GetComponent<DestroyAfter>().IsStart = true;
        DogController.ThrowBall();
    }
}