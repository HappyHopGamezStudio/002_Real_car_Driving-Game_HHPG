using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEmotes : MonoBehaviour
{
	public GameObject[] angerEmotePrefabs; 
//	public GameObject[] randomEmotePrefabs;
	public AudioClip[] AngerySounds;

	private int angerIndex = 0;
	public bool isCritical,normalhit;
	private int randomIndex;
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.CarDriving)
			{

				if (!forall)
				{

					// Determine if the hit is critical
					isCritical = collision.relativeVelocity.magnitude > 20f; // Adjust critical threshold
					normalhit = collision.relativeVelocity.magnitude > 2f; // Adjust critical threshold

					if (isCritical)
					{
						Debug.Log("here");
						int Emoteindex = Random.Range(0, angerEmotePrefabs.Length);
						InstantiateEmote(angerEmotePrefabs[Emoteindex], collision.transform); // Show the second anger emote
						randomIndex = Random.Range(0, AngerySounds.Length);
						HHG_LevelManager.instace.CoinSound.PlayOneShot(AngerySounds[randomIndex]);
						forall = true;

					}
					else if (normalhit)
					{
						Debug.Log("here");
						InstantiateEmote(angerEmotePrefabs[angerIndex], collision.transform);
						int randomIndex = Random.Range(0, AngerySounds.Length);
						angerIndex = (angerIndex + 1) % angerEmotePrefabs.Length;
						HHG_LevelManager.instace.CoinSound.PlayOneShot(AngerySounds[randomIndex]);
						forall = true;
					}
				}
			}
		}
	}

	private bool forall = false;
	

	public void ForNormalEmotes()
	{

		/*if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.CarDriving)
		{
			Debug.Log("here");
			// Show a random emote from the randomEmotePrefabs array
			int randomIndex = Random.Range(0, randomEmotePrefabs.Length);
			InstantiateEmote(randomEmotePrefabs[randomIndex], HHG_GameManager.Instance.CurrentCar.transform);
		}*/

	}

	private void InstantiateEmote(GameObject emotePrefab, Transform targetCar)
	{
		// Instantiate the emote above the target car
		Vector3 emotePosition = targetCar.position + Vector3.up * 1f; // Adjust the height as needed
		GameObject emoteInstance = Instantiate(emotePrefab, emotePosition, Quaternion.identity);
		emoteInstance.transform.SetParent(targetCar); // Attach to the car for proper positioning
		// Destroy the emote after 3 seconds
		Destroy(emoteInstance, 2f);
		forall = false;
	
	}
}
