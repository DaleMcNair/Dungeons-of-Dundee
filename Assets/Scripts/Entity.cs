﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable {
    public float startingHealth, maxHealth;
    public float currentHealth { get; protected set; }

    protected bool dead;

    public float currentSpeed; // Used incase we cast movement imparing effects on the entity
    public float defaultSpeed;

    public event System.Action OnDeath;

    // public ParticleSystem deathParticle --- Placeholder lol, add this Sprint2

    protected virtual void Start() {
        currentHealth = startingHealth;
        currentSpeed = defaultSpeed;
    }

    public virtual void TakeHit(float amount, Vector2 hitPoint, Vector2 hitDirection) {
        // Here we can use the additional parameters to instantiate a particle system with some positional context
        TakeDamage(amount);
    }

    public virtual void TakeDamage(float amount) {
        Debug.Log("Enemy taking " + amount + " damage. Total health remaining: " + (currentHealth - amount));
        currentHealth -= amount;

        if (currentHealth <= 0 && !dead) {
            Die();
        }
    }

    void RecoverHealth(int amount) {
        currentHealth += amount;

        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    void IncreaseMaxHealth(int amount) {
        maxHealth += amount;
    }

    void DecreaseMaxHealth(int amount) {
        maxHealth -= amount;
    }

    [ContextMenu("Self Destruct")]
    void Die() {
        // Bad luck :(
        // Add death particles here, for now just destroy gameobject

        dead = true;
        if (OnDeath != null) {
            OnDeath();
        }

        GameObject.Destroy(gameObject);
    }
}
