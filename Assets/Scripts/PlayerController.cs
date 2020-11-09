using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum PlayerState { Idle, Moving };

public class PlayerController : MonoBehaviour {

    Vector2 velocity;
    Rigidbody2D myRigidbody;
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    void Start() {
        myRigidbody = GetComponentInParent<Rigidbody2D>();
    }

    // Use fixed delta to account for dropped frames when moving our character
    void FixedUpdate() {
        //myRigidbody.velocity = velocity;
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
        //myRigidbody.velocity = new Vector2(player.horizontal * player.defaultSpeed, player.vertical * player.defaultSpeed);
    }

    public void Move(Vector2 _velocity) {
        velocity = _velocity;
    }

    public void Move(float horizontal, float vertical)
    {

    }

    public void LookAt(Vector2 lookPoint) {
        transform.LookAt(lookPoint);
    }
}
