using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform transformToFollow;

	void Start () {
		if (transformToFollow == null) {
			transformToFollow = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}
	
	void Update () {
		transform.position = new Vector3 (transformToFollow.position.x, transform.position.y, transformToFollow.position.z);
	}
}
