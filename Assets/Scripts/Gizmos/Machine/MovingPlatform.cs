using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///移动平台
///</summary>
public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    private float waitTime;
    public float shopTime = 1f;
    public Transform[] movePos;
    private int i = 0;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)
        {
            if (waitTime < 0.0f)
            {
                if (i < movePos.Length - 1)
                    i++;
                else
                    i = 0;
                waitTime = shopTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerLayer") || other.gameObject.layer == LayerMask.NameToLayer("角色无敌状态"))
        {
            other.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerLayer") || other.gameObject.layer == LayerMask.NameToLayer("角色无敌状态"))
        {
            other.gameObject.transform.parent = null;
        }
    }
}