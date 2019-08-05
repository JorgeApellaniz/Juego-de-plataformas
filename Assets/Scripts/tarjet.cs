using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tarjet : MonoBehaviour {

	public Transform origen;
	public Transform destino;

	void OnDrawGizmosSelected()
	{
		if (origen != null && destino != null) {
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine (origen.position, destino.position);
		}
	}
}
