using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///启动提示框
///</summary>
public class OpenDialog : MonoBehaviour
{
    public GameObject dialog;
    private void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.gameObject.layer == LayerMask.NameToLayer("PlayerLayer") || c2d.gameObject.layer == LayerMask.NameToLayer("角色无敌状态"))
        {
            dialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D c2d)
    {
        if (c2d.gameObject.layer == LayerMask.NameToLayer("PlayerLayer") || c2d.gameObject.layer == LayerMask.NameToLayer("角色无敌状态"))
        {
            dialog.SetActive(false);
        }
    }
}
