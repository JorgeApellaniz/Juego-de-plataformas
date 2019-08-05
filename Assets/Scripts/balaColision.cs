using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class balaColision : NetworkBehaviour {

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "limite") {
			Destroy (gameObject);
		}

		if (col.gameObject.tag == "plataforma") {
			Destroy (gameObject);
		}

		if (col.gameObject.tag == "enemigo") {
			Destroy (gameObject);
			Destroy (col.gameObject);
		}
		
	}
}
