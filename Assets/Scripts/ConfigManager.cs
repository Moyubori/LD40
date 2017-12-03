using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour {

	[SerializeField]
	private bool allowPlayerInput = true;
	public bool PlayerInputAllowed {
		get {
			return allowPlayerInput;
		}
		set {
			allowPlayerInput = value;
		}
	}
		
	[SerializeField]
	private float joystickTiltFireThreshold = 0.9f; // specifies the percentage of stick tilt needed before the character starts shooting
	public float JoystickTiltFireThreshold {
		get {
			return joystickTiltFireThreshold;
		}
	}

}
