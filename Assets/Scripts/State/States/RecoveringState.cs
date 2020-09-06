using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveringState : IState
{
    Enemy owner;

    public RecoveringState(Enemy owner) { this.owner = owner; }

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
