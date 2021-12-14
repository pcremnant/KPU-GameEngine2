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
    public MainGame mainGame;

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
        mainGame.EarnMoney(stageScene.CurrentStageIndex * 30);
        mainGame.ClearStage(stageScene.CurrentStageIndex + 1);
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
        moneyText.text = (stageScene.CurrentStageIndex * 30).ToString();
    }
}
