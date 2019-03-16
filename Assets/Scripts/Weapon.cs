using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float timeBetweenSwings = 500f;
    float nextAttackTime;

    public int damage;
    public float attackRange;
    public LayerMask enemyLayerMask;
    public Transform attackPosition;
    public bool isAttacking;

    public Animator animator;

    //private void Awake() {
    //    animator = GetComponent<Animator>();
    //}

    public void Attack(Vector2 facing) {
        Vector2 swingDirection = new Vector2(attackPosition.position.x, attackPosition.position.y) + facing.normalized;
        if (Time.time > nextAttackTime) {
            nextAttackTime = Time.time + timeBetweenSwings / 1000;
            animator.SetTrigger("Attacking");
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(swingDirection, attackRange, enemyLayerMask);

            for (int i = 0; i < enemiesToDamage.Length; i++) {
                IDamageable damageableObject = enemiesToDamage[i].GetComponent<IDamageable>();

                if (damageableObject != null) {
                    damageableObject.TakeHit(damage, swingDirection, transform.forward);
                }
            }

            // Swing our weapon here...
            // Play a swing sound here...
        }
    }

    public void Aim(Vector2 aimPoint) {
        transform.LookAt(aimPoint);
    }
}
