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
        Vector2 movementVelocity = movementInput.normalized * (currentSpeed * movementInput);
        controller.Move(movementVelocity);

        // Aiming
        GetMousePosition();

        // Placeholders once sprites are dun
        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Magnitude", movement.z);
    }

    void GetMousePosition() {
        //get the vector representing the mouse's position relative to the point...
        Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        //use atan2 to get the angle; Atan2 returns radians
        float angleRadians = Mathf.Atan2(v.y, v.x);

        //convert to degrees
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        //angleDegrees will be in the range (-180,180].
        //I like normalizing to [0,360) myself, but this is optional..
        if (angleDegrees < 0)
            angleDegrees += 360;

        Debug.Log(angleDegrees);
    }
}
