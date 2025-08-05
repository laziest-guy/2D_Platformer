using UnityEngine;

public class FallState : PlayerState
{
    public FallState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.animator.Play("Fall");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.IsGrounded())//땅에 닿으면 idleState 전환
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}