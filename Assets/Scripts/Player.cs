using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(WeaponController))]
public class Player : Entity {
    PlayerController controller;
    WeaponController weaponController;

    public float playerFacingAngle;

    public Animator animator;

    protected override void Start() {
        playerFacingAngle = 270f;
        base.Start();
    }

    private void Awake() {
        controller = GetComponent<PlayerController>();
        weaponController = GetComponent<WeaponController>();
    }

    void Update() {
        // Movement Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movementVelocity = new Vector2(horizontal * currentSpeed, vertical * currentSpeed);
        controller.Move(movementVelocity);

        // Attacking Input
        if (Input.GetMouseButtonDown(0)) {
            weaponController.Attack(movementVelocity);
        }

        // Movement Direction stuff

        playerFacingAngle = GetFacingDirection(movementVelocity);

        // Animator cues


        if (horizontal != 0 || vertical != 0) {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("FacingDirection", playerFacingAngle);
        }
        else {
            animator.SetBool("IsMoving", false);
        }
    }

    float GetFacingDirection(Vector2 vel) {
        // Atan2 returns radians
        float angleRadians = Mathf.Atan2(vel.y, vel.x);

        // Convert to degrees
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        if (angleDegrees < 0) {
            angleDegrees += 360;
        }

        return angleDegrees;
    }

    public override void Die() {
        animator.SetBool("IsDead", true);
        base.Die();
    }
}
