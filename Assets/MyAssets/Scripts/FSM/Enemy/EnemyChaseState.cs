using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private const float ChaseSpeed = 5;

    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.currentStateStr = "ChaseState";
        StateMachine.Animator.SetFloat(StateMachine.AnimatorMotionSpeed, StateMachine.AnimatorDefaultSpeed);
        StateMachine.Animator.SetFloat(StateMachine.AnimatorSpeed, ChaseSpeed);
        StateMachine.Agent.isStopped = false;
    }

    public override void Tick(float deltaTime)
    {
        if (!IsInChaseRange())
        {
            StateMachine.SwitchState(new EnemyIdleState(StateMachine));
            return;
        }

        MoveToPlayer(deltaTime);
    }

    private void MoveToPlayer(float deltaTime)
    {
        StateMachine.Agent.destination = StateMachine.Player.transform.position;
        Move(StateMachine.Agent.desiredVelocity.normalized * StateMachine.MovementSpeed, deltaTime);
        StateMachine.Agent.velocity = StateMachine.Controller.velocity;
    }

    public override void Exit()
    {
        StateMachine.Agent.destination = StateMachine.transform.position;
        StateMachine.Agent.velocity = Vector3.zero;
        StateMachine.Agent.ResetPath();

    }
}
