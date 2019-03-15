using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : Entity {
    PlayerController controller;

    // public Animator animator; // Placeholder for old mate McFifffffeeigh
    // public SpriteRenderer spriteRenderer;

    protected override void Start() {
        base.Start();
    }

    private void Awake() {
        controller = GetComponent<PlayerController>();
    }

    void Update() {
        // Movement Input
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 movementVelocity = movementInput.normalized * currentSpeed;
        controller.Move(movementVelocity);
        Debug.Log(movementInput);

        // Placeholders once sprites are dun
        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Magnitude", movement.z);
    }
}
