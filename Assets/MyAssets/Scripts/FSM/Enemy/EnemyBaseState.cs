using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine StateMachine;
    private float _verticalVelocity;
    private float _terminalVelocity = 53f;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }
    
    public override void Tick(float deltaTime)
    {
        StateMachine.Controller.SimpleMove(Vector3.zero);
        //if (StateMachine.controller.isGrounded) return;
        // StateMachine.controller.Move(Vector3.down * _verticalVelocity);
        // if (_verticalVelocity > _terminalVelocity) return;
        // _verticalVelocity += StateMachine.Gravity * Time.deltaTime;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        StateMachine.Controller.Move((motion + StateMachine.forceReceiver.Movement) * deltaTime);
    }

    protected bool IsInChaseRange()
    {
        var playerDistanceSqr = (StateMachine.Player.transform.position - StateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= StateMachine.playerDetectionRange * StateMachine.playerDetectionRange;
    }
    
}
