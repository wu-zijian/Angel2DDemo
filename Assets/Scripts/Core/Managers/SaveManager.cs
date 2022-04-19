using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///信息存储
///</summary>
public class SaveManager : Singleton<SaveManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    public void Save(Object data, string key)
    {
        var jsonData = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }
    public void Load(Object data, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
        }
    }

    ///<summary>
    ///保存玩家信息
    ///</summary>
    public void SavePlayerData()
    {
        Save(GameManager.Instance.player.GetComponent<PlayerControl>().characterStats.characterData_SO, "key");
        GameManager.Instance.templateData = InventoryManager.Instance.PlayerBag;
    }
    ///<summary>
    ///加载玩家信息
    ///</summary>
    public void LoadPlayerData()
    {
        Load(GameManager.Instance.player.GetComponent<PlayerControl>().characterStats.characterData_SO, "key");
        if (GameManager.Instance.templateData != null)
        {
            InventoryManager.Instance.PlayerBag = Instantiate(GameManager.Instance.templateData);
            InventoryManager.RefreshItem();
        }

    }
}
