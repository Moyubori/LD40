using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    [SerializeField]
    private GameObject _particles;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public float Health { get; set; }


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
            Destroy(this.gameObject);
        }
    }
}
