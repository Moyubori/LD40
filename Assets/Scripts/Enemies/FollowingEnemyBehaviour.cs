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
	    Damage = 3;
	    Health = 50;
	    Exp = 5;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	    GetComponent<NavMeshAgent>().destination =_player.transform.position;
	    transform.GetChild(0).eulerAngles = new Vector3(45, 0, 0);
	    transform.GetChild(0).GetChild(0).localEulerAngles = transform.eulerAngles;
    }
}
