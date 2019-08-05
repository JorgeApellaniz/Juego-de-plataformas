using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LogicaJuego : NetworkBehaviour {

	public GameObject[] monedas;
	public GameObject moneda;

	public static LogicaJuego instancia;

	public static bool puede_disparar = false;
	public static bool desaparece_moneda = false;
	public static bool coger_moneda = false;
	public static int puntuacion;
	public static bool nivelCompleto;
	public static bool tiempoAgotado;
	public static bool puedo_inicializar = false;

	//Mensajes del canvas
	public Text puntos;
	public Text msgPuedeDisparar;
	public Text nivelCompletoText;
	public Text nivelFallidoText;


	private int i;
	private int j;

	// Use this for initialization
	void Start () {
		puntuacion = 0;
		i = 0;
		j = i + 1;
		msgPuedeDisparar.text = " ";
		nivelCompleto = false;
		tiempoAgotado = false;
		nivelCompletoText.gameObject.SetActive (false);
		nivelFallidoText.gameObject.SetActive (false);

	}

	void Update()
	{
		if (puedo_inicializar) {
			Debug.Log ("instanciando monedas");
			instanciarMonedas (moneda);
			puedo_inicializar = false;
		}
				
		//Mostramos la puntuación
		puntos.text = "PUNCIACIÓN: " + puntuacion;
		if (puede_disparar) {
//			tiempoDeDisparo = tiempoDeDisparo - Time.deltaTime;
			msgPuedeDisparar.text = "Puedes disparar";
		} else {
			msgPuedeDisparar.text = " ";
		}
		try{
			if (desaparece_moneda) {
				puntuacion += 1;
				if (monedas [i] == null) {
					monedas [j + 1].SetActive (true);
					i = j;
					j = j + 1;
				} else {
					monedas [j + 1].SetActive (true);
					j = j + 1;
				}
					
				desaparece_moneda = false;
			}
		}
		catch (System.IndexOutOfRangeException e) 
		{
			desaparece_moneda = false;

		}
			
		//comprobarNivelCompletado ();
		//tiempoTerminado ();
	}
		
	public void tiempoTerminado()
	{
		if (LogicaJuego.tiempoAgotado) {
			mostrarNivelFallido ();
		}
	}

	public void instanciarMonedas(GameObject moneda){
		//Nuevo punto aleatorio
		for(int i=0;i<5;i++){
			Vector2 randonPoint1 = new Vector2(Random.Range(-11.25f,14.86f),Random.Range(-2.66f,7.53f));
			monedas[i] = (GameObject) Instantiate(moneda, randonPoint1, Quaternion.identity);
			NetworkServer.Spawn(monedas[i]);
		}
		monedas [2].SetActive (false);
		monedas [3].SetActive (false);
		monedas [4].SetActive (false);
	}

	public void comprobarNivelCompletado()
	{
		GameObject[] mon;
		mon = GameObject.FindGameObjectsWithTag ("moneda");
		//Debug.Log ("Quedan " + mon.Length + " monedas");
		if (mon.Length == 0) {
			mostrarNivelCompleto ();
		}
	}

	public void mostrarNivelCompleto()
	{
		nivelCompleto = true;
		nivelCompletoText.gameObject.SetActive (true);
	}

	public void mostrarNivelFallido()
	{
		nivelFallidoText.gameObject.SetActive (true);
	}
		
	public static void llevarMoneda(GameObject moneda, GameObject jugador)
	{
		if (jugador.transform.childCount == 0) {
			moneda.transform.SetParent (jugador.GetComponent<Transform> ());
		}
	}

	//Métodos networking
	public void ExitGame()
	{
		if (NetworkServer.active)
		{
			NetworkManager.singleton.StopServer();
		}
		if (NetworkClient.active)
		{
			NetworkManager.singleton.StopClient();
		}
	}
		
}
