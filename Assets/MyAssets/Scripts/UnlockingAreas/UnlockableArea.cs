using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnlockableArea : MonoBehaviour
{
    [Header("Grow")]
    [SerializeField] private Vector3 targetScale = new Vector3();
    [SerializeField] private float targetScaleTime = 0.15f;
    [SerializeField] private Collider islandCollider;
    
    [Header("Shake")]
    [SerializeField] private Vector3 shakeScale = new Vector3(-0.3f, 0.3f, -0.3f);
    [SerializeField] private float shakeTime = 0.15f;

    [SerializeField] private bool isUnlocked;
    
    [Header("Resources")]
    [SerializeField] private List<GameObject> islandResources;
    [SerializeField] private Vector3 resourceSize = Vector3.one;
    [SerializeField] private List<Unlocker> unlockers;

    private void Start()
    {
        if (isUnlocked) return;
        islandCollider.enabled = false;
    }

    public void UnlockArea()
    {
        if (isUnlocked) return;
        isUnlocked = true;
        var tween = transform.DOScale(targetScale, targetScaleTime);
        tween.OnComplete(() => ShakeAnimation(transform));
    }

    private void UnlockResource()
    {
        
    }

    public void ShakeAnimation(Transform objTransform)
    {
        var tween = objTransform.DOPunchScale(shakeScale, shakeTime);
        tween.OnComplete(SpawnIslandObjects);
    }

    private void ShakeResources(Transform resourceTransform)
    {
        resourceTransform.DOPunchScale(shakeScale, shakeTime);
    }

    private void SpawnIslandObjects()
    {
        islandCollider.enabled = true;
        foreach (var resource in islandResources)
        {
            SpawnResource(resource);
        }

        foreach (var unlocker in unlockers)
        {
            EnableUnlocker(unlocker);
        }
    }

    private void SpawnResource(GameObject resource)
    {
        var tween = resource.transform.DOScale(resourceSize, targetScaleTime);
        tween.OnComplete(() => ShakeResources(resource.transform));
    }

    private void EnableUnlocker(Unlocker unlocker)
    {
        unlocker.transform.gameObject.SetActive(true);
        // var tween = unlocker.transform.DOScale(resourceSize, targetScaleTime);
        // tween.OnComplete(() => ShakeResources(unlocker.transform));
    }
}
