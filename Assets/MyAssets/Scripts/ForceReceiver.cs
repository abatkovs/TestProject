using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float drag = 0.3f;

    private Vector3 _dampingVelocity;
    private Vector3 _impact;
    [SerializeField] private float _verticalVelocity;
    [SerializeField] private bool isGrounded;

    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

    private void Update()
    {
        isGrounded = controller.isGrounded;
        if (_verticalVelocity < 0f && controller.isGrounded)
        {
            _verticalVelocity = 0;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, drag);

        if (agent != null)
        {
            if (_impact.sqrMagnitude < 0.2f * 0.2f)
            {
                _impact = Vector3.zero;
                agent.enabled = true;
            }
        }
    }

    public void AddForce(Vector3 force)
    {
        _impact += force;
        if (agent != null)
        {
            agent.enabled = false;
        }
    }
}
