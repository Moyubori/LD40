using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour {

	public bool animateMesh = true;

	public Transform mesh;

	private void Update() {
		if (animateMesh) {
			AnimateMesh ();
		}
	}

	public abstract void AnimateMesh ();

	public abstract void Collect ();

}
