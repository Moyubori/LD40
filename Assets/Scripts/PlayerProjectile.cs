using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class PlayerProjectile : PooledObject
{
    public GameObject Particles,BloodSmall;
    [SerializeField]
    private float baseSpeed = 10f;
    [SerializeField]
    private float baseLifetime = 3f;
    [SerializeField]
    private float baseDamage = 10f;

    public Vector3 InheritedVelocity;

    public float speed;
    public float lifetime;
    public float damage;
  
    public float timer;
   
    
  
    

	private void Start () {
	
	}

	private bool physicsSet = false;



	private void OnEnable() {
	    foreach (var pooledObject in GameObject.FindGameObjectsWithTag("EnemyProjectile"))
	    {
	        Physics.IgnoreCollision(pooledObject.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
	    }
        timer = lifetime;
		GetComponent<Rigidbody> ().velocity =InheritedVelocity+ transform.forward * speed;
	}

	public override void Setup() {
		SetupPhysics ();
		speed = baseSpeed;
		lifetime = baseLifetime;
		//damage = baseDamage;
	}



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            if (collision.collider.tag == "Enemy")
            {
                Instantiate(BloodSmall, transform.position, new Quaternion());
            }
            else
            {
                Instantiate(Particles, transform.position, new Quaternion());
            }
            Disable();
        }
       
    }


  

	private void Update() {
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			Disable ();
		}
	}

	public void SetupPhysics() {

		if (!physicsSet) {
			Collider playerCollider = GameObject.FindGameObjectWithTag ("Player").GetComponent<Collider> ();	
			Physics.IgnoreCollision (playerCollider, GetComponent<Collider> ());
			List<GameObject> otherProjectiles = parentPool.GetAllInstances ();
			otherProjectiles.ForEach (projectile => {
				Collider collider = projectile.GetComponent<Collider> ();
				Physics.IgnoreCollision (collider, GetComponent<Collider> ());
			});
            foreach (var pooledObject in GameObject.FindGameObjectsWithTag("EnemyProjectile"))
		    {
		        Physics.IgnoreCollision(pooledObject.gameObject.GetComponent<Collider>(),GetComponent<Collider>());
		    }
			physicsSet = true;
		}
	}



    IEnumerator LaunchProjectile()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        yield return new WaitForSeconds(lifetime);
        Disable();
    }

    void Disable()
    {
        
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        speed = baseSpeed;
        lifetime = baseLifetime;
        //damage = baseDamage;
        ReturnToPool();
    }


}
