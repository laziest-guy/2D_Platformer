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
        
        // 좌우 이동 키 입력을 받아서 캐릭터의 방향을 뒤집습니다.
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            player.FlipCheck(moveInput);
        }

        if (player.IsGrounded())//땅에 닿으면 idleState 전환
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void PhysicsUpdate() // 물리학적 업데이트에서 속도를 변경
    {
        base.PhysicsUpdate();
        
        // 떨어지는 중에도 좌우 이동 입력을 받아 속도를 적용합니다.
        float moveInput = Input.GetAxisRaw("Horizontal");
        player.SetVelocityX(moveInput * player.moveSpeed);
    }
}