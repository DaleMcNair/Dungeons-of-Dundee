using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingState : IState
{
    Enemy enemy;

    public AimingState(Enemy enemy) { this.enemy = enemy; }

    public void Enter()
    {
        Debug.Log("Entering Aiming State");
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
