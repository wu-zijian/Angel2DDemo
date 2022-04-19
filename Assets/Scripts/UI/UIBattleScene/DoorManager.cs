using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///传送门管理
///</summary>
public class DoorManager : MonoBehaviour
{
    public GameObject enterDialog;
    // 传送目标
    public GameLevel gameLevel;
    public int gameLevelPointIndex;
    public bool isEnter = false;

    private void Start()
    {
        enterDialog = transform.GetChild(0).GetChild(0).gameObject;
    }

    private void Update()
    {
        if (enterDialog.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            isEnter = true;
        }
    }

    private void FixedUpdate()
    {
        if (isEnter)
        {
            ScenceControl.Instance.StartTransition(gameLevel, gameLevelPointIndex);
            isEnter = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enterDialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enterDialog.SetActive(false);
        }
    }
}
