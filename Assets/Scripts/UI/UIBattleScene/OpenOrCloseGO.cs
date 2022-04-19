using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///启动或者关闭GameObject
///</summary>
public class OpenOrCloseGO : MonoBehaviour
{
    public bool value = true;
    public GameObject[] gameObjects;
    private void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.gameObject.layer == LayerMask.NameToLayer("PlayerLayer")
        || c2d.gameObject.layer == LayerMask.NameToLayer("角色无敌状态"))
            OpenOrClose();
    }

    public void OpenOrClose()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject != null)
                gameObject.SetActive(value);
        }
    }
}
