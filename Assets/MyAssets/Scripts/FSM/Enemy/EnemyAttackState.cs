using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float attackDistance = 2.8f;
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.currentStateStr = "AttackState";
        StateMachine.Animator.SetBool(StateMachine.AnimatorAttacking, true);
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        if(GetDistance() > attackDistance) StateMachine.SwitchState(new EnemyChaseState(StateMachine));
    }

    public override void Exit()
    {
        StateMachine.Animator.SetBool(StateMachine.AnimatorAttacking, false);
    }
}
