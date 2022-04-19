using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///背包管理
///</summary>
public class InventoryManager : Singleton<InventoryManager>
{
    public Inventory templateData;
    public Inventory PlayerBag;
    public GameObject slotGrid;
    // public Slot slotPrefab;
    public GameObject emptySlot;
    public Text itemImformation;
    List<GameObject> slots = new List<GameObject>();

    private void OnEnable()
    {
        if (PlayerBag == null && templateData != null)
        {
            PlayerBag = Instantiate(templateData);
        }
        RefreshItem();
        Instance.itemImformation.text = "";
    }

    ///<summary>
    ///修改物品信息
    ///</summary>
    ///<param>物品名称</param>
    public static void UpdateItemInfo(string ItemDescription)
    {
        Instance.itemImformation.text = ItemDescription;
    }

    ///<summary>
    ///刷新背包
    ///</summary>
    public static void RefreshItem()
    {
        //清空背包
        for (int i = 0; i < Instance.slotGrid.transform.childCount; i++)
        {
            if (Instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(Instance.slotGrid.transform.GetChild(i).gameObject);
            Instance.slots.Clear();
        }
        //重新加载背包
        for (int i = 0; i < Instance.PlayerBag.itemList.Count; i++)
        {
            // CreateNewItem(Instance.PlayerBag.itemList[i]);
            Instance.slots.Add(Instantiate(Instance.emptySlot));
            Instance.slots[i].transform.SetParent(Instance.slotGrid.transform);
            Instance.slots[i].transform.localScale = new Vector3(1, 1, 1);
            Instance.slots[i].GetComponent<Slot>().slotID = i;
            Instance.slots[i].GetComponent<Slot>().SetupSlot(Instance.PlayerBag.itemList[i]);
        }
    }

    ///<summary>
    ///寻找背包物品
    ///</summary>
    public static Item FindItem(string name)
    {
        for (int i = 0; i < Instance.PlayerBag.itemList.Count; i++)
        {
            if (Instance.PlayerBag.itemList[i] == null)
                continue;
            if (Instance.PlayerBag.itemList[i].itemName == name)
            {
                return Instance.PlayerBag.itemList[i];
            }
        }
        return null;
    }
}
