using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    [SerializeField]
    private GameObject _particles;

    [SerializeField] private float _damage;

    [SerializeField] private List<GameObject> _ammoBoxes;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public float Health { get; set; }

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    void OnCollisionEnter(Collision col)
    {
        var projectile = col.gameObject.GetComponent<PlayerProjectile>();
        if(projectile != null)
        TakeDamage(projectile.damage);
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Instantiate(_particles, transform.position, transform.rotation);
            DropAmmo();
            Destroy(this.gameObject);
        }
    }

    public void DropAmmo()
    {
        if (Random.Range(0, 100) > 50)
        {
            Instantiate(_ammoBoxes[Random.Range(0, _ammoBoxes.Count)], transform.position, transform.rotation);
        }
    }
}
