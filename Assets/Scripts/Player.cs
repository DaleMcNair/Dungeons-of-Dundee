using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(WeaponController))]
public class Player : Entity {
    PlayerController controller;
    WeaponController weaponController;

    Vector2 previousMovementVelocity;

    public Animator animator;

    protected override void Start() {
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

        // Movement Direction stuff / animator cues

        if (horizontal != 0 || vertical != 0) {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("DirectionX", horizontal);
            animator.SetFloat("DirectionY", vertical);
            previousMovementVelocity = movementVelocity;
        }
        else {
            animator.SetBool("IsMoving", false);
        }

        // Attacking Input
        if (Input.GetMouseButtonDown(0)) {
            if (EventSystem.current && EventSystem.current.IsPointerOverGameObject()) {
                return;
            }
            else {
                if (horizontal == 0 && vertical == 0) {
                    weaponController.Attack(previousMovementVelocity);

                }
                else {
                    weaponController.Attack(movementVelocity);
                }
            }
        }

        controller.Move(movementVelocity);
    }

    public override void Die() {
        animator.SetBool("IsDead", true);
        base.Die();
    }
}
