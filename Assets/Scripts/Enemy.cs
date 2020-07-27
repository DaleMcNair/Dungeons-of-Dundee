using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : Entity {
    public enum EnemyState
    {
        idle,
        running,
        attacking
    }

    public EnemyState currentState;
    public float lastStateChange = 0f;

    public enum EnemyType { Spell, Ranged, Melee };

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

    public Animator animator;

    private void Awake() {
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            hasTarget = true;
            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<Entity>();

            //myCollisionRadius = GetComponent<CircleCollider2D>().radius;
            //targetCollisionRadius = target.GetComponent<CircleCollider2D>().radius;
        }

        if (gameObject.TryGetComponent<Animator>(out Animator _animator))
        {
            Debug.Log(gameObject.name);
            animator = _animator;
        }

        setCurrentState(EnemyState.idle);
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


        if (animator != null)
        {
            if (currentSpeed > 0)
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
    }

    void OnTargetDeath() {
        hasTarget = false;
    }

    private void setCurrentState(EnemyState state)
    {
        lastStateChange = Time.time;
        currentState = state;
    }
}
