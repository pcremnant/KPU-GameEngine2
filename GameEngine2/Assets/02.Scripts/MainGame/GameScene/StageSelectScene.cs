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
        // �ٲ�°Ÿ� Ȯ���ϰ� ���߿� UI ���ؼ� �ٲٱ�
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
