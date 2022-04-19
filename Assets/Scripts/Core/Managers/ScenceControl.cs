using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// 由于SceneManager与unity冲突，所以使用SceneControl命名

///<summary>
///场景管理
///</summary>
public class ScenceControl : Singleton<ScenceControl>
{
    public GameObject playerPrefab;
    public AllGameLevel allGameLevel;
    public GameLevel activeLevel;
    public GameObject loadingPanel;
    public Text text;
    public Image image;
    private AsyncOperation operation;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void StartTransition(GameLevel gameLevel, int gameLevelPointIndex)
    {
        if (GameManager.IsInstialized)
            StartCoroutine(Transition(GameManager.Instance.player, gameLevel, gameLevelPointIndex));
    }
    IEnumerator Transition(GameObject player, GameLevel gameLevel, int gameLevelPointIndex)
    {
        // 不同场景传送
        if (SceneManager.GetActiveScene().name != gameLevel.sceneName)
        {
            // 进入加载页面
            if (GameManager.Instance.player != null)
                SaveManager.Instance.SavePlayerData();                    //保存用户信息
            activeLevel = gameLevel;
            loadingPanel.SetActive(true);                                 //启动加载页面
            GameObject.Find("Canvas").SetActive(false);                   //关闭UI
            operation = SceneManager.LoadSceneAsync(gameLevel.sceneName);
            operation.allowSceneActivation = false;
            do
            {
                image.fillAmount = operation.progress;                      //进度条
                text.text = operation.progress * 100 + "%";                 //加载文字
                if (operation.progress >= 0.9f)                             //加载完成
                {
                    image.fillAmount = 1;
                    text.text = "100%";
                    operation.allowSceneActivation = true;
                }
                yield return null;
            } while (!operation.isDone);
            //生成人物
            yield return Instantiate(playerPrefab, gameLevel.points[gameLevelPointIndex], transform.rotation);
            if (gameLevel.sceneName != "1_1")
                SaveManager.Instance.LoadPlayerData();                          //保存人物信息
            loadingPanel.SetActive(false);                                  //关闭加载页面
            image.fillAmount = 0;                                           //初始化加载页面
            text.text = "加载中…";
            Time.timeScale = 1;
            yield break;
        }
        else
        {
            player.transform.SetPositionAndRotation(gameLevel.points[gameLevelPointIndex], player.transform.rotation);
            yield return null;
        }
    }

    public void Restart()
    {
        StartCoroutine(RestartScene());
    }
    public IEnumerator RestartScene()
    {
        // 进入加载页面
        loadingPanel.SetActive(true);
        GameObject.Find("Canvas").SetActive(false);
        operation = SceneManager.LoadSceneAsync(activeLevel.sceneName);
        operation.allowSceneActivation = false;
        do
        {
            image.fillAmount = operation.progress;
            text.text = operation.progress * 100 + "%";
            if (operation.progress >= 0.9f)
            {
                image.fillAmount = 1;
                text.text = "100%";
                operation.allowSceneActivation = true;
            }
            yield return null;
        } while (!operation.isDone);
        yield return Instantiate(playerPrefab, activeLevel.points[0], transform.rotation);
        if (SceneManager.GetActiveScene().name != "1_1")
            SaveManager.Instance.LoadPlayerData();
        loadingPanel.SetActive(false);
        image.fillAmount = 0;
        text.text = "加载中…";
        Time.timeScale = 1;
        yield break;
    }
}
