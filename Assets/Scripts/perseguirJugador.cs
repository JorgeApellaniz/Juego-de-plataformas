using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class perseguirJugador : NetworkBehaviour {

	//Variables para gestionar el radio de visión y velocidades
	public float radioVision;
	public float speed;

	//Variable para guardar al jugador
	GameObject[] jugador;

	//Variable para guardar la posición inicial
	Vector2 posicionInicial;

	void Start()
	{
		//Recuperamos al jugador gracias al tag
		jugador = GameObject.FindGameObjectsWithTag ("jugador");

		// Guardamos nuestra posición inicial
		posicionInicial = transform.position;
	}

	void Update(){

		// Por defecto nuestro objectivo siempre será nuestra posición inicial
		Vector2 target = posicionInicial;

		// Pero si la distancia hasta el jugador es menor que el radio de visión el objetivo será él.
		float dist1 = Vector2.Distance(jugador[0].transform.position, transform.position);
		if (dist1 < radioVision) {
			target = jugador[0].transform.position;
		}

		if (jugador.Length == 2) {
			// Pero si la distancia hasta el jugador es menor que el radio de visión el objetivo será él.
			float dist2 = Vector2.Distance (jugador [1].transform.position, transform.position);
			if (dist2 < radioVision) {
				target = jugador [1].transform.position;
			}
		}
		// Finalmente movemos al enemigo en dirección a su target
		float fixedSpeed = speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards (transform.position, target, fixedSpeed);
	}

	// Dibujamos el radio de visión sobre la escena dibujando un círculo
	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, radioVision);
	}

	// Si el enemigo pilla al jugador le quitará vida
	void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.tag == "jugador") {

			var combat = col.gameObject.GetComponent<Combate> ();
			combat.TakeDamage (20);
		}
	}

}
