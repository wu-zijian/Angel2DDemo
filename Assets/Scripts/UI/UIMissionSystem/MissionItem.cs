using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : MonoBehaviour
{
    public int id;
    private Text des;
    private Text need;
    private Text reward;
    private Button getBtn;

    public bool isFinish = false;
    private void Awake()
    {
        des = transform.Find("des").GetComponent<Text>();
        need = transform.Find("need").GetComponent<Text>();
        reward = transform.Find("reward").GetComponent<Text>();
        getBtn = transform.Find("getBtn").GetComponent<Button>();
        getBtn.onClick.AddListener(onClick);
    }

    private void OnEnable()
    {
        // if (isFinish) this.getBtn.enabled = false;
        MissionData missionData = MissionManager.Instance.mission.missionDatas[id];
        des.text = missionData.des;
        int j = 0;
        need.text = "";
        reward.text = "";
        for (int i = 0; i < missionData.needGoods.Count; i++)
        {
            switch (missionData.needGoods[i].goodsType)
            {
                case GoodsType.Level:

                    if (GameManager.Instance != null)
                    {
                        int level = GameManager.Instance.player.GetComponent<CharacterStats>().CurrentLevel;
                        need.text = need.text + "等级：\n" + level + "/" + missionData.needGoods[i].goodCount + "\n";
                        if (level < missionData.needGoods[i].goodCount)
                            j++;
                    }
                    break;
                default:
                    if (InventoryManager.Instance != null)
                    {
                        Item item = InventoryManager.FindItem(missionData.needGoods[i].goodsType.ToString());
                        int num = 0;
                        if (item != null)
                            num = item.itemHeld;
                        need.text = need.text + missionData.needGoods[i].goodsType.ToString() + "：\n" + num + "/" + missionData.needGoods[i].goodCount + "\n";
                        if (num < missionData.needGoods[i].goodCount)
                            j++;
                    }
                    break;
            }
        }
        if (j == 0)
        {
            this.isFinish = true;
            this.getBtn.gameObject.SetActive(true);
        }
        for (int i = 0; i < missionData.rewardGoods.Count; i++)
        {
            switch (missionData.rewardGoods[i].goodsType)
            {
                case GoodsType.Level:
                    break;
                default:
                    reward.text = reward.text + missionData.rewardGoods[i].goodsType.ToString() + "：\n" + missionData.rewardGoods[i].goodCount + "\n";
                    break;
            }
        }
    }

    public void onClick()
    {
        MissionData missionData = MissionManager.Instance.mission.missionDatas[id];
        for (int i = 0; i < missionData.rewardGoods.Count; i++)
        {
            switch (missionData.rewardGoods[i].goodsType)
            {
                case GoodsType.Level:
                    break;
                default:
                    if (MissionManager.Instance != null)
                    {
                        for (int j = 0; j < MissionManager.Instance.rewardList.Count; j++)
                        {
                            if (MissionManager.Instance.rewardList[j].itemName == missionData.rewardGoods[i].goodsType.ToString())
                            {
                                Inventory playerInventory = InventoryManager.Instance.PlayerBag;
                                for (int k = 0; k < playerInventory.itemList.Count; k++)
                                {
                                    if (playerInventory.itemList[k] != null && playerInventory.itemList[k].itemName == MissionManager.Instance.rewardList[j].itemName)
                                    {
                                        playerInventory.itemList[k].itemHeld += missionData.rewardGoods[i].goodCount;
                                        InventoryManager.RefreshItem();
                                        break;
                                    }
                                }
                                for (int k = 0; k < playerInventory.itemList.Count; k++)
                                {
                                    if (playerInventory.itemList[k] == null)
                                    {
                                        playerInventory.itemList[k] = Instantiate(MissionManager.Instance.rewardList[j]);
                                        playerInventory.itemList[k].itemHeld = missionData.rewardGoods[i].goodCount;
                                        InventoryManager.RefreshItem();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }

        getBtn.interactable = false;
    }

}
