using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    [SerializeField]
    private float movementSpeed = 10f;

    private void Update () {
        float horizontal = Input.GetAxis("xMovement");
        float vertical = Input.GetAxis("yMovement");

        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
    }

}
