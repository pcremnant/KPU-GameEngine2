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

    public int Grenade => _grenade;
    private int _grenade;
    public TMP_Text grenadeText;

    public PlayerInfo playerInfo;

    public Stage stage1;
    public Stage stage2;
    public Stage stage3;
    
    public void SetAdditionalHp(int damage)
    {
        _additionalHp -= damage;
        if (_additionalHp < 0)
            _additionalHp = 0;
    }

    public void SaveInfo()
    {
        PlayerPrefs.SetInt("Hp", AdditionalHp);
        PlayerPrefs.SetInt("gunLevel", GunLevel);
        //  PlayerPrefs.SetInt("magAmmo",   magAmmo);
        //PlayerPrefs.SetInt("ammoRemain", gun.ammoRemain);
        PlayerPrefs.SetInt("money", Money);
        PlayerPrefs.SetInt("stage", MaxStage);
        // PlayerPrefs.SetInt("grenade", grenade);
        _grenade = playerInfo.grenade;
        PlayerPrefs.SetInt("grenade", Grenade);
    }

    public void LoadInfo()
    {
        if (PlayerPrefs.HasKey("Hp"))
        {
            _additionalHp = PlayerPrefs.GetInt("Hp");
            _money = PlayerPrefs.GetInt("money");
            _maxStage = PlayerPrefs.GetInt("stage");
            _gunLevel = PlayerPrefs.GetInt("gunLevel");
            _grenade = PlayerPrefs.GetInt("grenade");
            playerInfo.grenade = Grenade;
            // hp = PlayerPrefs.GetInt("Hp");
            //gun.ammoRemain = PlayerPrefs.GetInt("ammoRemain");
            // money = PlayerPrefs.GetInt("money");
            // grenade = PlayerPrefs.GetInt("grenade");
        }
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
// ???????????? ????????????
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
            moneyText.text = Money.ToString();
        }

        if (currScene == Scene.Playing) // if (game end)
        {
            // ?????? ?????? -> ?????? ?????? ???????????? ???????????? ?????? or ?????? ??????
            if (Input.GetKeyDown(KeyCode.Alpha3) || TutorialMgr.curScore == 10)
            {
                ResultScene rs = (ResultScene)GetScene(Scene.Result);
                rs.SetPlayerWin(true);
                sceneManager.Push((GameScene)rs);
                TutorialMgr.curScore = 0;
            }
            
            if(stage2.IsOnStage() && PlayerInfo.destroySpanwerCnt >= stage2.enemySpawner.Count)
            {
                ResultScene rs = (ResultScene)GetScene(Scene.Result);
                rs.SetPlayerWin(true);
                sceneManager.Push((GameScene)rs);
                TutorialMgr.curScore = 0;
            }

            if(stage3.IsOnStage() && PlayerInfo.destroySpanwerCnt >= stage3.enemySpawner.Count)
            {
                ResultScene rs = (ResultScene)GetScene(Scene.Result);
                rs.SetPlayerWin(true);
                sceneManager.Push((GameScene)rs);
                TutorialMgr.curScore = 0;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha4) || GameObject.Find("Player").GetComponent<PlayerInfo>().hp <= 0)
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
