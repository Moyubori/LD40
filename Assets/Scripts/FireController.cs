using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

	public ObjectPool projectilePool;

	[SerializeField]
	private float rotationOffset = 0f;

	[SerializeField]
	private float baseFireCooldown = 0.1f;
	public float fireCooldown;
	[SerializeField]
	private float fireCooldownCounter = 0f;

	[SerializeField]
	private float playerProjectileDamage = 10f;
	public float playerProjectileDamageMofidier = 1f;
	public float ProjectileDamage {
		get {
			return playerProjectileDamage * playerProjectileDamageMofidier;
		}
	}

	[SerializeField]
	private float playerProjectileSpeed = 10f;
	public float playerProjectileSpeedModifier = 1f;
	public float ProjectileSpeed {
		get {
			return playerProjectileSpeed * playerProjectileSpeedModifier;
		}
	}

	public float playerProjectileLifetime = 3f;

	private float prevAngle = 0f;

	private void Awake() {
		fireCooldown = baseFireCooldown;
	}

	private void Start() {
		if (projectilePool == null) {
			projectilePool = GameManager.GetObjectPool ("PlayerProjectilePool");
		}
	}

	private void Update () {
		HandleInput ();
		fireCooldownCounter = Mathf.Clamp (fireCooldownCounter - Time.deltaTime, 0, fireCooldown);
	}

	public void RevertFireCooldownToBaseValue() {
		fireCooldown = baseFireCooldown;
	}

	private void HandleInput() {
		if (GameManager.PlayerInputAllowed) {
			float horizontalC = Input.GetAxis ("xFireController");
			float verticalC = Input.GetAxis ("yFireController");
			bool mouseMoved = (Input.GetAxis ("mouseX") != 0) || (Input.GetAxis ("mouseY") != 0);
			bool mouseClicked = Input.GetMouseButton (0); // left mouse button
			float angle = prevAngle;
			if (mouseMoved || mouseClicked) {
				angle = AngleToMousePos ();
			} else if ((horizontalC != 0) || (verticalC != 0)) {
				angle = AngleToControllerInput (horizontalC, verticalC);
			}
			prevAngle = angle;
			Rotate (angle);

			float sinY = Mathf.Sin (transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
			float cosY = Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
			//Debug.Log ((horizontalC / sinY) + " " + (verticalC / cosY));
			if ((horizontalC / sinY) >= 0.9f || (verticalC / cosY) >= 0.9f || mouseClicked) {
				Fire ();
			}
		}
	}

	private float AngleToMousePos() {
		Vector3 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		inputPos.Set (inputPos.x, 0, inputPos.z);
		Debug.DrawLine (transform.position, inputPos, Color.magenta);
		Vector3 inputPosDiff = inputPos - transform.position;
		inputPosDiff.Normalize ();
		return -Mathf.Atan2(inputPosDiff.z, inputPosDiff.x) * Mathf.Rad2Deg + 90;
	}

	private float AngleToControllerInput(float x, float z) {
		Vector3 inputPosDiff = new Vector3(x, transform.position.y, z);
		Debug.DrawLine (transform.position, transform.position + inputPosDiff, Color.green);
		inputPosDiff.Normalize ();
		return -Mathf.Atan2(inputPosDiff.z, inputPosDiff.x) * Mathf.Rad2Deg + 90;
	}

	private void Rotate(float angle) {
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, angle + rotationOffset, transform.rotation.eulerAngles.z);
	}
				
	private void Fire() {
		Player player = GetComponent<Player> ();
		if (Mathf.Approximately(fireCooldownCounter, 0f) && player.Ammo > 0) {
			PlayerProjectile instance = projectilePool.GetInstance().GetComponent<PlayerProjectile>();
			instance.transform.position = new Vector3 (transform.position.x, instance.transform.position.y, transform.position.z);
			instance.transform.rotation = transform.rotation;
			instance.damage = playerProjectileDamage * playerProjectileDamageMofidier;
			instance.speed = playerProjectileSpeed * playerProjectileSpeedModifier;
			instance.gameObject.SetActive (true);
			fireCooldownCounter = fireCooldown;
			player.DecreaseAmmo();
		}
	}

}
