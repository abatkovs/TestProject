using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class EnemyStateMachine : StateMachine
{
    [Header("Info")]
    [SerializeField] public string currentStateStr;
    [SerializeField] public Vector3 vector;
    [SerializeField] private Vector3 spawnPoint;
    
    [Space(20)]
    [Header("Animation Vars")]
    public readonly int AnimatorSpeed = Animator.StringToHash("Speed");
    [field: SerializeField] public Animator Animator { get; private set; }
    public readonly int AnimatorBlend = Animator.StringToHash("Idle Walk Run Blend");
    public readonly int AnimatorMotionSpeed = Animator.StringToHash("MotionSpeed");
    public readonly int AnimatorAttacking = Animator.StringToHash("Attacking");
    public readonly int AnimatorDead = Animator.StringToHash("IsDead");
    public readonly float AnimatorDefaultSpeed = 1;
    
    [Space(20)] 
    public float gravity = -15.0f;
    [field: SerializeField] public GameObject Player { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    //[field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; } = 5;
    [field: SerializeField] public float playerDetectionRange = 5f;
    [field: SerializeField] public ForceReceiver forceReceiver;

    [Header("HP")] 
    [SerializeField] public float invTimer = 0.7f;
    public float timer;
    [SerializeField] public int maxHp = 10;
    [SerializeField] public int hp;
    [SerializeField] public HPBar hpBar;
    [SerializeField] private float forceMultiplier = 2;

    public event Action OnDeathEvent;
    
    private void Start()
    {
        spawnPoint = transform.position;
        timer = invTimer;
        hp = maxHp;
        Player = GameManager.Instance.player;
        //Agent.updatePosition = false;
        //Agent.updateRotation = false;
        SwitchState(new EnemyIdleState(this));
    }


    
    private void OnTriggerEnter(Collider other)
    {
        if (timer > 0) return;
        
        timer = invTimer; 
        TakeDamage();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }

    private void TakeDamage()
    {
        hp -= GameManager.Instance.gear.Sword.toolTier;
        hpBar.UpdateHp(maxHp, hp);
        Controller.Move(-transform.forward  * forceMultiplier);
    }

    public void DestroyEnemy(float decayTime)
    {
        //TODO: Add particles
        OnDeathEvent?.Invoke();
        Destroy(gameObject, decayTime);
    }
}
