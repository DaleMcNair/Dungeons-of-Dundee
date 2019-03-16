﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : Entity {
    PlayerController controller;
    public float playerFacingAngle;

    public Animator animator;

    protected override void Start() {
        playerFacingAngle = 270f;
        base.Start();
    }

    private void Awake() {
        controller = GetComponent<PlayerController>();
    }

    void Update() {
        // Movement Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movementVelocity = new Vector2(horizontal * currentSpeed, vertical * currentSpeed);
        controller.Move(movementVelocity);

        // Movement Direction stuff
        playerFacingAngle = GetFacingDirection(movementVelocity);

        // Animator cues
        animator.SetFloat("FacingDirection", playerFacingAngle);

        if (horizontal != 0 || vertical != 0) {
            animator.SetBool("IsMoving", true);
        }
        else {
            animator.SetBool("IsMoving", false);
        }
    }

    float GetFacingDirection(Vector2 vel) {
        // Atan2 returns radians
        float angleRadians = Mathf.Atan2(vel.y, vel.x);

        //convert to degrees
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        if (angleDegrees < 0) {
            angleDegrees += 360;
        }

        return angleDegrees;
    }
}
