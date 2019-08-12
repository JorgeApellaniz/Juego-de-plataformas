using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class cerradura : NetworkBehaviour {



	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "jugador") {
			//hacer corrutina para hacer una espera de 1 segundo, el jugaodor no se pueda mover en ese tiempo
			if(col.transform.childCount == 1)
			{
				//Cuando se lleva la llave a la cerradura
				GameObject child = col.transform.GetChild (0).gameObject;


			}
		}
	}

}
