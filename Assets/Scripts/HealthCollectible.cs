using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : Collectible {

	[SerializeField]
	private float health = 20f;

	public float meshRotationSpeed = 150f;

	private void Awake() {
		meshRotationSpeed = meshRotationSpeed * (Random.value / 10f + 0.95f);
		meshRotationSpeed *= (Mathf.Sign (Random.value - 0.5f));
	}

	public override void AnimateMesh() {
		mesh.rotation = Quaternion.Euler (new Vector3 (mesh.rotation.eulerAngles.x, mesh.rotation.eulerAngles.y + (meshRotationSpeed * Time.deltaTime), mesh.rotation.eulerAngles.z));
	}

	public override void Collect() {
		GameManager.Player.IncreaseHealth (health);
		gameObject.SetActive (false);
	}

}
