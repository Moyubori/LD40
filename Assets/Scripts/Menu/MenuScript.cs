using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject Menu;
	// Use this for initialization
	void Start () {
		Menu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        Menu.SetActive(!Menu.activeInHierarchy);
            if(Menu.activeInHierarchy)
	        Time.timeScale = 0;
            else
            {
                Time.timeScale = 1;
            }
	    }
	}

    public void Resume()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
