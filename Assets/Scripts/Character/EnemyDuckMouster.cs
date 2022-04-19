using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///鸭怪
///</summary>
public class EnemyDuckMouster : Enemy
{
    
    protected override void Start()
    {
        base.Start();
        pc2D = transform.GetChild(0).gameObject.GetComponents<PolygonCollider2D>();
    }

    protected override void FixedUpdate()
    {
        GroundCheck();
        if (!isDeath && !isHited)
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
                if (Mathf.Abs(aimDistance) > attackRange)//玩家未进入敌人攻击范围
                {
                    xVelocity = aimDistance / Mathf.Abs(aimDistance);//方向
                    if (isHited) xVelocity = 0;
                    r2d.velocity = new Vector2(xVelocity * speed, r2d.velocity.y);//速度
                }
                else//玩家进入敌人攻击范围
                {
                    Attack();
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
                isAttack = false;
                anim.SetBool("isFly", false);
                SetAttackFalse(0);
            }
            else
            {
                if (attackTime - Time.time > attackIntervalTime / 6)
                {
                    transform.position = new Vector3(Mathf.Lerp(transform.position.x, attackPoint.x, 0.02f), Mathf.Lerp(transform.position.y, attackPoint.y + attackRange * 3, 0.02f), 0);
                }
                // else
                // {
                //     transform.position = new Vector3(Mathf.Lerp(transform.position.x, attackPoint.x, 0.02f), transform.position.y, 0);
                // }
            }
        }
        if (xVelocity != 0)
        {
            if (xVelocity < 0)//方向控制
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
        anim.SetBool("isRun", Mathf.Abs(xVelocity) != 0);//动画控制
    }
    private float jumpTime = 0;
    protected override void Attack()
    {
        if (attackTime < Time.time && !isAttack)
        {
            attackPoint = aimPoint;
            attackTime = Time.time + attackIntervalTime;
            anim.SetBool("isFly", true);
            isAttack = true;
            jumpTime = Time.time + 0.2f;
            xVelocity = 0;
            r2d.velocity = new Vector2(xVelocity, 0);
            // r2d.AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
        }
    }
    


}
