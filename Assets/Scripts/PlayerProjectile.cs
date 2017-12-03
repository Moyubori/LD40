using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : PooledObject {

	[SerializeField]
	private float baseSpeed = 10f;
	[SerializeField]
	private float baseLifetime = 3f;
	[SerializeField]
	private float baseDamage = 10f;

	public float speed;
	public float lifetime;
	public float damage;

	private bool physicsSet = false;

	private float timer;

	private void OnEnable() {
		timer = lifetime;
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
	}

	public override void Setup() {
		SetupPhysics ();
		speed = baseSpeed;
		lifetime = baseLifetime;
		damage = baseDamage;
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag != "Player") {
			Disable ();
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
			physicsSet = true;
		}
	}

	public void Disable() {
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		speed = baseSpeed;
		lifetime = baseLifetime;
		damage = baseDamage;
		ReturnToPool ();
	}

}
