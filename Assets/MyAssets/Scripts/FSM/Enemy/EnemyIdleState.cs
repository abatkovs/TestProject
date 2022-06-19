using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private const float IdleSpeed = 0;
    private const float AnimatorDampTime = 0.1f;
    private const float AnimatorDefaultSpeed = 1;
    
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.currentStateStr = "IdleState";
        StateMachine.Animator.SetFloat(StateMachine.AnimatorMotionSpeed, AnimatorDefaultSpeed);
        StateMachine.Animator.SetFloat(StateMachine.AnimatorSpeed, IdleSpeed);
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        if (IsInChaseRange())
        {
            Debug.Log("Chase player");
            return;
        }
    }

    public override void Exit()
    {
    }
}
