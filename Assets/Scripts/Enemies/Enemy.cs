 using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : Entity {
    public enum EnemyType { Caster, Ranged, Melee, Hybrid };

    public EnemyType enemyType;

    public GameObject target { get; private set; }
    AIPath path;
    AIDestinationSetter destination;

    public bool inRangeOfPlayer { get; private set; }
    public bool lineOfSightToPlayer { get; private set; }
    public LayerMask lineOfSightMask;
    public float lineOfSightDistance;

    [HideInInspector]
    public bool attacking { get; private set; }

    public float aimTime;
    public float recoveryTime;
    public float damage;
    public float attackRange;
    public GameObject firePoint, firePointFlipped;

    public float timeBetweenAttacks;

    public Animator animator { get; private set; }
    Task checkLineOfSightTask;
    Task waitForEndOfPath;

    public StateMachine stateMachine = new StateMachine();

    public virtual void Awake() {
        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null) {
            lineOfSightToPlayer = false;
            destination = FindObjectOfType<AIDestinationSetter>();
            destination.target = target.transform;

            path = GetComponent<AIPath>();

            checkLineOfSightTask = new Task(CheckLineOfSight(), false);
            waitForEndOfPath = new Task(WaitForEndOfPath(), false);

            checkLineOfSightTask.Finished += delegate (bool manual) {
                waitForEndOfPath.Start();
            };

            waitForEndOfPath.Finished += delegate (bool manual) {
                path.canSearch = false;
                path.canMove = false;
                stateMachine.ChangeState(new IdleState(this));
            };
        }

        if (gameObject.TryGetComponent<Animator>(out Animator _animator))
        {
            animator = _animator;
        }

        stateMachine.ChangeState(new IdleState(this));
    }

    public override void Start() {
        base.Start();

        Debug.Log("target = " + target.gameObject.name);
        if (target != null) {
            target.GetComponent<Player>().OnDeath += OnTargetDeath;
        }
    }

    public virtual void Update() {
        
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public virtual void Attack() {}

    void OnTargetDeath() {
        stateMachine.ChangeState(new IdleState(this));
    }

    public void SetWithinRange()
    {
        if (LineOfSightCheck())
        {
            inRangeOfPlayer = true;
            path.canSearch = true;
            path.canMove = true;

            if (!checkLineOfSightTask.Running) checkLineOfSightTask.Start();
        }
    }

    void CheckForAttack()
    {
        Debug.Log("Checking for attack for " + gameObject.name + " " + "... Range = " + Vector3.Distance(target.transform.position, transform.position));
        inRangeOfPlayer = Vector3.Distance(target.transform.position, transform.position) < attackRange;
        if (inRangeOfPlayer && lineOfSightToPlayer)
        {
            Debug.Log("In range... current state = " + stateMachine.currentState);
            if (stateMachine.currentState.ToString() == "IdleState" || stateMachine.currentState.ToString() == "MovingState") 
            {
                Debug.Log("Begin attack");
                checkLineOfSightTask.Stop();
                stateMachine.ChangeState(new AimingState(this));
            }
        }
        stateMachine.Update();
    }

    bool LineOfSightCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position - transform.position, lineOfSightDistance, lineOfSightMask);
        if (hit.collider != null)
        {
            //Debug.Log("There was a hit! :" + hit.collider.gameObject.name); 
            return hit.collider.CompareTag("Player");
        }

        checkLineOfSightTask.Stop();
        return false;
    }

    IEnumerator CheckLineOfSight()
    {
        while (lineOfSightToPlayer)
        {
            CheckForAttack();
            lineOfSightToPlayer = LineOfSightCheck();
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator WaitForEndOfPath()
    {
        bool reachedEndOfPath = false;

        while (!reachedEndOfPath) {
            reachedEndOfPath = path.reachedEndOfPath;
            yield return new WaitForSeconds(0.25f); 
        }
    }
}
