using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectScene : GameScene
{
    Stage[] stages;
    
    private int _curStage = 0;

    // Start is called before the first frame update
    void Start()
    {
       stages = FindObjectsOfType<Stage>();
    }

    // Update is called once per frame
    void Update()
    {
        // 바뀌는거만 확인하고 나중에 UI 통해서 바꾸기
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (var stage in stages)
            {
                if (stage.stageNumber == 1)
                    stage.OnStage();
                else
                    stage.OffStage();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (var stage in stages)
            {
                if (stage.stageNumber == 2)
                    stage.OnStage();
                else
                    stage.OffStage();
            }
        }
    }

    public override void Initialize() 
    {
        base.Initialize();

    }

    public override void DestroyScene()
    {
        base.DestroyScene();

    }
}
