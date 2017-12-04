using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager instance;

	private Player player;

	[SerializeField]
	private GameObject poolPrefab;

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
	    Time.timeScale = 1;
	    //DontDestroyOnLoad (gameObject);
	}

	private void Start() {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	public static ObjectPool GetObjectPool(string poolName) {
		Transform pool = instance.transform.Find (poolName);
		if (pool == null) {
			pool = instance.CreateNewPool (poolName).transform;
		}
		return pool.GetComponent<ObjectPool> ();
	}

	private GameObject CreateNewPool(string name) {
		GameObject newPool = Instantiate (poolPrefab, transform);
		newPool.name = name;
		return newPool;
	}

	public static void GameOver () {
		Debug.Log ("Game Over!");
		instance.allowPlayerInput = false;
	}
		
}
