using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodsType
{
    Gem,
    SandClock,
    Level,
}
[System.Serializable]
public class MissionGoods
{
    public GoodsType goodsType;
    public int goodCount;
}
[System.Serializable]
public class MissionData
{
    [Header("任务的描述")]
    public string des;
    [Header("任务的ID")]
    public int id;
    [Header("任务的需求")]
    public List<MissionGoods> needGoods = new List<MissionGoods>();
    [Header("任务的奖励")]
    public List<MissionGoods> rewardGoods = new List<MissionGoods>();
}
[CreateAssetMenu(fileName = "Mission", menuName = "Mission/Data", order = 0)]
public class Mission : ScriptableObject
{
    public List<MissionData> missionDatas;
}
