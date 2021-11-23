using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectScene : GameScene
{
    [SerializeField]
    public List<Stage> Stages;

    [SerializeField]
    public SceneUI sceneUI;

    [SerializeField]
    public TMP_Text stageText;
    
    [SerializeField]
    public Image stageImage;

    [SerializeField]
    public List<Sprite> stageImages;

    private int _currentStageIndex;
    public override void CreateScene()
    {
        base.CreateScene();
        _myScene = Scene.StageSelect;
        sceneUI.gameObject.SetActive(false);
    }

    public override void Initialize() 
    {
        base.Initialize();
        _currentStageIndex = 1;
        sceneUI.gameObject.SetActive(true);
    }

    public void OnNextStageButton()
    {
        if (_currentStageIndex < Stages.Count)
            _currentStageIndex++;
    }

    public void OnPrevStageButton()
    {
        if (_currentStageIndex > 1)
            _currentStageIndex--;
    }

    public void OnSelectStageButton()
    {
        foreach(var stage in Stages)
        {
            if (stage.stageNumber == _currentStageIndex)
                stage.OnStage();
            else
                stage.OffStage();
        }
    }

    public override void DestroyScene()
    {
        base.DestroyScene();
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

        if (_currentStageIndex == 1) 
            stageText.text = "Tutorial";
        else
            stageText.text = "Stage " + (_currentStageIndex - 1).ToString();
        stageImage.sprite = stageImages[_currentStageIndex - 1];
    }
}
