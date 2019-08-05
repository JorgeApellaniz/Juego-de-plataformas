using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movPajaro : MonoBehaviour {

	public Transform tarjet;
	public float speed;


	private SpriteRenderer sprite;
	private Vector3 inicio, fin;
	// Use this for initialization
	void Awake () {
		sprite = GetComponent<SpriteRenderer> ();

		if (tarjet != null) {
			tarjet.parent = null;
			inicio = transform.position;
			fin = tarjet.position;
		}

		speed = 4f;


	}

	// Update is called once per frame
	void FixedUpdate () {

		if (tarjet != null) {
			float fixedSpeed = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, tarjet.position, fixedSpeed);
		}

		//float distancia = Vector3.Distance (jugador.transform.position, transform.position);

		if (transform.position == tarjet.position) {
			if (tarjet.position == inicio) {
				tarjet.position = fin;
				sprite.flipX = false;
			} else {
				tarjet.position = inicio;
				sprite.flipX = true;
			}
		}


	}
	// Podemos dibujar el radio de visión sobre la escena dibujando una esfera

	void OnDrawGizmosSelected()
	{

		Gizmos.color = Color.cyan;
		Gizmos.DrawLine (inicio, fin);

	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.tag == "jugador") {

			var combat = col.gameObject.GetComponent<Combate> ();
			combat.TakeDamage (10);
		}
	}
}
