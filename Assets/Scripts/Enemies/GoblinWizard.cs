using UnityEditorInternal;

public class GoblinWizard : Goblin
{
    public GoblinWizardType type;

    // Update is called once per frame
    void Update()
    {
        tween.StopTween();
    }

    public override void Attack()
    {
        base.Attack();

        base.stateMachine.ChangeState(new AimingState());
    }
}
