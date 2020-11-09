using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : Entity {
    public enum EnemyType { Caster, Ranged, Melee, Hybrid };

    public EnemyType enemyType;

    public GameObject target { get; private set; }
    bool foundPlayer;
    AILerp path;
    AIDestinationSetter destination;

    public bool inRangeOfPlayer { get; private set; }
    public bool lineOfSightToPlayer { get; private set; }
    public LayerMask lineOfSightMask;
    public float lineOfSightDistance;
    float outOfSightTimer = 0f;

    [HideInInspector]
    public bool attacking { get; private set; }

    public float aimTime;
    public float recoveryTime;
    public float damage;
    public float attackRange;
    public GameObject firePoint, firePointFlipped;

    public float timeBetweenAttacks;

    float myCollisionRadius;
    float targetCollisionRadius;

    public Animator animator { get; private set; }

    public StateMachine stateMachine = new StateMachine();

    public virtual void Awake() {
        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null) {
            foundPlayer = true;
            lineOfSightToPlayer = false;
            destination = FindObjectOfType<AIDestinationSetter>();
            destination.target = target.transform;

            path = GetComponent<AILerp>();
        }

        if (gameObject.TryGetComponent<Animator>(out Animator _animator))
        {
            animator = _animator;
        }

        stateMachine.ChangeState(new IdleState(this));
    }

    public override void Start() {
        base.Start();

        if (foundPlayer) {
            target.GetComponent<Player>().OnDeath += OnTargetDeath;
        }
    }

    public virtual void Update() {
        if (inRangeOfPlayer && lineOfSightToPlayer)
        {
            if (stateMachine.currentState.ToString() == "IdleState")
            {
                stateMachine.ChangeState(new AimingState(this));
            }
        }
        stateMachine.Update();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public virtual void Attack()
    {
        if (!lineOfSightToPlayer)
        {
            stateMachine.ChangeState(new IdleState(this));
            Debug.Log("LoS was lost during casting");
        }
    }

    void OnTargetDeath() {
        foundPlayer = false;
        stateMachine.ChangeState(new IdleState(this));
    }

    public void SetAggro(bool aggro)
    {
        if (aggro)
        {
            CheckLineOfSight();
            inRangeOfPlayer = true;
            path.canSearch = true;
        }
        else
        {
            // for now this can never be called
            inRangeOfPlayer = false;
            path.canSearch = false;
        }
    }

    void CheckLineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position - transform.position, lineOfSightDistance, lineOfSightMask);
        if (hit.collider != null) 
        { 
            //Debug.Log("There was a hit! :" + hit.collider.gameObject.name); 
            lineOfSightToPlayer = hit.collider.CompareTag("Player"); 
        }
    }
}
