﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

	void Update () {
		if (Input.GetKey ("r")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
		
}
