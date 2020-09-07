using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : IState
{
    Enemy enemy;

    public AttackingState(Enemy enemy) { this.enemy = enemy; }

    public void Enter()
    {
        Debug.Log("Entering Attacking State");
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
