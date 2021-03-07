using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SpawnModes
{
    Fixed,
    Random

}

public class Spawner : MonoBehaviour
{
    public static Action OnWaveCompleted;



    [Header("Settings")]
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWaves = 1f;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    [Header("Poolers")]
    [SerializeField] private ObjectPooler enemyWave10Pooler;
    [SerializeField] private ObjectPooler enemyWave11To20Pooler;

    private float spawnTimer;
    private int enemiesSpawned;
    private int enemiesRemaining;


    private Waypoint waypoint;


    private void Start()
    {

        waypoint = GetComponent<Waypoint>();

        enemiesRemaining = enemyCount;
    }



    // Update is called once per frame
    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            spawnTimer = GetSpawnDelay();
            if (enemiesSpawned < enemyCount)
            {
                enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }


    private ObjectPooler GetPooler()
    {
        int currentWave = LevelManager.Instance.CurrentWave;
        if (currentWave <= 10)
        {
            return enemyWave10Pooler;
        }

        if (currentWave > 10 && currentWave <=20)
        {
            return enemyWave11To20Pooler;
        }

        return null;
    }
    private void SpawnEnemy()
    {
        GameObject newInstance = GetPooler().GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.Waypoint = waypoint;
        enemy.ResetEnemy();

        enemy.transform.localPosition = transform.position;
        newInstance.SetActive(true);
    }

    private float GetSpawnDelay()
    {
        float delay = 0f;
        if (spawnMode == SpawnModes.Fixed)
        {
            delay = delayBtwSpawns;
        }
        else
        {
            delay = GetRandomDelay();
        }

        return delay;
    }

    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        enemiesRemaining = enemyCount;
        spawnTimer = 0f;
        enemiesSpawned = 0;
    }


    private void RecordEnemy(Enemy enemy)
    {
        enemiesRemaining--;
        if (enemiesRemaining <= 0)
        {
            OnWaveCompleted?.Invoke();
            StartCoroutine(NextWave());
        }
    }

    private void OnEnable() 
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;
    }


}
