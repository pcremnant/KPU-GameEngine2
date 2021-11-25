using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject[] enemyList;
    
    private Wave currentWave;
    [SerializeField] private Wave[] waves;
    private int currentWaveIndex = -1;

    private bool SpawnerOn;
    private int i = 0;

    // Start is called before the first frame update
    void Awake()
    {
        SpawnerOn = false;
        //StartWaves();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnerOn = MainGame.SpawnerOn;
        if (SpawnerOn)
        {
            StartWaves();
            // Debug.Log(++i + "번째");
            SpawnerOn = false;
        }
    }

    public void StartWaves()
    {
        if (currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;
            StartWave(waves[currentWaveIndex]);
        }
    }
    
    public void StartWave(Wave wave)
    {
        currentWave = wave;
        StartCoroutine("EnemySpawn");
    }

    private IEnumerator EnemySpawn()
    {
        int enemySpawnCount = 0;

        while (enemySpawnCount < currentWave.maxEnemyCount)
        {
            int index = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[index],
                new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1),
                Quaternion.identity);

            enemyList.Append(clone);

            enemySpawnCount++;

            yield return new WaitForSeconds(currentWave.spawnTime);
        }
        
        // StartWaves();
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}