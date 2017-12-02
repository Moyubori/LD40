using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : PooledObject {

	[SerializeField]
	private float baseSpeed = 10f;

	public float speedModifier = 1f;

	void Start () {
		SetupPhysics ();
	}

	void OnEnable() {
		StartCoroutine (LaunchProjectile ());
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
		yield return new WaitForSeconds (3f);
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		ReturnToPool ();
	}

}
