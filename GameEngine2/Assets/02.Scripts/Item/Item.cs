using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // public ItemData itemData;
    public TMP_Text itemName;
    public TMP_Text itemPrice;
    public Image itemImage;

    public void SetItem(ItemData itemData)
    {
        itemName.text = itemData.itemName;
        itemPrice.text = itemData.itemPrice.ToString();
        itemImage.sprite = itemData.itemSprite;
    }
}
