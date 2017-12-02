﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    [SerializeField]
    private float movementSpeed = 10f;

    private void Update () {
		float horizontal = Mathf.Clamp(Input.GetAxis("xMovementController") + Input.GetAxis("xMovementKeyboard"), -1, 1);
		float vertical = Mathf.Clamp(Input.GetAxis("yMovementController") + Input.GetAxis("yMovementKeyboard"), -1, 1);
        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
    }

}
