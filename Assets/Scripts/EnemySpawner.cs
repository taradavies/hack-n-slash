using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(1, 100)]
   [SerializeField] float _respawnRate = 10f;
   [SerializeField] float _initialSpawnDelay = 0.1f;
   [SerializeField] Enemy[] _enemyPrefabs;
   [SerializeField] Transform[] _spawnPoints;
   [SerializeField] int _totalNumberOfEnemiesToSpawn = 5;
   [SerializeField] int _numberOfEnemiesToSpawnAtOnce = 1;
   
   float _spawnTimer;

   void Update()
   {
    _spawnTimer += Time.deltaTime;
    if (ShouldSpawn())
    {
        SpawnEnemy();
    }
   }

    bool ShouldSpawn()
    {
        return _spawnTimer >= _respawnRate;
    }

    void SpawnEnemy()
    {
        _spawnTimer = 0;
        
        Enemy prefab = ChooseRandomEnemyFromArray();
        if (prefab != null)
        {
            Transform spawnPoint = ChooseRandomSpawnPointFromArray();
            var instantiatedEnemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    Transform ChooseRandomSpawnPointFromArray()
    {
        if (_spawnPoints.Length == 0) {return transform;}
        else if (_spawnPoints.Length == 1) {return _spawnPoints[0];}

        int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[randomIndex];
    }

    Enemy ChooseRandomEnemyFromArray()
    {
        if (_enemyPrefabs.Length == 0) {return null;}
        else if (_enemyPrefabs.Length == 1) {return _enemyPrefabs[0];}

        int randomIndex = UnityEngine.Random.Range(0, _enemyPrefabs.Length);
        return _enemyPrefabs[randomIndex];
    }
}
