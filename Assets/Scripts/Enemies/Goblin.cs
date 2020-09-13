using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GoblinTween))]
public class Goblin : Enemy
{
    public GoblinTween tween;
    void Awake()
    {
        tween = GetComponentInChildren<GoblinTween>();
    }

    public virtual void Attack ()
    {

    }
}
