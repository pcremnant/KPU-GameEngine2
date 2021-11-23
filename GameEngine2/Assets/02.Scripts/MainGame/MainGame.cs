using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public GameState gameState => _gameState;
    private GameState _gameState;

    private SceneManager sceneManager;

    [SerializeField]
    public List<GameScene> gameScenes;

    public int Money => _money;
    private int _money = 0;
    [SerializeField]
    public TMP_Text moneyText;

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
// 게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnStageSelectButton()
    {
        sceneManager.Push(GetScene(Scene.Playing));
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

    GameScene GetScene(Scene sceneIndex)
    {
        switch (sceneIndex)
        {
            case Scene.Main:
                return gameScenes[0];
            case Scene.StageSelect:
                return gameScenes[1];
            case Scene.Playing:
                return gameScenes[2];
            case Scene.Pause:
                return gameScenes[3];
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
        sceneManager = new SceneManager();
        sceneManager.Initialize(GetScene(Scene.Main));
    }

    void Update()
    {
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

        // 임시 테스트용 코드
        if (Input.GetKeyDown(KeyCode.I))
        {
            var currentScene = sceneManager.GetCurrentScene();
            if (currentScene == Scene.StageSelect)
            {
                EarnMoney(100);
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            var currentScene = sceneManager.GetCurrentScene();
            if (currentScene == Scene.StageSelect)
            {
                SpendMoney(100);
            }
        }
        sceneManager.SceneUpdate();
    }
}
