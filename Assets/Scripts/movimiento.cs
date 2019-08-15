using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class movimiento : NetworkBehaviour {

	public Vector2 mov;
	private Animator anim;
	private Rigidbody2D r2d;

	private float tiempoDeDisparo;

	//Prefab de la bala
	public GameObject balaPrefab;

	//Variable booleana para comprobar que no se pueda hacer un doble salto
	public static bool salto = false;
	public static NetworkInstanceId idNet;

	private Vector3 escalaActual;

	private Vector3 izqScale;

	private Vector3 derScale;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		r2d = GetComponent<Rigidbody2D> ();
		izqScale = new Vector3 (1, 1, 1);
		derScale = new Vector3 (-1, 1, 1);
		escalaActual = gameObject.transform.localScale;
		tiempoDeDisparo = 5f;
	}
		
	void FixedUpdate () {

		if (!isLocalPlayer) {
			return;
		}
			

		escalaActual = gameObject.transform.localScale;

		if (Input.GetKey (KeyCode.RightArrow) 
			//&& LogicaJuego.tiempoAgotado == false 
			&& LogicaJuego.nivelCompleto == false) {

			mov = new Vector2 (400.0f, 0);
			r2d.AddForce (mov * Time.deltaTime);

			// Animación
			anim.SetFloat ("velocidad", 1f);
			anim.SetInteger ("saltando", 0);
			anim.SetInteger ("direccion", 0);

		} else if (Input.GetKey (KeyCode.LeftArrow) 
//			&& LogicaJuego.tiempoAgotado == false 
			&& LogicaJuego.nivelCompleto == false) {


			mov = new Vector2 (-400.0f, 0);
			r2d.AddForce (mov * Time.deltaTime);

			// Animación
			anim.SetFloat ("velocidad", 1f);
			anim.SetInteger ("saltando", 0);
			anim.SetInteger ("direccion", 1);
		} 
		else 
		{
			anim.SetFloat ("velocidad", 0f);
		}
		if (Input.GetKeyDown (KeyCode.UpArrow) && salto == false 
			//&& LogicaJuego.tiempoAgotado == false 
			&& LogicaJuego.nivelCompleto == false) {		// Para que se compruebe por separado
			r2d.AddForce (Vector2.up * 350.0f * Time.deltaTime, ForceMode2D.Impulse);
			// Actualizar estado de la animación si fuese necesario
			anim.SetInteger ("saltando", 1);
			salto = true;

		}

		// Para que se compruebe por separado
		if (Input.GetKeyDown (KeyCode.Space) && LogicaJuego.puede_disparar && LogicaJuego.tiempoAgotado == false && LogicaJuego.nivelCompleto == false) {		

			CmdFire (escalaActual);
		}

		if(LogicaJuego.puede_disparar)
		calculoTiempo (tiempoDeDisparo);

	}

	[Command]
	void CmdFire(Vector3 escala)
	{
		// This [Command] code is run on the server!

		// create the bullet object from the bullet prefab
		var bullet = (GameObject)Instantiate(
			balaPrefab,
			transform.position - transform.forward,
			Quaternion.identity);

		// make the bullet move away in front of the player
//		bullet.GetComponent<Rigidbody2D>().velocity = -transform.forward*4;
		if (escala.x > 0) {
			bullet.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (20, 0), ForceMode2D.Impulse);
		} else {
			bullet.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (-20, 0), ForceMode2D.Impulse);
		}

		// spawn the bullet on the clients
		NetworkServer.Spawn (bullet);

		// make bullet disappear after 2 seconds
		Destroy(bullet, 2.0f);        
	}

	void calculoTiempo(float tiempo)
	{

		StartCoroutine (UsingYield (5));

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		salto = false;
		anim.SetInteger ("saltando", 0);

	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "bonusDisparo") {
			LogicaJuego.puede_disparar = true;
			Debug.Log ("Puedes disparar");
		}
	}

	//Métodos de red
	public override void OnStartLocalPlayer()
	{
		idNet = this.netId;
		//Debug.Log (idNet);
		GetComponent<SpriteRenderer>().material.color = Color.green;

	}

	public IEnumerator UsingYield(int segundos){
		while (segundos > 0) {
			//Debug.Log ("Quedan " + segundos);
			yield return new WaitForSeconds (1.0f);
			segundos--;
		}
		if(segundos <=0) LogicaJuego.puede_disparar = false;
	}
}
