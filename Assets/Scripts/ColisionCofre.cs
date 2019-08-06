using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColisionCofre : NetworkBehaviour {

	public GameObject p1_oculta;
	public GameObject p2_oculta;
	public GameObject p3_oculta;
	public GameObject p4_oculta;
	public GameObject p5_oculta;

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
				GameObject child = col.transform.GetChild (0).gameObject;
				if (child.name == "pl1") {
					animCofre.SetBool ("abrir", true);
					Debug.Log ("Hay colision");
					Destroy (child);
					NetworkServer.Destroy (child);
					LogicaJuego.desaparece_moneda = true;

					//Hacer aparecer la primera pieza de la plataforma final
					p1_oculta.SetActive (true);
				} else if (child.name == "pl2") {
					animCofre.SetBool ("abrir", true);
					Debug.Log ("Hay colision");
					Destroy (child);
					NetworkServer.Destroy (child);
					LogicaJuego.desaparece_moneda = true;

					//Hacer aparecer la primera pieza de la plataforma final
					p2_oculta.SetActive (true);
				} else if (child.name == "pl3") {
					animCofre.SetBool ("abrir", true);
					Debug.Log ("Hay colision");
					Destroy (child);
					NetworkServer.Destroy (child);
					LogicaJuego.desaparece_moneda = true;

					//Hacer aparecer la primera pieza de la plataforma final
					p3_oculta.SetActive (true);
				} else if (child.name == "puerta1") {
					animCofre.SetBool ("abrir", true);
					Debug.Log ("Hay colision");
					Destroy (child);
					NetworkServer.Destroy (child);
					LogicaJuego.desaparece_moneda = true;

					//Hacer aparecer la primera pieza de la plataforma final
					p4_oculta.SetActive (true);
				} else if (child.name == "puerta2") {
					animCofre.SetBool ("abrir", true);
					Debug.Log ("Hay colision");
					Destroy (child);
					NetworkServer.Destroy (child);
					LogicaJuego.desaparece_moneda = true;

					//Hacer aparecer la primera pieza de la plataforma final
					p5_oculta.SetActive (true);
				}

			}
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "jugador") {
			animCofre.SetBool ("abrir", false);
		}
	}
}
