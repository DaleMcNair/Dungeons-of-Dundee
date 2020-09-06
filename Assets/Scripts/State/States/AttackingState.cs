using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : IState
{
    Enemy owner;

    public AttackingState(Enemy owner) { this.owner = owner; }

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
