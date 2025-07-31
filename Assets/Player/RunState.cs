using UnityEngine;
public class RunState : PlayerState
{
    public RunState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        player.animator.Play("Run");
    }

    public override void LogicUpdate()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(move) < 0.1f)
            stateMachine.ChangeState(player.idleState);
        else if (Input.GetButtonDown("Jump"))
            stateMachine.ChangeState(player.jumpState);
        else if (!player.IsGrounded())
            stateMachine.ChangeState(player.fallState);

        player.SetVelocityX(move * player.moveSpeed);
        player.FlipCheck(move);
    }
}