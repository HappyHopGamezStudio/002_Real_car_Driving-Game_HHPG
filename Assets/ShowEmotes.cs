using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEmotes : MonoBehaviour
{
	public GameObject[] angerEmotePrefabs; 
	public GameObject[] randomEmotePrefabs;
	public AudioClip[] AngerySounds;

	private int angerIndex = 0;
	public bool isCritical,normalhit;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.CarDriving)
			{
				ForNormal = false;
				if (!forall)
				{
					if (!foranger)
					{
						// Determine if the hit is critical
						isCritical = collision.relativeVelocity.magnitude > 20f; // Adjust critical threshold
						normalhit = collision.relativeVelocity.magnitude > 2f; // Adjust critical threshold

						if (isCritical)
						{
							Debug.Log("here");
							InstantiateEmote(angerEmotePrefabs[1], collision.transform); // Show the second anger emote
							int randomIndex = Random.Range(0, AngerySounds.Length);
							GetComponent<AudioSource>().PlayOneShot(AngerySounds[randomIndex]);
							forall = true;
							foranger = true;
						}
						else if (normalhit)
						{
							Debug.Log("here");
							InstantiateEmote(angerEmotePrefabs[angerIndex], collision.transform);
							forall = true;
							foranger = true;
							int randomIndex = Random.Range(0, AngerySounds.Length);
							GetComponent<AudioSource>().PlayOneShot(AngerySounds[randomIndex]);
							angerIndex = (angerIndex + 1) % angerEmotePrefabs.Length; // Cycle through anger emotes
						}
					}
				}
			}
		}
	}

	private bool forall = false;
	private bool foranger = true;
	private bool ForNormal = true;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (HHG_GameManager.Instance.TpsStatus == PlayerStatus.CarDriving)
			{
				foranger = false;
				if (!forall)
				{
					if (ForNormal)
					{
						Debug.Log("here");
						// Show a random emote from the randomEmotePrefabs array
						int randomIndex = Random.Range(0, randomEmotePrefabs.Length);
						InstantiateEmote(randomEmotePrefabs[randomIndex], other.transform);
						forall = true;
					}
				}
			}
		}
	}

	private void InstantiateEmote(GameObject emotePrefab, Transform targetCar)
	{
		// Instantiate the emote above the target car
		Vector3 emotePosition = targetCar.position + Vector3.up * 2f; // Adjust the height as needed
		GameObject emoteInstance = Instantiate(emotePrefab, emotePosition, Quaternion.identity);
		emoteInstance.transform.SetParent(targetCar); // Attach to the car for proper positioning

		// Destroy the emote after 3 seconds
		Destroy(emoteInstance, 2f);
		foranger = false;
		forall = false;
		ForNormal = true;
	}
}
