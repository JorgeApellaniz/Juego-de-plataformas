using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColisionMoneda : NetworkBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "jugador") {
			LogicaJuego.llevarMoneda (gameObject ,collider.gameObject);
		}
	}
}
