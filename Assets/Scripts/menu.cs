using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

	public void comenzar()
	{
		SceneManager.LoadScene ("Niveles");
	}

	public void salir()
	{
		Application.Quit ();
	}

	public void pasarANivel1()
	{
		SceneManager.LoadScene ("Lobby");
	}

	//TODO hacer escena 2
	public void pasarANivel2()
	{
		SceneManager.LoadScene ("Escena2");
	}
		

	public void instrucciones()
	{
		SceneManager.LoadScene ("instrucciones");
	}

	public void volverMenu()
	{
		SceneManager.LoadScene ("Menu");
	}
}
