using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

///<summary>
///游戏管理
///</summary>
public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public GameObject boss;
    public CinemachineVirtualCamera playerCamera;
    public Inventory templateData;
    List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    ///<summary>
    ///注册玩家信息
    ///</summary>
    public void RegisterPlayer(GameObject player)
    {
        this.player = player;
        playerCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (playerCamera != null)
        {
            playerCamera.Follow = player.transform;
        }
    }

    ///<summary>
    ///注册Boss信息
    ///</summary>
    public void RegisterBoss(GameObject boss)
    {
        this.boss = boss;
        GameObject.Find("UI界面").transform.Find("Boss信息").gameObject.SetActive(true);
    }

    ///<summary>
    ///添加订阅者
    ///</summary>
    public void AddObserver(IEndGameObserver observer)
    {
        endGameObservers.Add(observer);
    }

    ///<summary>
    ///移除订阅者
    ///</summary>
    public void RemoveObserver(IEndGameObserver observer)
    {
        endGameObservers.Remove(observer);
    }

    ///<summary>
    ///发布订阅
    ///</summary>
    public void NotifyObservers()
    {
        foreach (var observer in endGameObservers)
        {
            observer.EndNotify();
        }
    }
}
