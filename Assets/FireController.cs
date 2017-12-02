using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

	[SerializeField]
	private float rotationOffset = 0f;

	public float fireCooldown = 1f;
	[SerializeField]
	private float fireCooldownCounter = 0f;

	private float prevAngle = 0f;

	private void Update () {
		HandleInput ();
		fireCooldownCounter = Mathf.Clamp (fireCooldownCounter - Time.deltaTime, 0, fireCooldown);
	}

	private void HandleInput() {
		float horizontalC = Input.GetAxis ("xFireController");
		float verticalC = Input.GetAxis ("yFireController");
		bool mouseMoved = (Input.GetAxis ("mouseX") != 0) || (Input.GetAxis ("mouseY") != 0);
		bool mouseClicked = Input.GetMouseButton (0); // left mouse button
		float angle = prevAngle;
		if (mouseMoved || mouseClicked) {
			angle = AngleToMousePos ();
			if (mouseClicked) {
				Fire ();
			}
		} else if ((horizontalC != 0) || (verticalC != 0)) {
			angle = AngleToControllerInput (horizontalC, verticalC);
			Fire ();
		}
		prevAngle = angle;
		Rotate (angle);
	}

	private float AngleToMousePos() {
		Vector3 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		inputPos.Set (inputPos.x, 0, inputPos.z);
		Debug.DrawLine (transform.position, inputPos, Color.magenta);
		Vector3 inputPosDiff = inputPos - transform.position;
		inputPosDiff.Normalize ();
		return -Mathf.Atan2(inputPosDiff.z, inputPosDiff.x) * Mathf.Rad2Deg + 90;
	}

	private float AngleToControllerInput(float x, float z) {
		Vector3 inputPosDiff = new Vector3(x, transform.position.y, z);
		Debug.DrawLine (transform.position, transform.position + inputPosDiff, Color.green);
		inputPosDiff.Normalize ();
		return -Mathf.Atan2(inputPosDiff.z, inputPosDiff.x) * Mathf.Rad2Deg + 90;
	}

	private void Rotate(float angle) {
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, angle + rotationOffset, transform.rotation.eulerAngles.z);
	}

	private void Fire() {
		if (fireCooldownCounter == 0) {
			Debug.Log ("Fire");
			Debug.DrawRay (transform.position, transform.forward * 10, Color.red);
			fireCooldownCounter = fireCooldown;
		}
	}

}
