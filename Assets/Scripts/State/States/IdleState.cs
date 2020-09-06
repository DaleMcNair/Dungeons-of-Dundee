using UnityEngine;

public class IdleState : IState
{
    Enemy owner;

    public IdleState(Enemy owner) { this.owner = owner; }

    public void Enter()
    {
        Debug.Log("Entering Idle State");
        owner.animator.SetBool("IsMoving", false);
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
