using System.Collections;
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
        playerFacingAngle = GetFacingDirection();

        // Animator cues
        animator.SetFloat("FacingDirection", playerFacingAngle);

        if (horizontal != 0 || vertical != 0) {
            animator.SetBool("IsMoving", true);
        }
        else {
            animator.SetBool("IsMoving", false);
        }
    }

    float GetFacingDirection() {
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

        return angleDegrees;
    }
}
