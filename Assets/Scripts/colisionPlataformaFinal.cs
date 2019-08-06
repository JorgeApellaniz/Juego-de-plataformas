using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class colisionPlataformaFinal : NetworkBehaviour {

	public GameObject instanciaPl1;


	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "jugador") {
			if(collider.transform.childCount == 1)
			{
				GameObject child = collider.transform.GetChild (0).gameObject;
				if (child.name.Equals ("pl1")) {
					GameObject plat1 = GameObject.Find ("plataforma_izq_sombra");
					Debug.Log ("Colision con la primera pieza");
					Destroy (child);
					NetworkServer.Destroy (child);
					GameObject pla1 = Instantiate (instanciaPl1, plat1.transform.position, Quaternion.identity);
					NetworkServer.Spawn (pla1);
					//child.transform.SetParent (plat1.transform);
				}
			}
		}
	}
}
