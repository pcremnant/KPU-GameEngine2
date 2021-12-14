using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData ItemData => _itemData;
    private ItemData _itemData;
    public TMP_Text itemName;
    public TMP_Text itemPrice;
    public Image itemImage;
    public ShopScene shopScene;

    public void SetItem(ItemData itemData)
    {
        _itemData = itemData;
        itemName.text = itemData.itemName;
        itemPrice.text = itemData.itemPrice.ToString();
        itemImage.sprite = itemData.itemSprite;
    }

    public void OnItemSelected()
    {
        shopScene.SelectItem(this);
    }
}
