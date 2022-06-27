using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    private float decayTime = 5f;
    
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.currentStateStr = "DeadState";
        StateMachine.Animator.SetBool(StateMachine.AnimatorDead, true);
        StateMachine.Controller.enabled = false;
        StateMachine.DestroyEnemy(decayTime);
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
        
    }
}
