using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColisionCofre : NetworkBehaviour {

	public GameObject jugador;
	private Animator animCofre;

	void Start(){
		animCofre = GetComponent<Animator> ();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "jugador") {
			//hacer corrutina para hacer una espera de 1 segundo, el jugaodor no se pueda mover en ese tiempo
			if(col.transform.childCount == 1)
			{
				animCofre.SetBool ("abrir", true);
				Debug.Log ("Hay colision");
				GameObject child = col.transform.GetChild (0).gameObject;
				Destroy (child);
				NetworkServer.Destroy (child);
				LogicaJuego.desaparece_moneda = true;
			}
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "jugador") {
			animCofre.SetBool ("abrir", false);
		}
	}
}
