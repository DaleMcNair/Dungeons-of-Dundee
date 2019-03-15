using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Idle, Moving };

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    Vector2 velocity;
    Rigidbody2D myRigidbody;

    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Use fixed delta to account for dropped frames when moving our character
    void FixedUpdate() {
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
    }

    public void Move(Vector2 _velocity) {
        velocity = _velocity;
    }

    public void LookAt(Vector2 lookPoint) {
        transform.LookAt(lookPoint);
    }
}
