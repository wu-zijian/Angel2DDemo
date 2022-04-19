using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

///<summary>
///背包里的物品
///</summary>
public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;//物品原来位置
    public Inventory PlayerBag;//背包物品
    private int currentItemID;//当前物品原来ID
    public void OnBeginDrag(PointerEventData eventData)//开始拖动物品
    {
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);//物品跳出父类
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)//拖到物品过程
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)//停止拖动物品
    {
        if (eventData.pointerCurrentRaycast.gameObject.name != null)
        {
            GameObject eventItem = eventData.pointerCurrentRaycast.gameObject;
            if (eventItem.name == "Image")//判断下面物体名字是:Item Image那么互换位置
            {
                transform.SetParent(eventItem.transform.parent.parent);
                transform.position = eventItem.transform.parent.parent.position;
                //itemList的物品存储位置改变
                var temp = PlayerBag.itemList[currentItemID];
                PlayerBag.itemList[currentItemID] = PlayerBag.itemList[eventItem.GetComponentInParent<Slot>().slotID];
                PlayerBag.itemList[eventItem.GetComponentInParent<Slot>().slotID] = temp;

                eventItem.transform.parent.position = originalParent.position;
                eventItem.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;//射线阻挡开启，不然无法再次选中移动的物品
            }
            else if (eventItem.name == "ItemOnBag(Clone)")
            {
                //否则直接挂在检测到到Slot下面
                transform.SetParent(eventItem.transform);
                transform.position = eventItem.transform.position;
                //itemList的物品存储位置改变
                PlayerBag.itemList[eventItem.GetComponentInParent<Slot>().slotID] = PlayerBag.itemList[currentItemID];
                //解决自己故在自己位置的问题
                if (eventItem.GetComponent<Slot>().slotID != currentItemID)
                    PlayerBag.itemList[currentItemID] = null;

                GetComponent<CanvasGroup>().blocksRaycasts = true;//射线阻挡开启，不然无法再次选中移动的物品
            }
            else
            {
                //其他任何位置都归位物品
                transform.SetParent(originalParent);
                transform.position = originalParent.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;//射线阻挡开启，不然无法再次选中移动的物品
            }
        }
        else
        {
            //其他任何位置都归位物品
            transform.SetParent(originalParent);
            transform.position = originalParent.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;//射线阻挡开启，不然无法再次选中移动的物品
        }
    }
}
