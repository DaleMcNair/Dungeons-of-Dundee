using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Idle, Moving };

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 3f;
    public float dodgeSpeed = 5f;

    // public Animator animator; // Placeholder for old mate McFifffffeeigh
    // public SpriteRenderer spriteRenderer;

    void Update() {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Magnitude", movement.z);

        transform.position = transform.position + (movement * moveSpeed) * Time.deltaTime;
    }
}
