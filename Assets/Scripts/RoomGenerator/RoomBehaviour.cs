using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enemies;

    [SerializeField] private GameObject _player,_roof;
	// Use this for initialization
	void Start () {
	    foreach (var e in _enemies)
	    {
	        e.SetActive(false);
	    }
		_player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	    if (_player.transform.position.x < transform.position.x + 8 &&
	        _player.transform.position.x > transform.position.x - 8 &&
	        _player.transform.position.z < transform.position.z + 8 &&
	        _player.transform.position.z > transform.position.z - 8)
	    {
            _roof.SetActive(false);
	        for (int i = _enemies.Count-1;i>=0;i--)
	        {
                if (_enemies[i]!=null)
	            _enemies[i].SetActive(true);
                else
                {
                    _enemies.Remove(_enemies[i]);
                }
	        }
	    }
	}
}
