using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enemies,_doors;

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
	        if (_enemies.Count > 0)
	        {
	            foreach (var d in _doors)
	            {
	                StartCoroutine(CloseDors(d));
	            }
	        }
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
	        if (_enemies.Count == 0)
	        {
	            foreach (var d in _doors)
	            {
	                StartCoroutine(OpenDors(d));
	            }
	        }
	    }
	    foreach (var e in GameObject.FindGameObjectsWithTag("Enemy"))
	    {
	        if (!e.activeInHierarchy || !(e.transform.position.x < transform.position.x + 8) ||
	            !(e.transform.position.x > transform.position.x - 8) ||
	            !(e.transform.position.z < transform.position.z + 8) ||
	            !(e.transform.position.z > transform.position.z - 8)) continue;
	        if (!_enemies.Contains(e))
	        {
	            _enemies.Add(e);
	        }
	    }
	}

    IEnumerator CloseDors(GameObject d)
    {

        while (d.transform.GetChild(0).transform.localPosition.z >= -2f)
        {
            d.transform.GetChild(0).transform.localPosition +=new Vector3(0,0,-0.2f*Time.deltaTime);
            yield return null;
        }

    }
    IEnumerator OpenDors(GameObject d)
    {

        while (d.transform.GetChild(0).transform.localPosition.z <= 0f)
        {
            d.transform.GetChild(0).transform.localPosition += new Vector3(0, 0, +0.4f * Time.deltaTime);
            yield return null;
        }

    }
}
