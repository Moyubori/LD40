using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigManager))]
[RequireComponent(typeof(DebuffManager))]
public class GameManager : MonoBehaviour {

	private static GameManager instance;

	private Player player;
	private ConfigManager config;
	private DebuffManager debuff;

	[SerializeField]
	private GameObject poolPrefab;

	public static Player Player {
		get {
			return instance.player;
		}
	}
	public static ConfigManager Config {
		get {
			return instance.config;
		}
	}
	public static DebuffManager Debuff {
		get { 
			return instance.debuff;
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
		config = GetComponent<ConfigManager> ();
		debuff = GetComponent<DebuffManager> ();
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
		instance.config.PlayerInputAllowed = false;
	}
		
}
