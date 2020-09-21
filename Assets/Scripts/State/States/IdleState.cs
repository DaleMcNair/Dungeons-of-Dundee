using UnityEngine;

public class IdleState : IState
{
    Enemy enemy;
    float idleTimer;

    public IdleState(Enemy enemy) { this.enemy = enemy; }

    public void Enter()
    {
        if (enemy.animator != null) enemy.animator.SetTrigger("Idle");
        if (enemy.animator) enemy.animator.SetBool("IsMoving", false);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
