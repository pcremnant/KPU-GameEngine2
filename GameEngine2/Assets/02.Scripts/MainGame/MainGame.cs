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

    public int MaxStage => _maxStage;
    private int _maxStage = 0;
    public TMP_Text stageText;

    public int GunLevel => _gunLevel;
    private int _gunLevel = 1;
    public TMP_Text gunText;

    public int AdditionalHp => _additionalHp;
    private int _additionalHp = 0;
    public TMP_Text hpText;

    PlayerInfo playerInfo;
    public TMP_Text grenadeText;


    public void LoadInfo(int money, int stage, int gunLevel, int additionalHp)
    {
        _money = money;
        _maxStage = stage;
        _gunLevel = gunLevel;
        _additionalHp = additionalHp;
    }

    // public ItemData selected;

    // by oychan
    public static bool SpawnerOn = false;

    public void ClearStage(int stageIndex)
    {
        if (_maxStage < stageIndex)
            _maxStage = stageIndex;
    }

    public bool SpendMoney(int cost)
    {
        if (cost > _money) return false;
        _money -= cost;
        moneyText.text = _money.ToString();
        return true;
    }

    public void EarnMoney(int cost)
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

    public void ApplyItem(Item item)
    {
        switch(item.ItemData.itemIndex)
        {
            case 0:
                _additionalHp += 10;
                break;
            case 1:
                playerInfo.grenade++;
                break;
            case 2:
                _gunLevel++;
                break;
            default:
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
            hpText.text = "Additional Hp : " + _additionalHp.ToString();
            grenadeText.text = "Grenade : " + playerInfo.grenade.ToString();
        }
        
        if (currScene == Scene.Playing) // if (game end)
        {
            // 임시 코드 -> 게임 상태 확인해서 플레이어 승리 or 패배 세팅
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ResultScene rs = (ResultScene)GetScene(Scene.Result);
                rs.SetPlayerWin(true);
                sceneManager.Push((GameScene)rs);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ResultScene rs = (ResultScene)GetScene(Scene.Result);
                rs.SetPlayerWin(false);
                sceneManager.Push((GameScene)rs);
            }
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



        sceneManager.SceneUpdate();
    }
}
