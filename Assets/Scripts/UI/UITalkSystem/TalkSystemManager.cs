using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkSystemManager : Singleton<TalkSystemManager>
{

    public Text text;
    public Image faceImage;

    public TextAsset textFile;
    public int index = 0;
    public float textSpeed = 0.01f;
    private IEnumerator coroutine;
    public List<Sprite> faces;

    List<string> textList = new List<string>();
    // void Awake()
    // {

    // }

    // void OnEnable()
    // {
    // text.text = textList[index];
    // index++;

    // }

    void OnDisable()
    {
        index = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))//按E切换
        {
            if (index >= textList.Count)
            {
                gameObject.SetActive(false);
                return;
            }
            // text.text = textList[index];
            // index++;

            StopCoroutine(coroutine);
            coroutine = SetTextUI();
            StartCoroutine(coroutine);

        }

    }

    public void GetTextFromFile(TextAsset textFile)//文件初始化
    {
        textList.Clear();
        index = 0;

        var LineDate = textFile.text.Split('\n');

        foreach (var line in LineDate)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        text.text = "";
        switch (textList[index].Trim().ToString())//切换头像图片
        {
            case "A":
                faceImage.sprite = faces[1];
                index++;
                break;
            case "B":
                faceImage.sprite = faces[0];
                index++;
                break;
        }
        index++;
        for (int i = 0; i < textList[index - 1].Length; i++)//缓慢输出文字
        {
            text.text += textList[index - 1][i];
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void initData(TextAsset ta)
    {
        textFile = ta;
        GetTextFromFile(textFile);
        coroutine = SetTextUI();
        StartCoroutine(coroutine);
    }


}
