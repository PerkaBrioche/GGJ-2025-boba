using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesPrefab;
    [SerializeField] Transform player;
    [SerializeField] float timeBetweenSpawns = 2f;
    [SerializeField] float percentageDescreaseByKill = 0.05f;
    float lastSpawnTime;

    public static event Action EnemyDied;

    private void Start()
    {
        EnemyDied += OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        timeBetweenSpawns -= timeBetweenSpawns * percentageDescreaseByKill;
        SpawnRandomEnemy();
    }

    void SpawnRandomEnemy()
    {
        lastSpawnTime = Time.time;
        Instantiate(enemiesPrefab[UnityEngine.Random.Range(0, enemiesPrefab.Count)],player.position, Quaternion.identity);
    }

    private void Update()
    {
        if(Time.time >= lastSpawnTime + timeBetweenSpawns)
        {
            SpawnRandomEnemy();
        }
    }
}
