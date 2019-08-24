using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

	private GameObject lobby;

	void Start(){
		lobby = GameObject.Find ("LobbyManager");
	}

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
		SceneManager.LoadScene ("Lobby2");
	}
		

	public void volverMenu()
	{
		SceneManager.LoadScene ("Menu");
	}

	public void aNiveles(){
		Network.Disconnect ();
		Destroy (lobby);
		SceneManager.LoadScene ("Niveles");
	}
}
