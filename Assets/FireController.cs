﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

	[SerializeField]
	private float rotationOffset = 0f;

	private float prevAngle;

	private void Awake() {
		prevAngle = 0;
	}

	private void Update () {
		float horizontalC = Input.GetAxis ("xFireController");
		float verticalC = Input.GetAxis ("yFireController");
		bool mouseMoved = (Input.GetAxis ("mouseX") != 0) || (Input.GetAxis ("mouseY") != 0);
		float angle = prevAngle;
		if (mouseMoved) {
			angle = AngleToMousePos ();
		} else if ((horizontalC != 0) || (verticalC != 0)) {
			angle = AngleToControllerInput (horizontalC, verticalC);
		}
		prevAngle = angle;
		Rotate (angle);
	}

	private float AngleToMousePos() {
		Vector3 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		inputPos.Set (inputPos.x, inputPos.y, 0);
		Debug.DrawLine (transform.position, inputPos, Color.magenta);
		Vector3 inputPosDiff = inputPos - transform.position;
		inputPosDiff.Normalize ();
		return Mathf.Atan2(inputPosDiff.y, inputPosDiff.x) * Mathf.Rad2Deg - 90;
	}

	private float AngleToControllerInput(float x, float y) {
		Vector3 inputPosDiff = new Vector3(x, y, transform.position.z);
		Debug.DrawLine (transform.position, transform.position + inputPosDiff, Color.green);
		inputPosDiff.Normalize ();
		return Mathf.Atan2(inputPosDiff.y, inputPosDiff.x) * Mathf.Rad2Deg - 90;
	}

	private void Rotate(float angle) {
		transform.rotation = Quaternion.Euler (0, 0, angle + rotationOffset);
	}

}
