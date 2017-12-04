using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FragmentingEnemyBehaviour : EnemyBehaviour {
    private GameObject _player;

    private float _fullHealth;
    public int Stage;
    [SerializeField] private List<GameObject> _forms;
    // Use this for initialization
    void Start () {
        _fullHealth = Health = 100;
        Damage = 5;
        _player = GameObject.Find("Player");
        Exp = 5;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    GetComponent<NavMeshAgent>().destination=(_player.transform.position);
	    transform.GetChild(0).eulerAngles = new Vector3(45,0,0);
	    transform.GetChild(0).GetChild(0).localEulerAngles = transform.eulerAngles;
        if (Stage<2 &&Health / _fullHealth<=0.5f)
	    {
	        for (int i = 0; i < 2; i++)
	        {
	            var obj = Instantiate(_forms[Stage+1],transform.position,transform.rotation);
	            //obj.transform.localScale *= 0.5f;
	            obj.GetComponent<FragmentingEnemyBehaviour>().Health = Health;
	            obj.GetComponent<FragmentingEnemyBehaviour>().FullHealth = Health;
	            obj.GetComponent<FragmentingEnemyBehaviour>().Stage=Stage+1;
	            
                obj.GetComponent<BoxCollider>().size =new Vector3(obj.GetComponent<BoxCollider>().size.x,obj.GetComponent<BoxCollider>().size.y*1.5f,obj.GetComponent<BoxCollider>().size.z); 
	        }
            Destroy(this.gameObject);
	    }
	}

    public float FullHealth
    {
        get { return _fullHealth; }
        set { _fullHealth = value; }
    }
}
