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
        if (airTime >= minAirTime)//0.3초 뒤 FallState로 전환
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
