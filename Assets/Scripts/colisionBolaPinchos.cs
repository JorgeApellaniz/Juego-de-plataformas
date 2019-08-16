using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class colisionBolaPinchos : NetworkBehaviour {

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "suelo") {
			Destroy (gameObject);
			NetworkServer.Destroy (gameObject);
		}

		if (col.gameObject.tag == "jugador") {

			var combat = col.gameObject.GetComponent<Combate> ();
			combat.TakeDamage (20);
		}

	}
}
