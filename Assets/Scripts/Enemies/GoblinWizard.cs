using UnityEngine;
using UnityEngine.XR;

public class GoblinWizard : Goblin
{
    public GoblinWizardType type;

    public GameObject projectile;

    public GameObject aimingParticleGameObject;

    public override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();

        if (target)
        {
            Vector2 targetPos = target.transform.position;
            Vector2 direction = targetPos - (Vector2)transform.position;
            Quaternion projectileRotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);

            Instantiate(projectile, firePoint.transform.position, projectileRotation);

            if (type == GoblinWizardType.Default)
            {
                Quaternion projectileRotation2 = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 25, Vector3.forward);
                Instantiate(projectile, firePoint.transform.position, projectileRotation2);

                Quaternion projectileRotation3 = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 25, Vector3.forward);
                Instantiate(projectile, firePoint.transform.position, projectileRotation3);
            }
        }
    }
}
