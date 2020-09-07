using UnityEngine;

public class IdleState : IState
{
    Enemy enemy;

    public IdleState(Enemy enemy) { this.enemy = enemy; }

    public void Enter()
    {
        Debug.Log("Entering Idle State");
        enemy.animator.SetBool("IsMoving", false);
    }

    public void Execute()
    {
        Debug.Log("Executing Idle State");
    }

    public void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}
