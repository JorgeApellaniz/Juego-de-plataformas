using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Combate : NetworkBehaviour {


	public GameObject spawnDead;
	public const int maxHealth = 100;

	[SyncVar]
	public int health = maxHealth;

	public void TakeDamage(int amount)
	{
		if (!isServer) {
			return;
		}

		health -= amount;
		Debug.Log("Salud: " + health);
		if (health <= 0)
		{
			health = 100;
			transform.position = spawnDead.transform.position;
			Debug.Log("Dead!");
		}
	}

}
