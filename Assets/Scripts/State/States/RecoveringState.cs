using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveringState : IState
{
    float recoveryTimer = Time.time;
    GoblinTween tween = null;
    Enemy enemy;

    public RecoveringState(Enemy enemy) { this.enemy = enemy; }

    public void Enter()
    {
        recoveryTimer += enemy.recoveryTime;

        if (enemy is Goblin)
        {
            tween = enemy.GetComponentInChildren<GoblinTween>();
            tween.StartTween();
        }

        if (enemy is GoblinWizard)
        {
            enemy.animator.SetTrigger("Idle");
        }
    }

    public void Execute()
    {
        if (Time.time > recoveryTimer)
        {
            enemy.stateMachine.ChangeState(new AimingState(enemy));
        }
    }

    public void Exit()
    {
        if (enemy is Goblin)
        {
            Debug.Log("calling exit");
            tween.StopTween();
            Debug.Log("called exit");
        }
    }
}
