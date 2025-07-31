using UnityEngine;
public class IdleState : PlayerState
{
    public IdleState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        player.SetVelocityX(0);
        player.animator.Play("Idle");
    }

    public override void LogicUpdate()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f)
            stateMachine.ChangeState(player.runState);
        else if (Input.GetButtonDown("Jump"))
            stateMachine.ChangeState(player.jumpState);
        else if (!player.IsGrounded())
            stateMachine.ChangeState(player.fallState);
    }
}
