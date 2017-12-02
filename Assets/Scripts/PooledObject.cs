using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PooledObject : MonoBehaviour {

	public ObjectPool parentPool;
	[SerializeField]
	protected string objectTypeName;

	public void ReturnToPool() {
		transform.parent = parentPool.transform;
		gameObject.SetActive (false);
	}

	public virtual void Setup () {}

}
