using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingEnemyBehaviour : EnemyBehaviour
{
    [SerializeField] 
    private GameObject _player;
	// Use this for initialization
	void Start ()
	{
	    _player = GameObject.Find("Player");
	    Health = 100;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	    GetComponent<NavMeshAgent>().destination =_player.transform.position;
	}
}
