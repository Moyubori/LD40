using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public MovementController PlayerController;
    
	
    void Update()
    {
        GetComponent<Animator>().SetFloat("Speed",
            PlayerController.gameObject.GetComponent<Rigidbody>().velocity.magnitude / (PlayerController.MaxSpeed*0.7f));
        GetComponent<Animator>().SetFloat("Shooting",
            PlayerController.gameObject.GetComponent<Player>().FireController.Firing ? 0.5f : 0f);
    }

}
