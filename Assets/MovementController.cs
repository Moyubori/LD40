using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    [SerializeField]
    private float movementSpeed = 10f;

    private bool collision_happened = false;

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("dd");
        collision_happened = true;
    }
    private void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (collision_happened) {
            collision_happened = false;
        } else {
            transform.position += new Vector3(horizontal * movementSpeed, vertical * movementSpeed, 0);
        }
    }

}
