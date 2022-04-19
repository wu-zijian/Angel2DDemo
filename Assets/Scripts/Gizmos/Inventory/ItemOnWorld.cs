using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///战场上的物品
///</summary>
public class ItemOnWorld : MonoBehaviour
{
    public Item templateData;
    public Item thisItem;
    private void Start()
    {
        if (thisItem == null && templateData != null)
        {
            thisItem = Instantiate(templateData);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerLayer") || other.gameObject.layer == LayerMask.NameToLayer("角色无敌状态"))
        {
            if (AddNewItem())
            {
                SoundsManager.Instance.EatAudio();
                Destroy(gameObject);
            }
        }
    }
    private bool AddNewItem()
    {
        Inventory playerInventory = InventoryManager.Instance.PlayerBag;
        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (playerInventory.itemList[i] != null && playerInventory.itemList[i].itemName == thisItem.itemName)
            {
                playerInventory.itemList[i].itemHeld += thisItem.itemHeld;
                InventoryManager.RefreshItem();
                return true;
            }
        }
        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (playerInventory.itemList[i] == null)
            {
                playerInventory.itemList[i] = thisItem;
                InventoryManager.RefreshItem();
                return true;
            }
        }
        return false;
    }
}
