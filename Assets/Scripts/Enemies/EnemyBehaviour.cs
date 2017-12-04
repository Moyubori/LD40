using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    [SerializeField]
    private GameObject _particles;

    [SerializeField] private float _damage;

    [SerializeField] private List<GameObject> _ammoBoxes;
    [SerializeField] private GameObject _medKit;
    public float BaseHealth;
    // Use this for initialization
    void OnEnable()
    {
        BaseHealth = Health;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	   
	}
    public float Health { get; set; }
    public int Exp { get; set; }
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    void OnCollisionEnter(Collision col)
    {
        var projectile = col.gameObject.GetComponent<PlayerProjectile>();
        if (projectile != null)
        {
            Debug.Log(projectile.damage);
            TakeDamage(projectile.damage);
            
        }
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Instantiate(_particles, transform.position, transform.rotation);
            DropAmmo();
            GameObject.Find("Player").GetComponent<Player>().AddExperience(Exp);
            Destroy(this.gameObject);
        }
    }

    public void DropAmmo()
    {
        if (Random.Range(0, 100) > 70)
        {
            Instantiate(_ammoBoxes[Random.Range(0, _ammoBoxes.Count)], transform.position, transform.rotation);
            return;;
        }
        if (Random.Range(0, 100) > 70)
        {
            Instantiate(_medKit, transform.position, transform.rotation);
            return; ;
        }

    }
}
