using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public Projectile projectile;

    public float timeBetweenAttacks = 300f;
    float nextAttackTime;
    float recoilAngle;

    public int damage;
    public float attackRange;
    public LayerMask enemyLayerMask;
    public bool isAttacking;

    public Animator animator;

    [Header("Projectile Spawners")]
    public Transform projectileSpawnTop;
    public Transform projectileSpawnBottom;
    public Transform projectileSpawnRight;
    public Transform projectileSpawnLeft;

    Transform activeProjectileSpawn;

    public void Attack(Vector2 facing) {
        Debug.Log("facing: " + facing);
        activeProjectileSpawn = GetSpawner(facing);

        Vector2 projectileDirection = new Vector2(activeProjectileSpawn.position.x, activeProjectileSpawn.position.y) + facing.normalized;
        if (Time.time > nextAttackTime) {
            nextAttackTime = Time.time + timeBetweenAttacks / 1000;
            animator.SetTrigger("Attacking");

            Vector3 worldMousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePosition = new Vector2(worldMousePosition3D.x, worldMousePosition3D.y);
            Vector2 direction = mousePosition - activeProjectileSpawn.position;


            Projectile newProjectile = Instantiate(projectile, activeProjectileSpawn.position, Quaternion.identity) as Projectile;
            newProjectile.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);


            // Swing our weapon here...
            // Play a swing sound here...
        }
    }

    Transform GetSpawner(Vector2 facing) {
        //use atan2 to get the angle; Atan2 returns radians
        float angleRadians = Mathf.Atan2(facing.y, facing.x);

        //convert to degrees
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        //angleDegrees will be in the range (-180,180].
        //I like normalizing to [0,360) myself, but this is optional..
        if (angleDegrees < 0)
            angleDegrees += 180;

        Debug.Log(angleDegrees);

        if (angleDegrees > 315) {
            return projectileSpawnRight;
        }
        else if (angleDegrees > 225) {
            return projectileSpawnBottom;
        }
        else if (angleDegrees > 135) {
            return projectileSpawnLeft;
        }
        else if (angleDegrees > 45) {
            return projectileSpawnTop;
        }
        return projectileSpawnRight;
    }

    public void Aim(Vector2 aimPoint) {
        transform.LookAt(aimPoint);
    }
}
