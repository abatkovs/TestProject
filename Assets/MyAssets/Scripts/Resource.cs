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
    [SerializeField] private int maxHealth = 10;
    private int _health = 10;
    public int GetHealth() => _health;
    [SerializeField] private float resetTime = 60f;

    [SerializeField] private GameObject resourceGrown;
    [SerializeField] private GameObject resourceDamaged;
    [SerializeField] private GameObject resourceDepleted;

    [Header("Popup")] 
    [SerializeField] private PopUpInfo popUpPf;
    [SerializeField] private Texture2D popUpTexture; //PH replace with image


    [Header("Animation parameters")] 
    [SerializeField] private GameObject animationTarget;
    [SerializeField] private float shakeDuration = 0.25f;
    [SerializeField] private Vector3 shakeStrength = new Vector3(0.1f, 0.3f, 0.1f);

    [Header("Interaction")] 
    [SerializeField] private float interactionTimerInterval = 0.7f;
    [SerializeField] private float interactionTimer;
    
    [SerializeField] private Transform spawnPoint;

    private int _playerToolTier;
    
    private void Start()
    {
        _health = maxHealth;
        interactionTimer = interactionTimerInterval;
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if(interactionTimer > 0) interactionTimer -= Time.deltaTime;
    }

    public override void Interact()
    {
        //Debug.Break();
        if (interactionTimer > 0 || _health <= 0) return;
        
        
        _playerToolTier = GetPlayerToolTier();
        if (_playerToolTier < tier) return;
        interactionTimer = interactionTimerInterval;
        
        Animate();
        SpawnPopUp();
        
        for (int i = 0; i < _playerToolTier; i++)
        {
            SpawnResource();
        }
    }
    
    /// <summary>
    /// Get required resource tool tier
    /// </summary>
    /// <returns></returns>
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
        animationTarget.transform.DORewind();
        animationTarget.transform.DOShakeScale(shakeDuration, shakeStrength);
    }

    public void TakeDamage()
    {
        
    }
    
    private void SpawnResource()
    {
        if (_health <= 0) return;
        if (_health == maxHealth) StartCoroutine($"ResetHealth");
        _health--;
        UpdateVisuals();
        _gameManager.inventory.AddItem(item);
        var resource = Instantiate(resourcePrefab, spawnPoint.position, Quaternion.identity);
        //TODO: Add pooling
    }

    private void UpdateVisuals()
    {
        HideVisuals();
        if (_health <= 0)
        {
            resourceDepleted.SetActive(true);
            return;
        }
        if (_health > maxHealth / 2) resourceGrown.SetActive(true);
        if(_health <= maxHealth / 2) resourceDamaged.SetActive(true);
        
    }

    private void HideVisuals()
    {
        resourceGrown.SetActive(false);
        resourceDamaged.SetActive(false);
        resourceDepleted.SetActive(false);
    }

    private IEnumerator ResetHealth()
    {
        Debug.Log("Start reseting resource health");
        yield return new WaitForSeconds(resetTime);
        _health = maxHealth;
        UpdateVisuals();
    }

    private void SpawnPopUp()
    {
        PopUpInfo popUp = Instantiate(popUpPf, spawnPoint.position, quaternion.identity);
        popUp.displayInfo = $"+{Mathf.Min(_playerToolTier, _health)} ";
        popUp.popUpTexture = popUpTexture;
    }

    

}
