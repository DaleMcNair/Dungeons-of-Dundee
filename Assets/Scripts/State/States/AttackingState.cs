using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : IState
{
    Enemy enemy;
    bool attacked = false;

    public AttackingState(Enemy enemy) { this.enemy = enemy; }

    public void Enter()
    {
        if (enemy is GoblinWizard)
        {
            enemy.animator.SetTrigger("Attacking");
        }
    }

    public void Execute()
    {
        if (!attacked)
        {
            enemy.Attack();
            attacked = true;
        }

        enemy.stateMachine.ChangeState(new RecoveringState(enemy));
    }

    public void Exit()
    {
    }
}
