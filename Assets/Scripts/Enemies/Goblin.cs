using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    [HideInInspector]
    public GoblinTween tween;
    public override void Awake()
    {
        base.Awake();
        tween = GetComponentInChildren<GoblinTween>();
    }

    public override void Attack ()
    {

    }

    public override void Start()
    {
        base.Start();
    }
}
