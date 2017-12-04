﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : PooledObject
{

    [SerializeField]
    private float baseSpeed = 10f;
    [SerializeField]
    private float baseLifetime = 3f;
    [SerializeField]
    private float baseDamage = 10f;

    public float speed;
    public float lifetime;
    public float damage;

    private void Start()
        {
        

    }


    private void OnEnable()
    {
        foreach (var pooledObject in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Physics.IgnoreCollision(pooledObject.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
        StartCoroutine(LaunchProjectile());
    }
    

    private void OnCollisionEnter(Collision collision)
    {
       
            Disable();
        
    }

    
    private void SetupPhysics()
    {
        var enemyCollider = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var e in enemyCollider)
        {
            if(e.GetComponent<Collider>()!=null)
            Physics.IgnoreCollision(e.GetComponent<Collider>(), GetComponent<Collider>());
            }
        List<GameObject> otherProjectiles = parentPool.GetAllInstances();
        otherProjectiles.ForEach(projectile => {
            Collider collider = projectile.GetComponent<Collider>();
            Physics.IgnoreCollision(collider, GetComponent<Collider>());
        });
        foreach (var pooledObject in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Physics.IgnoreCollision(pooledObject.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    private IEnumerator LaunchProjectile()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        yield return new WaitForSeconds(lifetime);
        Disable();
    }

    public void Disable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        speed = baseSpeed;
        lifetime = baseLifetime;
        damage = baseDamage;
        ReturnToPool();
    }

    public override void Setup()
    {
        SetupPhysics();
    }
}
