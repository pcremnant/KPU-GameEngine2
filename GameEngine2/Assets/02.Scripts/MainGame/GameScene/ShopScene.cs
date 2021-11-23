using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScene : GameScene
{
    [SerializeField]
    public SceneUI sceneUI;

    public override void CreateScene()
    {
        base.CreateScene();
        _myScene = Scene.Playing;
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
        sceneUI.gameObject.SetActive(false);
        // ���� ������Ʈ�� �� ���� ��
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

    public void OnBuyButton()
    {
        // add something
    }
}
