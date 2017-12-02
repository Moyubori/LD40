using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	public GameObject prefab;
	public int instancesPooledOnStart = 0;

	private List<GameObject> pooledObjects = new List<GameObject>();

	private void Start() {
		for (int i = 0; i < instancesPooledOnStart; i++) {
			AddInstance ();
		}
	}

	private GameObject AddInstance() {
		GameObject newInstance = Instantiate (prefab);
		newInstance.GetComponent<PooledObject> ().parentPool = this;
		newInstance.transform.parent = transform;
		newInstance.SetActive (false);
		newInstance.GetComponent<PooledObject> ().Setup ();
		pooledObjects.Add (newInstance);
		return newInstance;
	}

	public GameObject GetInstance() {
		foreach (GameObject instance in pooledObjects) {
			if (!instance.activeSelf) {
				return instance;
			}
		}
		return AddInstance ();
	}

	public List<GameObject> GetAllInstances() {
		return pooledObjects;
	}

}
