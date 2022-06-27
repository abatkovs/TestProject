using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPf;
    [SerializeField] private GameObject spawnedEnemy;
    [SerializeField] private float respawnTime = 10;
    [SerializeField] private float respawnTimer = 10;
    private void Start()
    {
        respawnTimer = respawnTime;
        SpawnEnemy();
    }

    private void Update()
    {
        if (spawnedEnemy != null) return;
        respawnTimer -= Time.deltaTime;
        if (respawnTimer <= 0) SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        spawnedEnemy = Instantiate(enemyPf, transform.position, quaternion.identity);
        spawnedEnemy.GetComponent<EnemyStateMachine>().OnDeathEvent += EnemyDied;
        respawnTimer = respawnTime;
    }

    private void EnemyDied()
    {
        spawnedEnemy.GetComponent<EnemyStateMachine>().OnDeathEvent -= EnemyDied;
        spawnedEnemy = null;
    }
}
