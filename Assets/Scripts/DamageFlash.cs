using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    Material mat;
    public float flashTime = 0.25f;
    public float flashOpacity = 0.625f;
    public float flashIntensity = 1f;
    private float flashTimer = 0f;

    void Awake()
    {
        mat = GetComponentInChildren<SpriteRenderer>()?.material;
    }

    private void OnEnable()
    {
        mat.EnableKeyword("HITEFFECT_ON");
        flashTimer = 0f;
    }

    private void OnDisable()
    {
        flashTimer = 0f;
        mat.SetFloat("_HitEffectBlend", 0f);
        mat.DisableKeyword("HITEFFECT_ON");
    }

    void Update()
    {
        if (flashTimer < flashTime)
        {            
            mat.SetFloat("_HitEffectBlend", Mathf.Lerp(0f, flashOpacity, flashTimer / flashTime));
            flashTimer += Time.deltaTime;
            
        } else
        {
            enabled = false;
        }
    }
}
