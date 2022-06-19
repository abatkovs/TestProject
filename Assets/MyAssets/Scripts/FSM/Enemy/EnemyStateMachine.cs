using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [SerializeField] public string currentStateStr;
    
    [Space(20)]
    [Header("Animation Vars")]
    public readonly int AnimatorSpeed = Animator.StringToHash("Speed");
    [field: SerializeField] public Animator Animator { get; private set; }
    public readonly int AnimatorBlend = Animator.StringToHash("Idle Walk Run Blend");
    public readonly int AnimatorMotionSpeed = UnityEngine.Animator.StringToHash("MotionSpeed");

    [Space(20)] 
    public CharacterController controller;
    public float gravity = -15.0f;
    [field: SerializeField] public float playerDetectionRange = 5f;
    [field: SerializeField] public ForceReceiver forceReceiver;
    public GameObject player;

    private void Start()
    {
        player = GameManager.Instance.player;
        SwitchState(new EnemyIdleState(this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }
}
