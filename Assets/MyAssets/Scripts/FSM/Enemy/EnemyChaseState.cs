using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    //TODO: Fix falling
    private const float RotationSpeed = 5f;
    private const float ChaseSpeed = 3;
    private float stoppingDistance = 2.3f;

    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.currentStateStr = "ChaseState";
        StateMachine.Animator.SetFloat(StateMachine.AnimatorMotionSpeed, StateMachine.AnimatorDefaultSpeed);
        StateMachine.Animator.SetFloat(StateMachine.AnimatorSpeed, ChaseSpeed);
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log($"{GetDistance()} : {StateMachine.Agent.stoppingDistance}");
        base.Tick(deltaTime);
        if (GetDistance() <= stoppingDistance)
        {
            StateMachine.SwitchState(new EnemyAttackState(StateMachine));
            return;
        }
        
        if (!IsInChaseRange())
        {
            StateMachine.SwitchState(new EnemyIdleState(StateMachine));
            return;
        }

        MoveToPlayer(deltaTime);
    }

    // private void MoveToPlayer(float deltaTime)
    // {
    //     StateMachine.Agent.destination = StateMachine.Player.transform.position;
    //     Move(StateMachine.Agent.desiredVelocity.normalized * StateMachine.MovementSpeed, deltaTime);
    //     StateMachine.Agent.velocity = StateMachine.Controller.velocity;
    // }

    private void MoveToPlayer(float deltaTime)
    {
        var movementVector = StateMachine.Player.transform.position - StateMachine.transform.position;
        var targetRotation = Quaternion.LookRotation(movementVector);
        targetRotation.x = 0;
        targetRotation.z = 0;
        StateMachine.vector = Vector3.ClampMagnitude(movementVector, 1);
        //StateMachine.transform.LookAt(StateMachine.Player.transform, Vector3.up);
        
        StateMachine.transform.rotation = Quaternion.Slerp(StateMachine.transform.rotation, targetRotation, RotationSpeed * deltaTime); 
        StateMachine.Controller.Move(Vector3.ClampMagnitude(movementVector, 1) * deltaTime * StateMachine.MovementSpeed);
    }

    public override void Exit()
    {
        StateMachine.Animator.SetFloat(StateMachine.AnimatorSpeed, 0);
    }
}
