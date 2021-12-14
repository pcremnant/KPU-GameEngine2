using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScene : GameScene
{
    public SceneUI sceneUI;
    public ItemDatabase itemDatabase;
    public RectTransform itemTarget;
    public GameObject itemPrefab;
    public override void CreateScene()
    {
        base.CreateScene();
        _myScene = Scene.Shop;
        if (!sceneUI.gameObject.activeSelf)
            sceneUI.gameObject.SetActive(true);
        foreach (var item in itemDatabase.itemDatas)
        {
            var go = Instantiate(itemPrefab, itemTarget);
            var io = go.GetComponent<Item>();
            io.SetItem(item);
        }
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
        // 게임 오브젝트들 다 지울 것
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
