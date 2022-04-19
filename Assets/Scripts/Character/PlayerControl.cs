using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

///<summary>
///玩家
///</summary>
public class PlayerControl : Character
{
    public float rollSpeed = 6f;
    bool isRoll = false;

    [Header("按键设置")]
    bool jumpPressed;
    bool crouchHeld;
    bool shootPress;
    bool rollPress;
    bool BagPress;

    [Header("伴随物件")]
    public GameObject bulletPrefab;//子弹预设
    public GameObject PlayerBag;//背包

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.RegisterPlayer(this.gameObject);
    }

    void Update()
    {
        if (jumpCount > 0 && Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
        crouchHeld = Input.GetButton("Crouch");
        shootPress = Input.GetKey(KeyCode.J);
        rollPress = Input.GetKey(KeyCode.K);
        BagPress = Input.GetKey(KeyCode.B);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!isDeath)
        {
            if (!isRoll && !isHited)
            {
                Movement();
                JumpControl();
                Shoot();
                Roll();
            }
            BagControl();
        }
    }


    float xVelocity = 0;
    void Movement()
    {
        // 判断移动
        xVelocity = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(xVelocity * speed, r2d.velocity.y);
        // 移动
        if (!isAttack)
        {
            // 方向
            if (xVelocity < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (xVelocity > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        anim.SetBool("isRun", Mathf.Abs(xVelocity) != 0);
    }

    private float jumpTime = 0;
    private void JumpControl()
    {
        if (isOnGround)
        {
            if (crouchHeld && isOnOWG)//单向平台下落
            {
                gameObject.layer = LayerMask.NameToLayer("角色无敌状态");
                isJump = true;
                anim.SetBool("isJump", isJump);
                anim.SetBool("isFall", true);
                jumpCount = jumpTopCount - 1;
                jumpTime = Time.time + 0.1f;
                StartCoroutine("FallOWG");
            }
            if (jumpTime < Time.time)// 起跳时间判断
            {
                jumpCount = jumpTopCount;
                isJump = false;
                anim.SetBool("isJump", isJump);
                anim.SetBool("isFall", false);
            }
        }
        else
        {
            if (!isJump)//对于没有跳跃但是掉落的情况
            {
                isJump = true;
                anim.SetBool("isJump", isJump);
                jumpCount = jumpTopCount - 1;
            }
            if (r2d.velocity.y < 0)
            {
                anim.SetBool("isFall", true);
            }
            else
            {
                anim.SetBool("isFall", false);
            }
        }
        if (jumpPressed && jumpCount > 0)
        {
            isJump = true;
            anim.SetBool("isJump", true);//设置动画
            SoundsManager.Instance.JumpAudio();//跳跃音效
            jumpTime = Time.time + 0.1f;
            jumpCount--;//跳跃次数减一
            r2d.velocity = new Vector2(r2d.velocity.x, 0f);//清除上一次跳跃的效果
            r2d.AddForce(new Vector2(0f, jumpFore), ForceMode2D.Impulse);//为角色施加向上的力
            jumpPressed = false;
        }
    }
    private IEnumerator FallOWG()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = LayerMask.NameToLayer("PlayerLayer");
    }

    float ShootWaitTime = 0;
    private void Shoot()
    {
        if (ShootWaitTime <= Time.time)
        {
            isAttack = false;
            anim.SetBool("isShoot", false);
            if (shootPress)
            {
                ShootWaitTime = Time.time + 0.4f;
                GameObject bullet = Instantiate(bulletPrefab, r2d.position, Quaternion.identity);
                Bullet playerbullet = bullet.GetComponent<Bullet>();
                if (playerbullet != null)
                {
                    isAttack = true;
                    anim.SetBool("isShoot", true);
                    bullet.SetActive(true);
                    playerbullet.Shoot(transform.localScale.x, 1000);
                    SoundsManager.Instance.ShootAudio();//音效
                }
            }
        }
    }

    private void Roll()
    {
        if (rollPress)
        {
            gameObject.layer = LayerMask.NameToLayer("角色无敌状态");
            isRoll = true;
            anim.SetBool("isRoll", true);
            if (xVelocity == 0)
                r2d.velocity = new Vector2(transform.localScale.x / Math.Abs(transform.localScale.x) * rollSpeed, 0f);
            else
                r2d.velocity = new Vector2(xVelocity / Math.Abs(xVelocity) * rollSpeed, 0f);
            r2d.gravityScale = 0;
        }
    }

    public void EventAfterRoll()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerLayer");
        r2d.velocity = new Vector2(0f, 0f);
        isRoll = false;
        anim.SetBool("isRoll", false);
        r2d.gravityScale = 1;
    }

    float BagWaitTime = 0;
    private void BagControl()
    {
        if (PlayerBag == null)
        {
            PlayerBag = GameObject.Find("Canvas").transform.Find("背包").gameObject;
        }
        if (BagPress && BagWaitTime <= Time.time)
        {
            BagWaitTime = Time.time + 0.3f;
            PlayerBag.SetActive(!PlayerBag.activeSelf);
        }
    }

    public void Hurt()
    {
        isHited = true;
        anim.SetBool("isHited", true);
        xVelocity = 0;
        r2d.velocity = new Vector2(0, r2d.velocity.y);
        gameObject.layer = LayerMask.NameToLayer("角色无敌状态");
        StartCoroutine("SetIsHited");
        SoundsManager.Instance.HurtAudio();//音效
        DeathCheck();
    }
    protected virtual IEnumerator SetIsHited()
    {
        yield return new WaitForSeconds(1f);
        if (!isDeath)
            gameObject.layer = LayerMask.NameToLayer("PlayerLayer");
    }
    public override bool DeathCheck()
    {
        if (base.DeathCheck())
        {
            SoundsManager.Instance.DeathAudio();//音效
            return true;
        }
        return false;
    }
    protected override void EventDeath()
    {
        SoundsManager.Instance.GameOverAudio();//音效
        GameManager.Instance.NotifyObservers();
    }
}
