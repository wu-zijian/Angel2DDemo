using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

///<summary>
///UI管理
///</summary>
public class UIManager : Singleton<UIManager>, IEndGameObserver
{
    public GameObject PausePanel;
    public GameObject MisionPanel;
    public GameLevel gameLevel;
    public PlayableDirector director;
    public GameObject gameOverPanel;
    public GameObject vectoryPanel;
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddObserver(this);
        }
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RemoveObserver(this);
        }
    }

    ///<summary>
    ///开始Timeline
    ///</summary>
    public void StartTimeline()
    {
        if (director != null)
        {
            PlayerPrefs.DeleteKey("key");
            director.Play();
            director.stopped += StartGame;
        }
    }

    ///<summary>
    ///开始游戏
    ///</summary>
    public void StartGame(PlayableDirector obj)
    {
        ResumeGame();
        ScenceControl.Instance.StartTransition(gameLevel, 0);
    }

    ///<summary>
    ///开始游戏
    ///</summary>
    public void StartGame()
    {
        ResumeGame();
        ScenceControl.Instance.StartTransition(gameLevel, 0);
    }

    ///<summary>
    ///退出游戏
    ///</summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    ///<summary>
    ///注册暂停面板
    ///</summary>
    public void RegisterPausePanel(GameObject gameObject)
    {
        PausePanel = gameObject;
    }

    ///<summary>
    ///重新开始
    ///</summary>
    public void Restart()
    {
        ScenceControl.Instance.Restart();
    }

    ///<summary>
    ///暂停
    ///</summary>
    public void PauseGame()//暂停游戏
    {
        if (PausePanel != null)
        {
            PausePanel.SetActive(true);

            //初始化
            float value;
            SoundsManager.Instance.mainAudioMixer.GetFloat("MainVolume", out value);
            PausePanel.transform.Find("音量").gameObject.GetComponent<Slider>().value = value;
            SoundsManager.Instance.audioMixer.GetFloat("MainVolume", out value);
            PausePanel.transform.Find("音效").gameObject.GetComponent<Slider>().value = value;
            PausePanel.transform.Find("全屏").gameObject.GetComponent<Toggle>().isOn = Screen.fullScreen;
        }

        Time.timeScale = 0;
    }

    ///<summary>
    ///任务
    ///</summary>
    public void OpenMissionPanel()//暂停游戏
    {
        if (MisionPanel != null)
        {
            MisionPanel.SetActive(true);
        }
        Time.timeScale = 0;
    }

    ///<summary>
    ///继续游戏
    ///</summary>
    public void ResumeGame()
    {
        if (PausePanel != null)
            PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    ///<summary>
    ///返回界面
    ///</summary>
    public void Back()
    {
        SceneManager.LoadScene("开始界面");
    }

    ///<summary>
    ///设置音量
    ///</summary>
    public void SetMainVolume(float value)
    {
        SoundsManager.Instance.mainAudioMixer.SetFloat("MainVolume", value);
    }

    ///<summary>
    ///设置音效
    ///</summary>
    public void SetVolume(float value)
    {
        SoundsManager.Instance.audioMixer.SetFloat("MainVolume", value);
    }

    ///<summary>
    ///设置全屏
    ///</summary>
    public void SetFullScreen(bool value)
    {
        if (value)
        {
            //获取设置当前屏幕分辩率
            Resolution[] resolutions = Screen.resolutions;
            //设置当前分辨率
            Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);

            Screen.fullScreen = true;  //设置成全屏
        }
        else
        {
            //获取设置当前屏幕分辩率
            Resolution[] resolutions = Screen.resolutions;
            //设置当前分辨率
            Screen.SetResolution(resolutions[resolutions.Length - 1].width - 100, resolutions[resolutions.Length - 1].height - 100, true);

            Screen.fullScreen = false;  //设置成不全屏
        }

    }

    ///<summary>
    ///订阅回调函数
    ///</summary>
    public void EndNotify()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    ///<summary>
    ///胜利
    ///</summary>
    public void Vectory()
    {
        if (vectoryPanel != null)
        {
            SoundsManager.Instance.VectoryAudio();
            if (InventoryManager.FindItem("钻石") != null)
                vectoryPanel.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "" + InventoryManager.FindItem("钻石").itemHeld;
            vectoryPanel.SetActive(true);
        }
    }

}
