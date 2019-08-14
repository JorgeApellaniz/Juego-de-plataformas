using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class jugadorEntrando : NetworkBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "jugador" && LogicaJuego.puerta_desbloqueada) {
			Destroy (collider.gameObject);
			NetworkServer.Destroy (collider.gameObject);
			Debug.Log ("Jugador entrando");
		}
	}
}
