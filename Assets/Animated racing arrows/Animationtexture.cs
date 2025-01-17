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

    /*private void OnCollisionEnter(Collision collision)
    {
	    if (collision.gameObject.CompareTag("AICar"))
	    {
		    GetComponent<Collider>().enabled = false;
	    }
	    if (collision.gameObject.CompareTag("Player"))
	    {
		    GetComponent<Collider>().enabled = false;
	    }
    }*/
}
