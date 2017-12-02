using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectible : MonoBehaviour {

	public float rotationSpeed = 150f;

	private void Update() {
		transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + (rotationSpeed * Time.deltaTime), transform.rotation.eulerAngles.z));
	}

}
