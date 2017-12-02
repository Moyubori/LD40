using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    [SerializeField]
	private float baseMovementSpeed = 10f;
	public float movementSpeedModifier = 1f;

    private void Update () {
		HandleInput ();
    }

	private void HandleInput() {
		if (GameManager.PlayerInputAllowed) {
			float horizontal = Mathf.Clamp(Input.GetAxis("xMovementController") + Input.GetAxis("xMovementKeyboard"), -1, 1);
			float vertical = Mathf.Clamp(Input.GetAxis("yMovementController") + Input.GetAxis("yMovementKeyboard"), -1, 1);
			GetComponent<Rigidbody>().velocity = new Vector3(horizontal * baseMovementSpeed * movementSpeedModifier, 0, vertical * baseMovementSpeed * movementSpeedModifier);
		}
	}

}
