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

        //�������� ���
        SoundMgr.instance.BGSoundPlay(1);
    }

    public override void DestroyScene()
    {
        base.DestroyScene();
        sceneUI.gameObject.SetActive(false);
        var enemies = FindObjectsOfType<EnemyAI>();
        // ���� ������Ʈ�� �� ���� ��
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        // �÷��̾� �������� �ϴ� �״�� ü�¸� ä������
        player.hp = player.maxHp;
        playingObjects.SetActive(false);

        //�޴� ���
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
        // ���� ������Ʈ�� �� ���� ��
        foreach (var enemy in enemies)
        {
            var enemyCol = enemy.GetComponent<EnemyCollide>();
            enemyCol.curHp = 0;
            enemyCol.SpawnerDestroyed();
           
            // Destroy(enemy.gameObject);
        }
    }
}
