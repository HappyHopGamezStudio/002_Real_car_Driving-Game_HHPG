using UnityEngine;
using UnityEngine.UI;

public class CarJumpChecker : MonoBehaviour
{
    
    
    public float currentHeight = 100f;
    
    public float jumpHeight100m = 100f;
    public float jumpHeight200m = 200f;
    public float jumpHeight500m = 500f;

    private bool currenthight = false;
    private bool hasJumped100m = false;
    private bool hasJumped200m = false;
    private bool hasJumped500m = false;
    public Text Jump;
    
    
    public Text hightCounter;
    
    void Update()
    {
         currentHeight = transform.position.y;
         hightCounter.text = currentHeight.ToString("");
         if (!currenthight && currentHeight >= 0)
         {
             Jump.text = "";
            
             currenthight = true;
         }
        if (!hasJumped100m && currentHeight >= jumpHeight100m)
        {
            Jump.text = "Little Jump!";
            Debug.Log("Little Jump!");
            hasJumped100m = true;
        }
        
        if (!hasJumped200m && currentHeight >= jumpHeight200m)
        {
            Jump.text = "Great Jump!";
            Debug.Log("Great Jump!");
            hasJumped200m = true;
        }
        
        if (!hasJumped500m && currentHeight >= jumpHeight500m)
            
        {
            Jump.text = "Great Jump!";
            Debug.Log("Great Jump!");
            hasJumped500m = true;
        }
    }
}