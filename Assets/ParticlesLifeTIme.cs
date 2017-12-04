using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesLifeTIme : MonoBehaviour
{
    private float _time=0;

    [SerializeField] private float _lifeTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _time += Time.deltaTime;
	    if (_time > _lifeTime)
	    {
	        Destroy(this.gameObject);
	    }
	}
}
