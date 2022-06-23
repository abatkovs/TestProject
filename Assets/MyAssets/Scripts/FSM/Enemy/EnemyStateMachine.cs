using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [SerializeField] public string currentStateStr;
    
    [Space(20)]
    [Header("Animation Vars")]
    public readonly int AnimatorSpeed = Animator.StringToHash("Speed");
    [field: SerializeField] public Animator Animator { get; private set; }
    public readonly int AnimatorBlend = Animator.StringToHash("Idle Walk Run Blend");
    public readonly int AnimatorMotionSpeed = UnityEngine.Animator.StringToHash("MotionSpeed");
    public readonly float AnimatorDefaultSpeed = 1;
    
    [Space(20)] 
    public float gravity = -15.0f;
    [field: SerializeField] public GameObject Player { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; } = 5;
    [field: SerializeField] public float playerDetectionRange = 5f;
    [field: SerializeField] public ForceReceiver forceReceiver;
    

    private void Start()
    {
        Player = GameManager.Instance.player;
        Agent.updatePosition = false;
        //Agent.updateRotation = false;
        SwitchState(new EnemyIdleState(this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }
}
