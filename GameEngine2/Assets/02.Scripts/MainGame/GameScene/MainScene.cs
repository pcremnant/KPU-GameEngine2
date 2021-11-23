using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : GameScene
{
    [SerializeField]
    public SceneUI sceneUI;

    public override void CreateScene()
    {
        base.CreateScene();
        _myScene = Scene.Main;
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
    }
}
