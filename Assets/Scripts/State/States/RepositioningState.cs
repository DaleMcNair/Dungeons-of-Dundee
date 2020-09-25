using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositioningState : IState
{
    Enemy enemy;
    GoblinTween tween = null;
    int repositionedCount = 0;
    public RepositioningState(Enemy enemy) { this.enemy = enemy; }

    public void Enter()
    {
        if (enemy is Goblin)
        {
            tween = enemy.GetComponentInChildren<GoblinTween>();
            tween.StartTween();
        }
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        if (enemy is Goblin) tween.StopTween();
    }
}
