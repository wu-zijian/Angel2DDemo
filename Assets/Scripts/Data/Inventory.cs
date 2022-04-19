using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///背包信息
///</summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory", order = 0)]
public class Inventory : ScriptableObject 
{
    public List<Item> itemList = new List<Item>();
}

