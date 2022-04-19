using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///背包中的插槽
///</summary>
public class Slot : MonoBehaviour
{
    public int slotID;
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    public GameObject ItemInSlot;

    public void ItemOnClick()//点击物品时展示信息
    {
        InventoryManager.UpdateItemInfo(slotInfo);
    }

    public void SetupSlot(Item item)
    {
        if (item == null)
        {
            ItemInSlot.SetActive(false);
        }
        else
        {
            slotImage.sprite = item.itemImage;
            slotNum.text = item.itemHeld.ToString();
            slotInfo = item.itemInfo;
        }
    }
}
