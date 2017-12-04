using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectible : Collectible {

	[SerializeField]
	private int ammo = 100;

	public float meshRotationSpeed = 150f;

	private void Awake() {
		meshRotationSpeed = meshRotationSpeed * (Random.value / 10f + 0.95f);
		meshRotationSpeed *= (Mathf.Sign (Random.value - 0.5f));
	}

	public override void AnimateMesh() {
		mesh.localRotation = Quaternion.Euler (new Vector3 (mesh.localRotation.eulerAngles.x, mesh.localRotation.eulerAngles.y + (meshRotationSpeed * Time.deltaTime), mesh.localRotation.eulerAngles.z));
	}

	public override void Collect() {
		GameManager.Player.IncreaseAmmo (ammo);
		ReturnToPool ();
	}

}
