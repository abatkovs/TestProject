using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class Resource : Interactable
{
    private GameManager _gameManager;
    
    [Header("Resource")]
    [SerializeField] private ResourceType type;
    [SerializeField] private Item item;
    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private int tier = 1;
    [SerializeField] private int health = 10;

    [Header("Popup")] 
    [SerializeField] private PopUpInfo popUpPf;
    [SerializeField] private string popUpAffix; //PH replace with image
    

    [Header("Shake parameters")] 
    [SerializeField] private float shakeDuration = 0.25f;
    [SerializeField] private Vector3 shakeStrength = new Vector3(0.1f, 0.3f, 0.1f);

    [Header("Interaction")] 
    [SerializeField] private float interactionTimerInterval = 0.7f;
    [SerializeField] private float interactionTimer;
    
    [SerializeField] private Transform spawnPoint;

    private int _playerToolTier;
    
    private void Start()
    {
        interactionTimer = interactionTimerInterval;
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if(interactionTimer > 0) interactionTimer -= Time.deltaTime;
    }

    public override void Interact()
    {
        if (interactionTimer > 0) return;
        interactionTimer = interactionTimerInterval; 
        _playerToolTier = GetPlayerToolTier();
        if (_playerToolTier < tier) return;
        Animate();
        SpawnPopUp();
        for (int i = 0; i < _playerToolTier; i++)
        {
            SpawnResource();
        }
    }

    private int GetPlayerToolTier()
    {
        switch (type)
        {
            case ResourceType.Stone:
                return _gameManager.gear.Pickaxe.toolTier;
            case ResourceType.Wood:
                return _gameManager.gear.Axe.toolTier;
            default:
                return _gameManager.gear.Sword.toolTier;
        }
    }
    
    /// <summary>
    /// Animate resource when hit
    /// </summary>
    private void Animate()
    {
        transform.DORewind();
        transform.DOShakeScale(shakeDuration, shakeStrength);
    }

    public void TakeDamage()
    {
        
    }
    
    private GameObject SpawnResource()
    {
        _gameManager.inventory.AddItem(item);
        var resource = Instantiate(resourcePrefab, spawnPoint.position, Quaternion.identity);
        return resource;
    }

    private void SpawnPopUp()
    {
        PopUpInfo popUp = Instantiate(popUpPf, spawnPoint.position, quaternion.identity);
        popUp.displayInfo = $"+{_playerToolTier} {popUpAffix}";
    }
}
