using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectible : MonoBehaviour {

	public float rotationSpeed = 150f;

	public int ammo = 100;

	public Transform mesh;

	private void Update() {
		mesh.rotation = Quaternion.Euler (new Vector3 (mesh.rotation.eulerAngles.x, mesh.rotation.eulerAngles.y + (rotationSpeed * Time.deltaTime), mesh.rotation.eulerAngles.z));
	}

	public int Collect() {
		gameObject.SetActive (false);
		return ammo;
	}

}
