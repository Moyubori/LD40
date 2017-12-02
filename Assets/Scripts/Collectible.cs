using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : PooledObject {

	public bool animateMesh = true;

	public Transform mesh;

	private void Start() {
		if (parentPool == null) {
			ObjectPool pool = GameManager.GetObjectPool (objectTypeName + "Pool");
			pool.AddInstance (gameObject);
		}
	}

	private void Update() {
		if (animateMesh) {
			AnimateMesh ();
		}
	}
		
	public abstract void AnimateMesh ();

	public abstract void Collect ();

}
