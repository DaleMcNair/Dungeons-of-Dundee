using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public LayerMask collisionMask;
    float speed = 15f;
    float damage = 1f;

    float lifetime = 3f;
    float skinWidth = .1f;


    void Start() {
        Destroy(gameObject, lifetime);

        Collider2D initialCollisions = Physics2D.OverlapCircle(transform.position, .15f, collisionMask);
        if (initialCollisions) {
            OnHitObject(initialCollisions, transform.position);
        }

        SetSpeed(speed);
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    // Update is called once per frame
    void Update() {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.right * moveDistance);
    }

    void CheckCollisions(float moveDistance) {
        Ray2D ray = new Ray2D(transform.position, transform.right);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, transform.right, moveDistance, collisionMask);

        if (hit.Length > 0) {
            for (int i = 0; i < hit.Length; i++) {
                OnHitObject(hit[i].collider, hit[i].point);
            }
        }
    }

    void OnHitObject(Collider2D c, Vector2 hitPoint) {
        IDamageable damageableObject = c.GetComponent<IDamageable>();
        if (damageableObject != null) {
            damageableObject.TakeHit(damage, hitPoint, transform.forward);
        }
        Destroy(gameObject);
    }
}