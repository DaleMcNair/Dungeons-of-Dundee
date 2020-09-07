using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveringState : IState
{
    Enemy enemy;

    public RecoveringState(Enemy enemy) { this.enemy = enemy; }

    public void Enter()
    {
        Debug.Log("Entering Recovering State");
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
