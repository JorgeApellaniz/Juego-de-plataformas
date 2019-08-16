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
	public GameObject llavePrefab;

	//Variable booleana para comprobar que no se pueda hacer un doble salto
	public static bool salto = false;
	public static NetworkInstanceId idNet;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		r2d = GetComponent<Rigidbody2D> ();
		tiempoDeDisparo = 5f;
	}
		
	void FixedUpdate () {

		if (!isLocalPlayer) {
			return;
		}
			

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
		if (Input.GetKeyDown (KeyCode.Space) 
			&& LogicaJuego.puede_disparar 
			&& LogicaJuego.tiempoAgotado == false 
			&& LogicaJuego.nivelCompleto == false) {		

			CmdFire ();
		}

		//Cuando se complete la puerta aparece la llave
		if (LogicaJuego.aparece_llave) {
			CmdApareceLlave ();
			LogicaJuego.aparece_llave = false;
		}

		if(LogicaJuego.puede_disparar)
		calculoTiempo (tiempoDeDisparo);

	}

	[Command]
	void CmdApareceLlave(){
		
		Vector2 posAleatoria_llave = new Vector2 (Random.Range (-14f, 8.5f), Random.Range (-3f, 5.25f));
		GameObject llave = Instantiate (llavePrefab, posAleatoria_llave, Quaternion.identity);
		NetworkServer.Spawn (llave);
	}

	[Command]
	void CmdFire()
	{
		int direccion;

		var bullet = (GameObject)Instantiate(
			balaPrefab,
			transform.position - transform.forward,
			Quaternion.identity);

		direccion = anim.GetInteger ("direccion");

		if (direccion == 0) {
			bullet.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (20, 0), ForceMode2D.Impulse);
		} else {
			bullet.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (-20, 0), ForceMode2D.Impulse);
		}
			
		NetworkServer.Spawn (bullet);

		// Se destruye la bala pasados 2 segundos
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
