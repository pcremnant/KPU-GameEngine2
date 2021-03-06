using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour, IDamageable
{
    public int maxHp;
    public int curHp;
    public Image HpBar;
    
    public GameObject Enemy;
    public GameObject[] enemyList;
    
    private Wave currentWave;
    [SerializeField] private Wave[] waves;
    private int currentWaveIndex = -1;

    private bool SpawnerOn;
    private int i = 0;
    
    public Transform destination;
    public GamePlayingScene gamePlayingScene;

    public Transform transformHQ;

    public void Init()
    {
        currentWaveIndex = -1;
        SpawnerOn = true;
        HpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        curHp = maxHp;
        PlayerInfo.destroySpanwerCnt = 0;
        gameObject.SetActive(true);
    }    // Start is called before the first frame update
    void Awake()
    {
        SpawnerOn = false;
        //StartWaves();
        
        HpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        // SpawnerOn = MainGame.SpawnerOn;
        if (SpawnerOn)
        {
            StartWaves();
            // Debug.Log(++i + "번째");
            SpawnerOn = false;
        }
        
        HpBar.rectTransform.localScale =
            new Vector3((float) curHp / (float) maxHp > 0 ? (float) curHp / (float) maxHp : 0, 1f, 1f);
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
            
            clone.GetComponent<EnemyAI>().destination = transformHQ;

            enemyList.Append(clone);

            enemySpawnCount++;

            yield return new WaitForSeconds(currentWave.spawnTime);
        }
        
        // StartWaves();
    }
    
    public bool ApplyDamage(DamageMessage damageMessage)
    {
        curHp -= (int)damageMessage.amount;

        if (curHp <= 0)
        {
            //gamePlayingScene.DestroyEnemies();
            gameObject.SetActive(false);
            PlayerInfo.destroySpanwerCnt += 1;
        }
        
        return false;
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}