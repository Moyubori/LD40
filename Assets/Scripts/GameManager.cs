using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager instance;

	private bool allowPlayerInput = true;

	public static bool PlayerInputAllowed {
		get {
			return instance.allowPlayerInput;
		}
	}

	private void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	public static ObjectPool GetObjectPool(string poolName) {
		return instance.transform.Find ("poolName").GetComponent<ObjectPool> ();
	}

	public static void GameOver () {
		Debug.Log ("Game Over!");
		instance.allowPlayerInput = false;
	}
		
}
