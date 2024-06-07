using UnityEngine;

public class KeepUpright : MonoBehaviour
{
	public Vector3 KeepRot = new Vector3(0f, 180f, 90f);

	private Quaternion WantedRot;

	private void Awake()
	{
		WantedRot = Quaternion.Euler(KeepRot);
	}

	private void LateUpdate()
	{
		base.transform.rotation = WantedRot;
	}
}
