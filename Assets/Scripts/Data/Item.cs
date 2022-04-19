using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<summary>
///物品信息
///</summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;//物品名称
    public Sprite itemImage;//物品图片
    public int itemHeld;//物品数量
    [TextArea]
    public string itemInfo;//物品介绍
    public bool equip;//是否为装备
}