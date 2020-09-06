using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : IState
{
    Enemy owner;

    public MovingState(Enemy owner) { this.owner = owner; }

    public void Enter()
    {
        Debug.Log("Entering Moving State");
        owner.animator.SetBool("IsMoving", true);
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
