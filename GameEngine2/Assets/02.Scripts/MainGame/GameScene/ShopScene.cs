using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScene : GameScene
{
    public SceneUI sceneUI;
    public ItemDatabase itemDatabase;
    public RectTransform itemTarget;
    public GameObject itemPrefab;

    public TMP_Text itemDescription;
    private Item selectedItem;
    public MainGame mainGame;

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
            io.shopScene = this;
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
        if (selectedItem != null)
            itemDescription.text = selectedItem.ItemData.description;
    }

    public void OnBuyButton()
    {
        if (mainGame.SpendMoney(selectedItem.ItemData.itemPrice))
        {
            mainGame.ApplyItem(selectedItem);
        }
        // add something
    }

    public void SelectItem(Item item)
    {
        selectedItem = item;
    }
}
