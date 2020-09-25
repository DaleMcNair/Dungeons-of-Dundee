using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveringState : IState
{
    float recoveryTimer = Time.time;
    Enemy enemy;

    public RecoveringState(Enemy enemy) { this.enemy = enemy; }

    public void Enter()
    {
        recoveryTimer += enemy.recoveryTime;

        if (enemy is Goblin)
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
    }
}
