using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

	[SerializeField]
	private float rotationOffset = 0f;

	private void Update () {
		RotateToMousePos ();
	}

	private void RotateToMousePos() {
		Vector3 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		inputPos.Set (inputPos.x, inputPos.y, 0);
		Debug.DrawLine (transform.position, inputPos);
		Vector3 inputPosDiff = inputPos - transform.position;
		inputPosDiff.Normalize ();
		float angle = Mathf.Atan2(inputPosDiff.y, inputPosDiff.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.Euler (0, 0, angle + rotationOffset);
	}

}
