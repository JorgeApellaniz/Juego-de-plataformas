using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class colisionPincho : NetworkBehaviour {

	void OnCollisionExit2D(Collision2D col) {

		if (col.gameObject.tag == "jugador") {

			var combat = col.gameObject.GetComponent<Combate> ();
			combat.TakeDamage (25);
		}
	}
}
