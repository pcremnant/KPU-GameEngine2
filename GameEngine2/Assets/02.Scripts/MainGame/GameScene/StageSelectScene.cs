using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectScene : GameScene
{
    [SerializeField] List<GameObject> stage;

    public int curStage => _curStage;
    private int _curStage = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 바뀌는거만 확인하고 나중에 UI 통해서 바꾸기
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (var s in stage)
            {
                s.SetActive(false);
            }
            stage[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (var s in stage)
            {
                s.SetActive(false);
            }
            stage[1].SetActive(true);
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

    public override void SceneUpdate()
    {
        base.SceneUpdate();
    }
}
