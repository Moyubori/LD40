using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	private MovementController movementController;
	[SerializeField]
	private FireController fireController;

	[SerializeField]
	private int ammo = 0;
	public int Ammo {
		get { 
			return ammo;
		}
	}
	[SerializeField]
	private bool overrideDamageModifier = false;

	[SerializeField]
	private float baseHealth = 100f;
	[SerializeField]
	private float health = 100f;
	public float Health {
		get { 
			return health;
		}
		set {
			health = Mathf.Clamp (value, 0f, baseHealth);
		}
	}
	[SerializeField]
	private bool overriderFireCooldown = false;
		
	private void Start () {
		if (movementController == null) {
			movementController = GetComponent<MovementController> ();
		}
		if (fireController == null) {
			fireController = GetComponent<FireController> ();
		}
	}

	private void Update() {
		if (GameManager.Debuff.AmmoDamageDebuffActive) {
			UpdateDamageModifier ();
		}
		if (GameManager.Debuff.HealthFireCooldownDebuffActive) {
			UpdateFireCooldownModifier ();
		}
	}
		
	private void OnTriggerEnter(Collider collider) {
		if (collider.tag == "Collectible") {
			collider.GetComponent<Collectible> ().Collect();
		}
	}

	public void SetMovementSpeedModifier(float modifier) { // permanently modifies movement speed
		movementController.movementSpeedModifier = modifier;
	}

	public void SetMovementSpeedModifier(float modifier, float time) { // modifies movement speed for a given time
		StartCoroutine(TimedMovementSpeedModifier(modifier,time));
	}

	private IEnumerator TimedMovementSpeedModifier(float modifier, float time) {
		float prevModifier = movementController.movementSpeedModifier;
		movementController.movementSpeedModifier = modifier;
		yield return new WaitForSeconds (time);
		movementController.movementSpeedModifier = prevModifier;
	}

	public float GetCurrentMovementSpeed() {
		return movementController.MovementSpeed;
	}

	public void SetFireCooldown(float cooldown) { // permanently modifies fire cooldown
		fireController.fireCooldown = cooldown;
	}

	public void SetFireCooldown(float cooldown, float time) { // modifies fire cooldown for a given time
		StartCoroutine(TimedFireCooldownModifier(cooldown, time));
	}

	private IEnumerator TimedFireCooldownModifier(float cooldown, float time) { // Warning: do not launch this multiple times at once or things WILL break
		overriderFireCooldown = true;
		float prevCooldown = fireController.fireCooldown;
		fireController.fireCooldown = cooldown;
		yield return new WaitForSeconds (time);
		fireController.fireCooldown = prevCooldown;
		overriderFireCooldown = false;
	}

	public void RevertFireCooldownToBaseValue() {
		fireController.RevertFireCooldownToBaseValue ();
	}

	public float GetCurrentFireCooldown() {
		return fireController.fireCooldown;
	}

	public void SetProjectileDamageModifier(float modifier) { // permanently modifies projectile damage
		fireController.playerProjectileDamageMofidier = modifier;
	}

	public void SetProjectileDamageModifier(float modifier, float time) { // modifies prtojectile damage for a given time
		StartCoroutine(TimedProjectileDamageModifier(modifier,time));
	}

	private IEnumerator TimedProjectileDamageModifier(float modifier, float time) {
		overrideDamageModifier = true;
		float prevModifier = fireController.playerProjectileDamageMofidier;
		fireController.playerProjectileDamageMofidier = modifier;
		yield return new WaitForSeconds (time);
		fireController.playerProjectileDamageMofidier = prevModifier;
		overrideDamageModifier = false;
	}

	public float GetCurrentProjectileDamage() {
		return fireController.ProjectileDamage;
	}

	public void SetProjectileLifetime(float lifetime) { // permanently modifies projectile lifetime
		fireController.playerProjectileLifetime = lifetime;
	}

	public void SetProjectileLifetime(float lifetime, float time) { // modifies projectile lifetime for a given time
		StartCoroutine(TimedProjectileLifetimeModifier(lifetime, time));
	}

	private IEnumerator TimedProjectileLifetimeModifier(float lifetime, float time) { // Warning: do not launch this multiple times at once or things WILL break
		float prevLifetime = fireController.playerProjectileLifetime;
		fireController.playerProjectileLifetime = lifetime;
		yield return new WaitForSeconds (time);
		fireController.playerProjectileLifetime = prevLifetime;
	}

	public float GetCurrentProjectileLifetime() {
		return fireController.playerProjectileLifetime;
	}

	public void DecreaseHealth(float amount) {
		health = Mathf.Clamp (health - amount, 0f, baseHealth);
		if (health == 0) {
			GameManager.GameOver ();
		}
	}
		
	public void IncreaseHealth(float amount) {
		health = Mathf.Clamp (health + amount, 0f, baseHealth);
	}

	public void DecreaseAmmo() {
		DecreaseAmmo (1);
	}

	public void DecreaseAmmo(int amount) {
		ammo -= amount;
		if (ammo < 0) {
			ammo = 0;
		}
	}

	public void IncreaseAmmo(int amount) {
		ammo += amount;
	}

	public void UpdateDamageModifier() {
		if (!overrideDamageModifier) {
			float modifier = Mathf.Clamp (-0.001f * ammo + 1.1f, 0.1f, 1f);
			SetProjectileDamageModifier (modifier);
		}
	}

	public void UpdateFireCooldownModifier() {
		if(!overriderFireCooldown) {
			float fireCooldown = Mathf.Clamp (0.5f * health / baseHealth, 0.1f, 0.5f);
			SetFireCooldown (fireCooldown);
		}
	}

}
