using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///子弹基类
///</summary>
public class Bullet : MonoBehaviour
{
    public GameObject master;
    protected Rigidbody2D r2d;
    protected CapsuleCollider2D cc2D;
    protected Animator anim;
    public float LiveTime = 0.5f;
    public LayerMask aim;

    protected virtual void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
        cc2D = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        LiveTime = Time.time + LiveTime;
    }

    protected virtual void FixedUpdate()
    {
        BulletLive();
    }

    ///<summary>
    ///发射子弹
    ///</summary>
    public void Shoot(float bulletDirect, float bulletSpeed)
    {
        if (bulletDirect < 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
        }
        r2d.AddForce((new Vector2(bulletDirect, 0f)) * bulletSpeed);
    }


    protected virtual void OnTriggerStay2D(Collider2D c2d)
    {
        aim = c2d.gameObject.layer;
        cc2D.enabled = false;
        DisAppear();
    }

    ///<summary>
    ///子弹存活时间
    ///</summary>
    protected virtual void BulletLive()
    {
        if (cc2D.isActiveAndEnabled && LiveTime < Time.time)
        {
            cc2D.enabled = false;
            DisAppear();
        }
    }

    ///<summary>
    ///摧毁子弹
    ///</summary>
    protected virtual void DisAppear()
    {
        Destroy(this.gameObject);
    }
}

