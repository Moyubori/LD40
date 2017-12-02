using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager instance;

	private Player player;

	public static Player Player {
		get {
			return instance.player;
		}
	}

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

	private void Start() {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	public static ObjectPool GetObjectPool(string poolName) {
		return instance.transform.Find (poolName).GetComponent<ObjectPool> ();
	}

	public static void GameOver () {
		Debug.Log ("Game Over!");
		instance.allowPlayerInput = false;
	}
		
}
