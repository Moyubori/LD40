using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    [SerializeField]
	private float baseMovementSpeed = 10f;
	public float movementSpeedModifier = 1f;
    public float MaxSpeed;
	public float MovementSpeed {
		get {
			return baseMovementSpeed * movementSpeedModifier;
		}
	}

    private void Update () {
		HandleInput ();
    }

	private void HandleInput() {
		if (GameManager.PlayerInputAllowed) {
			float horizontal = Mathf.Clamp(Input.GetAxis("xMovementController") + Input.GetAxis("xMovementKeyboard"), -1, 1);
			float vertical = Mathf.Clamp(Input.GetAxis("yMovementController") + Input.GetAxis("yMovementKeyboard"), -1, 1);
			GetComponent<Rigidbody>().velocity = new Vector3(horizontal , 0, vertical).normalized * baseMovementSpeed * movementSpeedModifier;
            MaxSpeed = Vector3.Magnitude(new Vector3(1 * baseMovementSpeed * movementSpeedModifier, 0,1 * baseMovementSpeed * movementSpeedModifier));

        }
	}

}
