using UnityEngine;

public class AttackState : PlayerState
{
    private bool attackFinished;

    public AttackState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        attackFinished = false;
        player.animator.Play("Attack");
    }

    public override void LogicUpdate()
    {
        if (attackFinished)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public void OnAttackAnimationEnd()
    {
        attackFinished = true;
    }

    public void PerformAttack()
    {
        player.PerformAttack();
    }
}
