using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : PooledObject {

	[SerializeField]
	private float baseSpeed = 10f;

	public float speedModifier = 1f;

	[SerializeField]
	private float baseLifetime = 3f;

	public float lifetimeModifier = 1f;

	private void Start () {
		SetupPhysics ();
	}

	private void OnEnable() {
		StartCoroutine (LaunchProjectile ());
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag != "Player") {
			Disable ();
		}
	}

	private void SetupPhysics() {
		Collider playerCollider = GameObject.FindGameObjectWithTag ("Player").GetComponent<Collider> ();	
		Physics.IgnoreCollision (playerCollider, GetComponent<Collider> ());
		List<GameObject> otherProjectiles = parentPool.GetAllInstances ();
		otherProjectiles.ForEach (projectile => {
			Collider collider = projectile.GetComponent<Collider> ();
			Physics.IgnoreCollision(collider, GetComponent<Collider>());
		});
	}

	private IEnumerator LaunchProjectile() {
		GetComponent<Rigidbody> ().velocity = transform.forward * baseSpeed * speedModifier;
		yield return new WaitForSeconds (baseLifetime * lifetimeModifier);
		Disable ();
	}

	public void Disable() {
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		ReturnToPool ();
	}

}
