using System;
using UnityEngine;
using System.Collections;

public class Animationtexture : MonoBehaviour 
{

    public Vector2 Animationspeed = new Vector2( -0.5f, 0.0f );


    Vector2 uvOffset = Vector2.zero;

    void LateUpdate() 
    {
		uvOffset += ( Animationspeed * Time.deltaTime );
        if( GetComponent<Renderer>().enabled )
        {
			GetComponent<Renderer>().materials[ 0 ].SetTextureOffset( "_MainTex", uvOffset );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
	    if (other.gameObject.CompareTag("TrafficCar"))
	    {
		    GetComponent<Collider>().enabled = true;
	    }
	    if (other.gameObject.CompareTag("Player"))
	    {
		    GetComponent<Collider>().enabled = false;
	    }
    }
}
