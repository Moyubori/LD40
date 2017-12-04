using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager instance;

	private Player player;

	[SerializeField]
	private GameObject poolPrefab;

	public GameObject gameOverScreen;
	public GameObject inGameUI;

	private bool gameOver = false;
	public static bool IsGameOver {
		get { 
			return instance.gameOver;
		}
	}

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
	}

	private void Start() {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	private void Update () {
		if (Input.GetKey ("r")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
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
		instance.gameOver = true;
		instance.allowPlayerInput = false;
		instance.inGameUI.SetActive (false);
		instance.gameOverScreen.SetActive (true);
		instance.player.GetComponent<Rigidbody> ().isKinematic = true;
	}
		
}
