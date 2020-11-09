using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingCircle : MonoBehaviour
{
    [HideInInspector]
    public ChaosWizard caster;
    [HideInInspector]
    public Transform target;

    public float castTime;
    private float castTimer = 0f;

    public float fadeInTime;
    private float fadeTimer = 0f;
    
    public GameObject castingCirclePrefab { get; private set; }

    void Start()
    {
        StartCoroutine(ActivateCastingCircle());
    }

    private void OnDestroy()
    {
        StopCoroutine(ActivateCastingCircle());
    }

    IEnumerator ActivateCastingCircle()
    {
        Vector2 targetPos = target.transform.position;
        Vector2 direction = targetPos - (Vector2)transform.position;
        Quaternion projectileRotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);

        while (castTimer < castTime)
        {
            castTimer += Time.deltaTime;
        }

        Instantiate(caster.projectile, caster.firePoint.transform.position, projectileRotation);

        Destroy(this);

        yield return null;
    }
}
