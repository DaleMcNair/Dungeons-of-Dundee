using UnityEngine;
using UnityEngine.XR;

public class ChaosWizard : Enemy
{
    public ChaosWizardType type;

    public GameObject projectile;
    public CastingCircle castingCircle;

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
            CastingCircle cc = Instantiate(castingCircle, firePoint.transform.position, Quaternion.identity);
            cc.caster = this;
            cc.target = target.transform;
        }
    }
}
