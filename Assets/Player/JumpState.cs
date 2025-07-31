using UnityEngine;
public class JumpState : PlayerState
{
    private float airTime;
    private float minAirTime = 0.3f;

    public JumpState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        airTime = 0f;
        player.SetVelocityY(player.jumpForce);
        player.animator.Play("Jump");
        //Debug.Log("JumpState: Enter");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        airTime += Time.deltaTime;

        if (airTime >= minAirTime)
        {
            //Debug.Log("JumpState: Landed");
            stateMachine.ChangeState(player.fallState);
        }
    }
}
