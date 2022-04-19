using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImformationControl : MonoBehaviour
{

    public GameObject textPanel;
    public TextAsset textAsset;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !textPanel.activeInHierarchy)
        {
            textPanel.SetActive(true);
            TalkSystemManager.Instance.initData(textAsset);
        }
    }

    void OnDisable()
    {
        textPanel.SetActive(false);
    }
}
