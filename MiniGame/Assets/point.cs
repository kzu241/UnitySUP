using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point : MonoBehaviour {
	public float gizmoSize = 0.75f;
	private void OnDrawGizmos( ) {
		Gizmos.DrawWireSphere( transform.position, gizmoSize );
	}
}
