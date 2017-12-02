using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PooledObject : MonoBehaviour {

	public ObjectPool parentPool;

	public void ReturnToPool() {
		transform.parent = parentPool.transform;
		gameObject.SetActive (false);
	}

	public abstract void Setup ();

}
