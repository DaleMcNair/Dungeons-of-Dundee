using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {
    public enum EnemyType { Ranged, Melee };

    public EnemyType enemyType;

    Transform target;
    Entity targetEntity;
    bool hasTarget;

    public float damage;

    float attackDistanceThreshold = 1f;
    public float timeBetweenAttacks = 500f;
    float nextAttackTime;

    float myCollisionRadius;
    float targetCollisionRadius;

    private void Awake() {
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            hasTarget = true;
            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<Entity>();

            myCollisionRadius = GetComponent<CircleCollider2D>().radius;
            targetCollisionRadius = target.GetComponent<CircleCollider2D>().radius;
        }
    }

    protected override void Start() {
        base.Start();

        if (hasTarget) {
            targetEntity.OnDeath += OnTargetDeath;
        }
    }

    private void Update() {
        if (enemyType == EnemyType.Melee && hasTarget) {
            if (Time.time > nextAttackTime) {
                float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
                if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2)) {
                    targetEntity.TakeDamage(damage);
                    Debug.Log("Enemy attacked player for " + damage + " damage. Player has " + targetEntity.currentHealth + " remaining.");
                    nextAttackTime = Time.time + timeBetweenAttacks / 1000;
                }
            }
        }
    }

    void OnTargetDeath() {
        hasTarget = false;
    }
}
