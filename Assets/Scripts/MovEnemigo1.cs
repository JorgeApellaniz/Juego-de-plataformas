using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovEnemigo1 : NetworkBehaviour {

	private Rigidbody2D r2d;
	private Transform tr;

	public int segundosSalto;
	private bool timerReached;

	[SyncVar]
	private float timer = 0;

	// Use this for initialization
	void Start () {
		r2d = GetComponent<Rigidbody2D> ();
		tr = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		salto (segundosSalto);
	}

	public void salto(int sec)
	{
		if (!timerReached) {
			timer += Time.deltaTime;
		}
		if (!timerReached && timer > sec) {

			r2d.AddForce (Vector2.up * 330.0f * Time.deltaTime, ForceMode2D.Impulse);

			//Ponemos a falso el timerReached si no queremos que se ejecute de nuevo
			//timerReached = true;

			//Ponemos el contador de nuevo a 0 para realizar un bucle de saltos
			timer=0;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.tag == "jugador") {
			
			var combat = col.gameObject.GetComponent<Combate> ();
			combat.TakeDamage (20);
		}
	}
		
}
