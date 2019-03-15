using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float timeBetweenSwings = 500f;
    float nextAttackTime;

    //Animator animator;

    private void Awake() {
        //animator = GetComponent<Animator>();
    }

    public void Update() {
        if (Input.GetAxis("Fire1") > 0) {
            Attack();
        }
    }

    public void Attack() {
        if (Time.time > nextAttackTime) {
            Debug.Log("fire");
            nextAttackTime = Time.time + timeBetweenSwings / 1000;
            // Swing our weapon here...
            // Play a swing sound here...
        }
    }

    public void Aim(Vector2 aimPoint) {
        transform.LookAt(aimPoint);
    }
}
