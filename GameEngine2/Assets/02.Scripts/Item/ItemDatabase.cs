using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Item Database", menuName = "Item/Create Item Database", order = 0)]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> itemDatas;
}
