using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectScene : GameScene
{
    private Stage[] _stages;

    public override void Initialize() 
    {
        base.Initialize();
        var go = GameObject.Find("Map");
        _stages = go.GetComponentsInChildren<Stage>(true);
    }

    public override void DestroyScene()
    {
        base.DestroyScene();

    }

    public override void Pause()
    {
        base.Pause();
    }

    public override void Resume()
    {
        base.Resume();
    }

    public override void SceneUpdate()
    {
        base.SceneUpdate();
        // �ٲ�°Ÿ� Ȯ���ϰ� ���߿� UI ���ؼ� �ٲٱ�
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (var stage in _stages)
            {
                if (stage.stageNumber == 1)
                    stage.OnStage();
                else
                    stage.OffStage();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (var stage in _stages)
            {
                if (stage.stageNumber == 2)
                    stage.OnStage();
                else
                    stage.OffStage();
            }
        }
    }
}
