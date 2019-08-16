using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class cerradura : NetworkBehaviour {

	public GameObject puertaFinal;
	public GameObject puertaFinal2;

	public GameObject puertaFinal_abierta;
	public GameObject puertaFinal2__abierta;

	private Animator animPuerta1;
	private Animator animPuerta2;


	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "jugador") {

			Debug.Log ("Colision con jugador");
			//hacer corrutina para hacer una espera de 1 segundo, el jugaodor no se pueda mover en ese tiempo
			if(col.transform.childCount == 1)
			{
				GameObject child = col.transform.GetChild (0).gameObject;
				if (child.tag.Equals ("llave")) {

					puertaFinal.SetActive (false);
					puertaFinal_abierta.SetActive (true);

					puertaFinal2.SetActive (false);
					puertaFinal2__abierta.SetActive (true);
						
					Destroy (child);
					NetworkServer.Destroy (child);
				}

				LogicaJuego.puerta_desbloqueada = true;


			}
		}
	}

}
