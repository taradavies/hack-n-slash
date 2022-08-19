using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(1, 50)]
   [SerializeField] float _respawnRate = 10f;
   [SerializeField] float _initialSpawnDelay = 0.1f;
   [SerializeField] Enemy[] _enemyPrefabs;
   [SerializeField] Transform[] _spawnPoints;
   [SerializeField] int _totalNumberOfEnemiesToSpawn = 5;
   [SerializeField] int _numberOfEnemiesToSpawnAtOnce = 1;
   
   float _spawnTimer = 0f;
   int _enemiesSpawned = 0;

   void OnEnable()
   {
        _spawnTimer = _respawnRate - _initialSpawnDelay;
   }

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
        return _spawnTimer >= _respawnRate && _enemiesSpawned < _totalNumberOfEnemiesToSpawn;
    }

    void SpawnEnemy()
    {
        _spawnTimer = 0;

        var availableSpawnPoints = _spawnPoints.ToList();

        for (int i = 0; i < _numberOfEnemiesToSpawnAtOnce; i++)
        {
            if (MaxEnemiesSpawned()) { break; }

            Enemy prefab = ChooseRandomEnemyFromArray();
            if (prefab != null)
            {
                Transform spawnPoint = ChooseRandomSpawnPointFromArray(availableSpawnPoints);

                if (availableSpawnPoints.Contains(spawnPoint))
                    availableSpawnPoints.Remove(spawnPoint);

                // var instantiatedEnemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
                var pooledEnemy = prefab.Get<Enemy>(spawnPoint.position, spawnPoint.rotation);
                _enemiesSpawned++;
            }
        }
    }

    bool MaxEnemiesSpawned()
    {
        return _enemiesSpawned >= _totalNumberOfEnemiesToSpawn;
    }

    Transform ChooseRandomSpawnPointFromArray(List<Transform> availableSpawnPoints)
    {
        if (availableSpawnPoints.Count == 0) {return transform;}
        else if (availableSpawnPoints.Count == 1) {return availableSpawnPoints[0];}

        int randomIndex = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
        return availableSpawnPoints[randomIndex];
    }

    Enemy ChooseRandomEnemyFromArray()
    {
        if (_enemyPrefabs.Length == 0) {return null;}
        else if (_enemyPrefabs.Length == 1) {return _enemyPrefabs[0];}

        int randomIndex = UnityEngine.Random.Range(0, _enemyPrefabs.Length);
        return _enemyPrefabs[randomIndex];
    }

// only compiles in the editor 

#if UNITY_EDITOR

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, Vector3.one);

        foreach (var spawnPoint in _spawnPoints)
        {
            Gizmos.DrawSphere(spawnPoint.position, 0.3f);
        }
    }

#endif
}
