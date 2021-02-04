using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(WeaponController))]
public class Player : Entity {
    PlayerController controller;
    WeaponController weaponController;

    public Camera cam;

    Vector2 movementVelocity;
    Vector2 previousMovementVelocity;
    public float horizontal { get; private set; } 
    public float vertical { get; private set; }

    Vector2 mousePos = new Vector2(0, 0);

    public Animator animator;

    public float aggroRadius;
    Collider2D[] enemiesInRange;
    public LayerMask lineOfSightMask;
    public float lineOfSightDistance;

    DamageFlash damageFlash;

    public event System.Action<bool> OnAggro;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        weaponController = GetComponent<WeaponController>();
        damageFlash = GetComponentInChildren<DamageFlash>();
    }

    public override void Start() {
        base.Start();

        StartCoroutine(EnemyAggroRangeCheck());
    }

    void Update() {
        // Movement Input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Mouse Input
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - (Vector2) gameObject.transform.position;
        float mouseAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 180f;
        SetDirection(mouseAngle);

        movementVelocity = new Vector2(horizontal * currentSpeed, vertical * currentSpeed);

        // Movement Direction stuff / animator cues

        if (horizontal != 0 || vertical != 0) {
            animator.SetBool("IsMoving", true);
            previousMovementVelocity = movementVelocity;
        }
        else {
            animator.SetBool("IsMoving", false);
        }

        // Attacking Input
        if (Input.GetMouseButtonDown(0)) {
            if (EventSystem.current && EventSystem.current.IsPointerOverGameObject()) {
                return;
            }
            else {
                if (horizontal == 0 && vertical == 0) {
                    weaponController.Attack(previousMovementVelocity);

                }
                else {
                    weaponController.Attack(movementVelocity);
                }
            }
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        controller.Move(movementVelocity);
    }

    void SetDirection(float mouseAngle) {
        if (mouseAngle < 45 || mouseAngle >= 315)
        {
            //Left
            animator.SetInteger("MoveDirection", 4);
        }
        else if (mouseAngle > 225)
        {
            //Up
            animator.SetInteger("MoveDirection", 1);
        }
        else if (mouseAngle > 135)
        {
            //Right
            animator.SetInteger("MoveDirection", 2);
        }
        else
        {
            //Down
            animator.SetInteger("MoveDirection", 3);
        }
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        damageFlash.enabled = true;
    }

    public override void Die() {
        StopAllCoroutines();
        animator.SetBool("IsDead", true);
        base.Die();
    }

    public IEnumerator EnemyAggroRangeCheck()
    {
        LayerMask enemyMask = LayerMask.GetMask("Enemy");

        while (true)
        {
            enemiesInRange = Physics2D.OverlapCircleAll(transform.position, aggroRadius, enemyMask);

                foreach (Collider2D e in enemiesInRange)
                {
                    Enemy enemy = e.GetComponent<Enemy>();

                    if (enemy != null)
                    {
                        enemy.SetWithinRange();
                        //RaycastHit2D hit = Physics2D.Raycast(transform.position, enemy.transform.position - transform.position, lineOfSightDistance, lineOfSightMask);
                        //if (hit.collider != null)
                        //{
                        //    //Debug.Log("There was a hit! :" + hit.collider.gameObject.name); 
                        //    if (hit.collider.CompareTag("Enemy"))
                        //    {
                        //        enemy.SetAggro(true);
                        //    } else
                        //    {
                        //        enemy.SetAggro(false);
                        //    }
                        //}
                    }
                }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
