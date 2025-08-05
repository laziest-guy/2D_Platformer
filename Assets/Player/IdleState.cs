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
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f) // 좌우 이동 키
            stateMachine.ChangeState(player.runState);
        else if (Input.GetButtonDown("Jump")) // 점프 키
            stateMachine.ChangeState(player.jumpState);
        else if (!player.IsGrounded()) // 공중에 뜸
            stateMachine.ChangeState(player.fallState);
        else if (Input.GetKeyDown("z")) // 공격 키
            stateMachine.ChangeState(player.attackState);
    }       
}
