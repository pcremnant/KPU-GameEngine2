using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : GameScene
{
    [SerializeField]
    public SceneUI sceneUI;
    public StageSelectScene stageScene;
    public TMP_Text moneyText;
    public TMP_Text resultText;
    public MainGame mainGame;
    private bool _playerWin;
    public override void CreateScene()
    {
        base.CreateScene();
        _myScene = Scene.Result;
        sceneUI.gameObject.SetActive(false);
    }

    public override void Initialize()
    {
        base.Initialize();
        sceneUI.gameObject.SetActive(true);
    }

    public override void DestroyScene()
    {
        base.DestroyScene();
        if (_playerWin)
        {
            mainGame.EarnMoney(stageScene.CurrentStageIndex * 30);
            mainGame.ClearStage(stageScene.CurrentStageIndex + 1);
        }
        sceneUI.gameObject.SetActive(false);
    }

    public override void Pause()
    {
        base.Pause();
        sceneUI.gameObject.SetActive(false);
    }

    public override void Resume()
    {
        base.Resume();
        sceneUI.gameObject.SetActive(true);
    }

    public override void SceneUpdate()
    {
        base.SceneUpdate();
        if (_playerWin)
        {
            moneyText.text = (stageScene.CurrentStageIndex * 30).ToString();
        }
        else
        {
            moneyText.text = 0.ToString();
        }
    }

    public void SetPlayerWin(bool win)
    {
        _playerWin = win;
        if (win)
        {
            resultText.text = "Stage Clear";
        }
        else
        {
            resultText.text = "Stage Fail";
        }
    }
}
