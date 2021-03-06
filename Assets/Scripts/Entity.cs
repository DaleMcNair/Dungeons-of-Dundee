﻿using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable {
    public float startingHealth, maxHealth;
    public float currentHealth { get; protected set; }

    protected bool dead;

    public float currentSpeed; // Used incase we cast movement imparing effects on the entity
    public float defaultSpeed;

    public GameObject corpse;

    public event System.Action OnDeath;

    SpriteRenderer spriteRenderer;

    // public ParticleSystem deathParticle --- Placeholder lol, add this Sprint2

    private void Awake()
    {
         spriteRenderer = GetComponent<SpriteRenderer>();

        if (!spriteRenderer)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (!spriteRenderer)
        {
            GetComponentInParent<SpriteRenderer>();
        }
    }

    public virtual void Start() {
        currentHealth = startingHealth;
        currentSpeed = defaultSpeed;
    }

    public virtual void LateUpdate()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        if (spriteRenderer) { Debug.Log("got sprite"); spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -1; }
    }

    public virtual void TakeHit(float amount, Vector2 hitPoint, Vector2 hitDirection) {
        // Here we can use the additional parameters to instantiate a particle system with some positional context
        TakeDamage(amount);
    }

    public virtual void TakeDamage(float amount) {
        currentHealth -= amount;

        if (currentHealth <= 0 && !dead) {
            Die();
        }
    }

    public virtual void RecoverHealth(float amount) {
        currentHealth += amount;

        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public virtual void IncreaseMaxHealth(int amount) {
        maxHealth += amount;
    }

    public virtual void DecreaseMaxHealth(int amount) {
        maxHealth -= amount;
    }

    [ContextMenu("Self Destruct")]
    public virtual void Die() {
        // Bad luck :(
        // Add death particles here, for now just destroy gameobject

        dead = true;
        OnDeath?.Invoke();

        Instantiate(corpse, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
