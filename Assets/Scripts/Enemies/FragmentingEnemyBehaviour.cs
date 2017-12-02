using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FragmentingEnemyBehaviour : EnemyBehaviour {
    private GameObject _player;

    private float _fullHealth;
    // Use this for initialization
    void Start () {
        _fullHealth = Health = 100;
        Damage = 5;
        _player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update ()
	{
	    GetComponent<NavMeshAgent>().destination=(_player.transform.position);
	    if (transform.localScale.x > 0.25f &&Health / _fullHealth<=0.75f)
	    {
	        for (int i = 0; i < 2; i++)
	        {
	            var obj = Instantiate(this.gameObject,transform.position,transform.rotation);
	            obj.transform.localScale *= 0.5f;
	            obj.GetComponent<FragmentingEnemyBehaviour>().Health = Health;
	            obj.GetComponent<FragmentingEnemyBehaviour>().FullHealth = Health;
	            obj.GetComponent<BoxCollider>().size *= 2;
	        }
            Destroy(this.gameObject);
	    }
	}

    public float FullHealth
    {
        get { return _fullHealth; }
        set { _fullHealth = value; }
    }
}
