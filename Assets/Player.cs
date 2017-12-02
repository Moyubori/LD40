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
		private set { 
			ammo = value;
		}
	}

	public float health = 100f;

	private void Start () {
		if (movementController == null) {
			movementController = GetComponent<MovementController> ();
		}
		if (fireController == null) {
			fireController = GetComponent<FireController> ();
		}
	}

	public void SetMovementSpeedModifier(float modifier) { // permanently modifies movement speed
		movementController.movementSpeedModifier *= modifier;
	}

	public void SetMovementSpeedModifier(float modifier, float time) { // modifies movement speed for a given time
		StartCoroutine(TimedMovementSpeedModifier(modifier,time));
	}

	private IEnumerator TimedMovementSpeedModifier(float modifier, float time) {
		movementController.movementSpeedModifier *= modifier;
		yield return new WaitForSeconds (time);
		movementController.movementSpeedModifier /= modifier;
	}

	public void SetFireCooldown(float cooldown) { // permanently modifies fire cooldown
		fireController.fireCooldown = cooldown;
	}

	public void SetFireCooldown(float cooldown, float time) { // modifies fire cooldown for a given time
		StartCoroutine(TimedFireCooldownModifier(cooldown, time));
	}

	private IEnumerator TimedFireCooldownModifier(float cooldown, float time) { // Warning: do not launch this multiple times at once or things WILL break
		float prevCooldown = fireController.fireCooldown;
		fireController.fireCooldown = cooldown;
		yield return new WaitForSeconds (time);
		fireController.fireCooldown = prevCooldown;
	}

	public void RevertFireCooldownToBaseValue() {
		fireController.RevertFireCooldownToBaseValue ();
	}

	public void SetProjectileDamageModifier(float modifier) { // permanently modifies projectile damage
		fireController.playerProjectileDamageMofidier *= modifier;
	}

	public void SetProjectileDamageModifier(float modifier, float time) { // modifies prtojectile damage for a given time
		StartCoroutine(TimedProjectileDamageModifier(modifier,time));
	}

	private IEnumerator TimedProjectileDamageModifier(float modifier, float time) {
		fireController.playerProjectileDamageMofidier *= modifier;
		yield return new WaitForSeconds (time);
		fireController.playerProjectileDamageMofidier /= modifier;
	}

	public void SetProjectileLifetime(float cooldown) { // permanently modifies projectile lifetime
		fireController.fireCooldown = cooldown;
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


}
