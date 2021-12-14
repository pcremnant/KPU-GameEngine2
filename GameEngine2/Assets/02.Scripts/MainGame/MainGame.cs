using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    private PC.SceneManager sceneManager;

    [SerializeField]
    public List<GameScene> gameScenes;

    public int Money => _money;
    private int _money = 0;
    public TMP_Text moneyText;

    private int _maxStage = 0;
    public TMP_Text stageText;

    private int _gunLevel = 1;
    public TMP_Text gunText;



    // by oychan
    public static bool SpawnerOn = false;
    
    private bool SpendMoney(int cost)
    {
        if (cost > _money) return false;
        _money -= cost;
        moneyText.text = _money.ToString();
        return true;
    }

    private void EarnMoney(int cost)
    {
        _money += cost;
        moneyText.text = _money.ToString();
    }

    public void OnGameStartButton()
    {
        sceneManager.Push(GetScene(Scene.StageSelect));
    }

    public void OnQuitButton()
    {
// ���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnStageSelectButton()
    {
        SpawnerOn = true;
        sceneManager.Push(GetScene(Scene.Playing));
    }

    public void OnShopButton()
    {
        sceneManager.Push(GetScene(Scene.Shop));
    }

    public void OnResumeButton()
    {
        Scene scene = sceneManager.GetCurrentScene();
        while (scene != Scene.Playing)
        {
            if (sceneManager.Pop())
            {
                scene = sceneManager.GetCurrentScene();
                continue;
            }
            break;
        }
    }

    public void OnResultQuitButton()
    {
        Scene scene = sceneManager.GetCurrentScene();
        while (scene != Scene.StageSelect)
        {
            if (sceneManager.Pop())
            {
                scene = sceneManager.GetCurrentScene();
                continue;
            }
            break;
        }
    }

    public void OnStageSelectBackButton()
    {
        Scene scene = sceneManager.GetCurrentScene();
        while (scene != Scene.Main)
        {
            if (sceneManager.Pop())
            {
                scene = sceneManager.GetCurrentScene();
                continue;
            }
            break;
        }
    }

    public void OnPauseQuitButton()
    {
        Scene scene = sceneManager.GetCurrentScene();
        while (scene != Scene.StageSelect)
        {
            if (sceneManager.Pop())
            {
                scene = sceneManager.GetCurrentScene();
                continue;
            }
            break;
        }
    }

    public void OnShopBackButton()
    {
        Scene scene = sceneManager.GetCurrentScene();
        while (scene != Scene.Main)
        {
            if (sceneManager.Pop())
            {
                scene = sceneManager.GetCurrentScene();
                continue;
            }
            break;
        }
    }

    GameScene GetScene(Scene sceneIndex)
    {
        switch (sceneIndex)
        {
            case Scene.Main:
                return gameScenes[0];
            case Scene.StageSelect:
                return gameScenes[1];
            case Scene.Playing:
                //SpawnerOn = true;
                return gameScenes[2];
            case Scene.Pause:
                return gameScenes[3];
            case Scene.Shop:
                return gameScenes[4];
            case Scene.Result:
                return gameScenes[5];
            default:
                return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var scene in gameScenes)
        {
            scene.CreateScene();
        }
        moneyText.text = _money.ToString();
        sceneManager = new PC.SceneManager();
        sceneManager.Initialize(GetScene(Scene.Main));
    }

    void Update()
    {
        var currScene = sceneManager.GetCurrentScene();
        if (currScene == Scene.Main)
        {
            stageText.text = "Max Stage : " + _maxStage.ToString();
            gunText.text = "Gun Level : " + _gunLevel.ToString();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var currentScene = sceneManager.GetCurrentScene();
            if (currentScene == Scene.Playing)
            {
                sceneManager.Push(GetScene(Scene.Pause));
            }
            else if (currentScene == Scene.Pause)
            {
                sceneManager.Pop();
            }
            else if (currentScene == Scene.StageSelect)
            {
                sceneManager.Pop();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            var currentScene = sceneManager.GetCurrentScene();
            if (currentScene == Scene.Shop)
            {
                EarnMoney(100);
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            var currentScene = sceneManager.GetCurrentScene();
            if (currentScene == Scene.Shop)
            {
                SpendMoney(100);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            var currentScene = sceneManager.GetCurrentScene();
            if (currentScene == Scene.Playing)
            {
                sceneManager.Push(GetScene(Scene.Result));
            }
        }

        sceneManager.SceneUpdate();
    }
}
