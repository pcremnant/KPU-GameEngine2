using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingScene : GameScene
{
    [SerializeField]
    public SceneUI sceneUI;

    [SerializeField]
    public GameObject playingObjects;

    public PlayerInfo player;

    public override void CreateScene()
    {
        base.CreateScene();
        _myScene = Scene.Playing;
        sceneUI.gameObject.SetActive(false);
        playingObjects.SetActive(false);
    }

    public override void Initialize()
    {
        base.Initialize();
        sceneUI.gameObject.SetActive(true);
        playingObjects.SetActive(true);
        Time.timeScale = 1f;

        //스테이지 브금
        SoundMgr.instance.BGSoundPlay(1);
    }

    public override void DestroyScene()
    {
        base.DestroyScene();
        sceneUI.gameObject.SetActive(false);
        var enemies = FindObjectsOfType<EnemyAI>();
        // 게임 오브젝트들 다 지울 것
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        // 플레이어 정보들은 일단 그대로 체력만 채워주자
        player.hp = player.maxHp;
        playingObjects.SetActive(false);

        //메뉴 브금
        SoundMgr.instance.BGSoundPlay(0);
    }

    public override void Pause()
    {
        base.Pause();
        sceneUI.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public override void Resume()
    {
        base.Resume();
        sceneUI.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public override void SceneUpdate()
    {
        base.SceneUpdate();
    }

    public void DestroyEnemies()
    {
        var enemies = FindObjectsOfType<EnemyAI>();
        // 게임 오브젝트들 다 지울 것
        foreach (var enemy in enemies)
        {
            var enemyCol = enemy.GetComponent<EnemyCollide>();
            enemyCol.curHp = 0;
            enemyCol.SpawnerDestroyed();
           
            // Destroy(enemy.gameObject);
        }
    }
}
