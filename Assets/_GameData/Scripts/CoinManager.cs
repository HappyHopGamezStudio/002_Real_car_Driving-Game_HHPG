using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    public Text coinsText; 
    public float initialSpeed = 0.1f; 
    public float speedDecrement = 0.01f;

    private int currentCoins = 0;
    private float currentSpeed;

    public void AddingCoins()
    {
        currentSpeed = initialSpeed;
        StartCoroutine(AddCoins());
    }
    IEnumerator AddCoins()
    {
        while (currentCoins < PrefsManager.GetCoinsValue())
        {
            currentCoins++;
            coinsText.text = currentCoins.ToString();

            // Decrease the speed
            currentSpeed -= speedDecrement;
            if (currentSpeed < 0.01f*5*Time.deltaTime) // Prevent speed from becoming too low
            {
                currentSpeed = 0.01f;
            }

            yield return new WaitForSeconds(currentSpeed);
        }
    }
}