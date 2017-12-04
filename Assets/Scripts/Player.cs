    using System.Collections;
using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.PostProcessing;

public class Player : MonoBehaviour {

	[SerializeField]
	private MovementController movementController;
	[SerializeField]
	private FireController fireController;

    [SerializeField] private PostProcessingProfile profile;
    public GUISkin skin;
    public GameObject Life,AmmoContainer;
    public GameObject AmmoSprite;
    public List<GameObject> AmmoList;
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

    private float damageTimer=0;
    [SerializeField]
    private int experience;
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
		UpdateDamageModifier ();
		UpdateFireCooldownModifier ();
	    damageTimer += Time.deltaTime;
	    while (Ammo != AmmoList.Count)
	    {
	        if (Ammo < AmmoList.Count)
	        {
	            var a = AmmoList.Last();

                AmmoList.Remove(a);
                Destroy(a);
	        }
	        else
	        {
	            var am = Instantiate(AmmoSprite);
                am.transform.SetParent(AmmoContainer.transform);
	            if (AmmoList.Count > 0)
	                am.GetComponent<RectTransform>().anchoredPosition =
	                    AmmoList.Last().GetComponent<RectTransform>().anchoredPosition + new Vector2(0, -5);
	            else
	            {
	                am.GetComponent<RectTransform>().anchoredPosition =
	                    AmmoSprite.GetComponent<RectTransform>().anchoredPosition;
	            }
                AmmoList.Add(am);
	        }
	    }
	}
		
	private void OnTriggerEnter(Collider collider) {
		if (collider.tag == "Collectible") {
			collider.GetComponent<Collectible> ().Collect();
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
		fireController.playerProjectileDamageMofidier *= modifier;
		yield return new WaitForSeconds (time);
		fireController.playerProjectileDamageMofidier /= modifier;
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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy" )
        {
            health -= col.gameObject.GetComponent<EnemyBehaviour>().Damage;
            GetComponent<CameraShake>().ShakeDuration = 0.1f;
        }
        else if (col.gameObject.tag == "EnemyProjectile")
        {
            health -= col.gameObject.GetComponent<EnemyProjectile>().damage;
            GetComponent<CameraShake>().ShakeDuration = 0.1f;
        }
        Life.GetComponent<RectTransform>().sizeDelta =new Vector2( 140 * health / baseHealth,16);
        ChromaticAberrationModel.Settings effectSettings = profile.chromaticAberration.settings;
        effectSettings.intensity = (1 - health / baseHealth)*2;
        
        profile.chromaticAberration.enabled = true;
        profile.chromaticAberration.settings=effectSettings;
        damageTimer = 0;
       
    }

    private IEnumerator HitIndicator()
    {
        yield return null;
    }
    private void OnCollisionStay(Collision col)
    {
        if (damageTimer < 0.4f) return;
        if (col.gameObject.tag == "Enemy")
        {
            health -= col.gameObject.GetComponent<EnemyBehaviour>().Damage;
        }
        else if (col.gameObject.tag == "EnemyProjectile")
        {
            health -= col.gameObject.GetComponent<EnemyProjectile>().damage;
        }
        Life.GetComponent<RectTransform>().sizeDelta = new Vector2(140 * health / baseHealth, 16);
        damageTimer = 0;
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
            //Debug.Log(Mathf.Clamp((-Mathf.Log10(ammo) / 2 + 1) * 10, 0.1f, 100f));
			float modifier = Mathf.Clamp ((-Mathf.Log10(ammo) / 2 + 1) * 10, 0.1f, 100f);
			SetProjectileDamageModifier (modifier);
		}
	}

	public void UpdateFireCooldownModifier() {
		if(!overriderFireCooldown) {
			float fireCooldown = Mathf.Clamp (0.2f * health / baseHealth, 0.1f, 0.5f);
			SetFireCooldown (fireCooldown);
		}
	}

    public void AddExperience(int exp)
    {
        experience += exp;
        if (experience >= 100)
        {
            StartCoroutine(ShowLevelUpMessage());
            experience = 0;
        }
    }

    private IEnumerator ShowLevelUpMessage()
    {
        LeveledUp = true;
        yield return new WaitForSeconds(1.5f);
        LeveledUp = false;
    }

    void OnGUI()
    {
        if (LeveledUp)
        {
            
                    GUI.Box(new Rect(Screen.width / 2, Screen.height / 2 - 30, 300, 200), "Level UP!", skin.GetStyle("EquipmentInfo"));
                   
                
        }
    }

    public bool LeveledUp { get; set; }

    public FireController FireController
    {
        get { return this.fireController; }
    }

}
