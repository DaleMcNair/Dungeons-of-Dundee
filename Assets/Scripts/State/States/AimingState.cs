using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingState : IState
{
    float aimTimer;

    Enemy enemy;

    public AimingState(Enemy enemy) { this.enemy = enemy; }

    public void Enter()
    {
        aimTimer = Time.time + enemy.aimTime;

        if (enemy is GoblinWizard)
        {
            enemy.animator.SetTrigger("Aiming");
            enemy.GetComponent<GoblinWizard>().aimingParticleGameObject.SetActive(true);
        }
    }

    public void Execute()
    {
        if (Time.time > aimTimer)
        {
            enemy.stateMachine.ChangeState(new AttackingState(enemy));
        }
    }

    public void Exit()
    {
        if (enemy is GoblinWizard)
        {
            enemy.GetComponent<GoblinWizard>().aimingParticleGameObject.SetActive(false);
        }
    }
}
