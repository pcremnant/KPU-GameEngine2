using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int stageNumber => _stageNumber;
    [SerializeField] private int _stageNumber;

    public static bool SpawnerOn = false;

    public List<Spawner> enemySpawner;
    public GameObject playerSpawnTransform;
    public GameObject player;

    public void OnStage()
    {
        gameObject.SetActive(true);
        SpawnerOn = true;
        player.transform.position = playerSpawnTransform.transform.position;
        if (enemySpawner.Count != 0)
        {
            foreach (var es in enemySpawner)
            {
                es.Init();
            }
        }
    }

    public void OffStage()
    {
        gameObject.SetActive(false);
        SpawnerOn = false;
    }

    public bool IsOnStage()
    {
        return gameObject.activeSelf;
    }
}
