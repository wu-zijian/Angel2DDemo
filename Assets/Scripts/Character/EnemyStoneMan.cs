using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Boss石头人
///</summary>
public class EnemyStoneMan : Enemy
{
    public int Life = 2;
    public int DeathCount = 0;
    [SerializeField] private int attackState = 0;
    [Header("石头")]
    public GameObject bulletPrefab;//子弹预设

    protected override void Start()
    {
        base.Start();
        pc2D = transform.GetChild(0).gameObject.GetComponents<PolygonCollider2D>();
        GameManager.Instance.RegisterBoss(this.gameObject);
    }

    protected override void FixedUpdate()
    {
        GroundCheck();
        if (!isDeath)
        {
            Movement();
        }
    }

    protected override void Movement()
    {
        float aimDistance;
        if (!isAttack)
        {
            if (FoundPlayer())//发现玩家的情况
            {
                aimDistance = aimPoint.x - transform.position.x;
                if (Mathf.Abs(aimDistance) > attackRange * 3)//玩家未进入敌人攻击范围
                {
                    xVelocity = aimDistance / Mathf.Abs(aimDistance);//方向
                    if (isAttack) xVelocity = 0;
                    r2d.velocity = new Vector2(xVelocity * speed, r2d.velocity.y);//速度
                }
                else//玩家进入敌人攻击范围
                {
                    if (Mathf.Abs(aimDistance) > attackRange)//玩家未进入敌人攻击范围
                    {
                        xVelocity = aimDistance / Mathf.Abs(aimDistance);//方向
                        if (isAttack) xVelocity = 0;
                        r2d.velocity = new Vector2(xVelocity * speed, r2d.velocity.y);//速度
                    }
                    Attack(aimDistance);
                }
            }
            else            //未发现玩家的情况
            {
                if (patrolTime < Time.time)
                {
                    patrolTime = Time.time + patrolIntervalTime;
                    aimPoint = GetRandomPoint();
                }
                aimDistance = aimPoint.x - transform.position.x;
                if (Mathf.Abs(aimDistance) >= 0.1f)
                    xVelocity = aimDistance / Mathf.Abs(aimDistance);
                else
                    xVelocity = 0;
                r2d.velocity = new Vector2(xVelocity * speed, r2d.velocity.y);
            }
        }
        else
        {
            if (jumpTime < Time.time && isOnGround)
            {
                anim.SetInteger("indexOfAttackList", 0);
            }
            else
            {
                if (attackState == 3 && attackTime - Time.time > attackIntervalTime / 6)
                {
                    transform.position = new Vector3(Mathf.Lerp(transform.position.x, attackPoint.x, 0.05f), Mathf.Lerp(transform.position.y, attackPoint.y + attackRange * 6, 0.05f), 0);
                }
                if (attackState == 2 || attackState == 1)
                {
                    if (isAttack) xVelocity = 0;
                    r2d.velocity = new Vector2(xVelocity * speed, r2d.velocity.y);//速度
                }
            }
        }
        if (xVelocity != 0 && !isAttack)
        {
            if (xVelocity < 0)//方向控制
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
        anim.SetBool("isRun", Mathf.Abs(xVelocity) != 0);//动画控制
    }
    private float jumpTime = 0;
    protected void Attack(float aimDistance)
    {
        if (attackTime < Time.time && !isAttack)
        {
            if (Mathf.Abs(aimDistance) > 2.5f)
                attackState = (int)Random.Range(2, 4);
            else
                attackState = (int)Random.Range(1, 4);
            attackPoint = aimPoint;
            attackTime = Time.time + attackIntervalTime;
            anim.SetInteger("indexOfAttackList", attackState);
            anim.SetBool("isAttack", true);
            isAttack = true;
            jumpTime = Time.time + 0.2f;
            xVelocity = 0;
            r2d.velocity = new Vector2(xVelocity, 0);
            // r2d.AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
        }
    }

    private void EventBeforeDeath()
    {
        DeathCount++;
        if (Life - DeathCount > 0)
        {
            anim.SetBool("isDeath", false);
            anim.SetBool("isRebirth", true);
            this.characterStats.CurrentHealth = this.characterStats.MaxHealth;
        }
    }

    private void EventShoot()
    {
        if (isDeath) return;
        GameObject bullet = Instantiate(bulletPrefab, r2d.position, Quaternion.identity);
        Bullet enemybullet = bullet.GetComponent<Bullet>();
        if (enemybullet != null)
        {
            bullet.SetActive(true);
            enemybullet.Shoot(transform.localScale.x, 800);
        }
    }

    private void EventRebirth()
    {
        anim.SetBool("isRebirth", false);
        gameObject.layer = LayerMask.NameToLayer("EnemyLayer");
        isDeath = false;
    }

}

