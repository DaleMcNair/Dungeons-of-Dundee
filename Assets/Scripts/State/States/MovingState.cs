using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : IState
{
    Enemy enemy;

    public MovingState(Enemy enemy) { this.enemy = enemy; }

    public void Enter()
    {
        enemy.animator.SetTrigger("Moving");
        Debug.Log("Entering Moving State");
        enemy.animator.SetBool("IsMoving", true);
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
