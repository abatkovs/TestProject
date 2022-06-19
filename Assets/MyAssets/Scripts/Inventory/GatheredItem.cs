using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class GatheredItem : MonoBehaviour
{
    //TODO: Add pooling
    //TODO: Try bezier curves
    [Header("Item jump parameters")] [SerializeField]
    private Vector3 offset = new Vector3(0, 1, 0);
    [SerializeField] private float jumpPower = 8f;
    [SerializeField] private int numJumps = 1;
    [SerializeField] private float jumpDuration = 2f;
    [SerializeField] private Ease easeType;
    private Transform jumpToTarget;
    [SerializeField] private Sequence jumpSequence;
    [SerializeField] private float followSpeed = 50;

    private void Start()
    {
        jumpToTarget = GameManager.Instance.gatherTargetPoint;
        AnimateResources();
    }

    private void AnimateResources()
    {
        jumpSequence = transform.DOJump(transform.position + RandomisePosition(), jumpPower, numJumps, jumpDuration).SetEase(easeType);
        
        jumpSequence.SetAutoKill(true);
        jumpSequence.OnComplete(MoveToPlayer);
    }

    private void MoveToPlayer()
    {
        //transform.DOMove(jumpToTarget.position, 0.25f).ChangeEndValue(jumpToTarget.position, false);
        var moveToPlayer = transform.DOMove(jumpToTarget.position, followSpeed).SetSpeedBased(true).SetEase(easeType);
        moveToPlayer.OnUpdate(delegate() { MoveResource(moveToPlayer); });
    }

    private Vector3 RandomisePosition()
    {
        const float randomRange = 0.3f;
        offset += Random.insideUnitSphere * randomRange;
        return offset;
    }

    private void MoveResource(Tweener tween)
    {
        //var targetPos = GameManager.Instance.gatherTargetPoint.position;
        if (Vector3.Distance(transform.position, jumpToTarget.position) > 0.5f)
        {
            //Debug.Log($"Updating tween {jumpToTarget.position} {transform.position}");
            tween.ChangeEndValue(jumpToTarget.position, true);
        }
        else
        {
            tween.Complete();
            Destroy(gameObject);
        }
    }

    private void DestroyItem()
    {
        Destroy(gameObject, 0.1f);
    }
}
