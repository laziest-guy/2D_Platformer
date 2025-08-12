using UnityEngine;

public class JumpState : PlayerState
{
    private float airTime;
    private float minAirTime = 0.3f;

    public JumpState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter() // jumpForce만큼의 힘을 캐릭터에게 위로 가함
    {
        base.Enter();
        airTime = 0f;
        player.SetVelocityY(player.jumpForce);
        player.animator.Play("Jump");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        airTime += Time.deltaTime;

        // 좌우 이동 키 입력을 받아서 캐릭터의 방향을 뒤집습니다.
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            player.FlipCheck(moveInput);
        }

        if (airTime >= minAirTime)//0.3초 뒤 FallState로 전환
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void PhysicsUpdate() // 물리학적 업데이트에서 속도를 변경
    {
        base.PhysicsUpdate();

        // 점프 중에도 좌우 이동 입력을 받아 속도를 적용합니다.
        float moveInput = Input.GetAxisRaw("Horizontal");
        player.SetVelocityX(moveInput * player.moveSpeed);
    }
}