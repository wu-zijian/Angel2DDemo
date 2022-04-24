using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : Singleton<MissionManager>
{
    public Mission templateData;
    public Mission mission;
    public List<Item> rewardList;

    protected override void Awake()
    {
        base.Awake();
        if (mission == null && templateData != null)
        {
            mission = Instantiate(templateData);
        }
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
