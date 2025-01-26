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
        lastSpawnTime = Time.time;
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
        GameObject randomPrefab = enemiesPrefab[UnityEngine.Random.Range(0, enemiesPrefab.Count)];
        Instantiate(randomPrefab, new Vector3(player.position.x, randomPrefab.transform.localPosition.y, player.position.x) , Quaternion.identity);
    }

    private void Update()
    {
        if(Time.time >= lastSpawnTime + timeBetweenSpawns)
        {
            SpawnRandomEnemy();
        }
    }
}
