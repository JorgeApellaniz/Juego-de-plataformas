using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class cerradura : NetworkBehaviour {

	public GameObject puertaFinal;
	public GameObject puertaFinal2;

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

					//Componentes animator
					animPuerta1 = puertaFinal.GetComponent<Animator> ();
					animPuerta2 = puertaFinal2.GetComponent<Animator> ();

					Debug.Log ("Cambiando animación");
					if (animPuerta1 != null) {
						Debug.Log ("animPuerta1 distinto null");
						if (animPuerta1.runtimeAnimatorController != null) {
							Debug.Log ("animPuerta1 runtime... distinto null");
							animPuerta1.SetInteger ("hay_llave", 1);
						}
					}

					if (animPuerta2 != null) {
						if (animPuerta2.runtimeAnimatorController != null) {
							animPuerta2.SetInteger ("hay_llave", 1);
						}
					}
						
					Destroy (child);
					NetworkServer.Destroy (child);
				}

				LogicaJuego.puerta_desbloqueada = true;


			}
		}
	}

}
