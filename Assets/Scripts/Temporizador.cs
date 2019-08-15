using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Temporizador : NetworkBehaviour {

	public Text contador;
	public GameObject[] bonusDisparo;
	public GameObject[] bolasPinchos;

	[SyncVar]
	public float tiempo = 60f;

	// Use this for initialization
	void Start () {
		contador.text = " " + tiempo;
	}
	
	// Update is called once per frame
	void Update () {
		if (tiempo >= 0) {
			tiempo -= Time.deltaTime;
			contador.text = " " + tiempo.ToString ("f0");
			bonus (tiempo);
			soltarBolaPinchos (tiempo);

		} else {
			contador.text = "Tiempo agotado";
			LogicaJuego.tiempoAgotado = true;
		}
	}

	public void bonus(float tiempo)
	{
		if (tiempo <= 50 && tiempo >= 40) {
			bonusDisparo [0].SetActive (true);
		} else {
			bonusDisparo [0].SetActive (false);
		}

		if (tiempo <= 35 && tiempo >= 25) {
			bonusDisparo [1].SetActive (true);
		}else {
			bonusDisparo [1].SetActive (false);
		}

		if (tiempo <= 20 && tiempo >= 10) {
			bonusDisparo [2].SetActive (true);
		}else {
			bonusDisparo [2].SetActive (false);
		}
	}

	public void soltarBolaPinchos(float tiempo)
	{
		try {
			if (tiempo <= 50 && tiempo >= 40) {
				bolasPinchos [0].SetActive (true);
			} 
			
			if (tiempo <= 35 && tiempo >= 25) {
				bolasPinchos [1].SetActive (true);
			} 
			
			if (tiempo <= 20 && tiempo >= 10) {
				bolasPinchos [2].SetActive (true);
			}
		} catch (System.Exception ex) {
			throw new MissingReferenceException (ex.Message); 
		} 
	}
}
