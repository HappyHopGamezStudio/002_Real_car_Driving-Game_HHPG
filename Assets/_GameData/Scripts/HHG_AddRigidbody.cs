using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHG_AddRigidbody : MonoBehaviour
{
    private Vector3 pos;

    private Quaternion rot;

    private Renderer rend;

    private bool trig;

    private void Start()
    {
        pos = base.gameObject.transform.position;
        rot = base.gameObject.transform.rotation;
        rend = GetComponent<Renderer>();
    }

    // private void Update()
    // {
    //     if (trig && !rend.isVisible)
    //     {
    //         UnityEngine.Object.Destroy(base.gameObject.GetComponent<Rigidbody>());
    //         base.gameObject.transform.position = pos;
    //         base.gameObject.transform.rotation = rot;
    //         //  UnityEngine.Debug.Log("aaaaaa");
    //         trig = false;
    //     }
    // }

    private void OnTriggerEnter(Collider collision)
    {
        //   Debug.Log(collision.gameObject.tag);
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Vehicle" || collision.gameObject.tag == "AICar") && !base.gameObject.GetComponent<Rigidbody>())
        {
            base.gameObject.AddComponent(typeof(Rigidbody));
            base.gameObject.GetComponent<Rigidbody>().mass = 50f;
            UnityEngine.Object.Destroy(base.gameObject.GetComponent<Rigidbody>(), 5f);
            StartCoroutine(ResetProps());
            trig = true;
        }
    }

    public IEnumerator ResetProps()
    {
        yield return new WaitForSeconds(3);
        if (!rend.isVisible)
        {
            UnityEngine.Object.Destroy(base.gameObject.GetComponent<Rigidbody>());
            base.gameObject.transform.position = pos;
            base.gameObject.transform.rotation = rot;
            //  UnityEngine.Debug.Log("aaaaaa");
            trig = false;
        }
        else
        {
            StartCoroutine(ResetProps());
        }
    }
    
}