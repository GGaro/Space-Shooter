using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemySpawner : MonoBehaviour {
	[SerializeField]GameObject enemyPrefab;
	[SerializeField]float spawnTimer = 5f;
	[SerializeField] int maxEnemies = 5;
    private int currEnemies = 0;


    void OnEnable()
	{
		EventManager.onStartGame += StartSpawning;
        EventManager.onPlayerDeath += StopSpawning;
    }

	void OnDisable()
	{
		StopSpawning();
		EventManager.onStartGame -= StartSpawning;
		EventManager.onPlayerDeath -= StopSpawning;
	}


	void SpawnEnemy()
	{
		Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        currEnemies++;
    }

	void StartSpawning()
	{
        if (currEnemies != maxEnemies)
        {
            InvokeRepeating("SpawnEnemy", spawnTimer, spawnTimer);
        }
    }

	void StopSpawning()
	{
		CancelInvoke();
	}
}
