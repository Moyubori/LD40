using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemyBehaviour : EnemyBehaviour
{
    private float _shootingInterval,_timer;
    private GameObject _player;
    private GameObject _projectile;
    [SerializeField]
    private ObjectPool _projectilePool;

    [SerializeField] private float _range;
    // Use this for initialization
    void Start ()
	{
	    _shootingInterval = 0.5f;
	    Damage = 5;
	    Health = 100;
        _player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _timer += Time.deltaTime;
	    if (Vector3.Distance(transform.position, _player.transform.position) > _range)
	    {
	        GetComponent<NavMeshAgent>().destination = _player.transform.position;
            transform.LookAt(_player.transform.position);
	        return;
	    }
	    if (_timer > _shootingInterval)
	    {
	        _timer = 0;
            Shoot();
	    }
	}

    void Shoot()
    {
        transform.LookAt(_player.transform.position);
        var instance = _projectilePool.GetInstance().GetComponent<EnemyProjectile>();
        instance.transform.position = new Vector3(transform.position.x, instance.transform.position.y, transform.position.z);
        instance.damage = Damage;
        instance.transform.rotation = transform.rotation;
        instance.gameObject.SetActive(true);
    }
}
