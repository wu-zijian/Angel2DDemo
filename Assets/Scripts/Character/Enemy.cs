using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///敌人基类
///</summary>
public class Enemy : Character, IEndGameObserver
{
    public float findPlayerRange = 8f;//发现玩家距离
    protected Vector3 originPoint;  //初始位置
    protected Vector3 aimPoint;     //攻击目标位置
    public float patrolDistance = 5f;//巡逻范围
    public float attackRange = 1.5f;//开始攻击范围
    public float xVelocity = 0;     //速度
    protected PolygonCollider2D[] pc2D; //攻击有效范围
    public List<GameObject> Items;  //掉落物

    #region 生命周期
    protected override void Start()
    {
        base.Start();
        originPoint = transform.position;
    }
    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddObserver(this);
        }
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RemoveObserver(this);
        }
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!isDeath && !isAttack)
        {
            Movement();
        }
    }
    #endregion

    protected float patrolTime = 0;//巡逻时间
    public float patrolIntervalTime = 3f;//巡逻间隔时间
    protected virtual void Movement()
    {
        float aimDistance;
        if (FoundPlayer())//发现玩家的情况
        {
            aimDistance = aimPoint.x - transform.position.x;
            if (Mathf.Abs(aimDistance) > attackRange)//玩家未进入敌人攻击范围，前进
            {
                xVelocity = Mathf.Sign(aimDistance);//方向
                if (isHited) xVelocity = 0;
                r2d.velocity = new Vector2(xVelocity * speed, r2d.velocity.y);//速度
            }
            else                                    //玩家进入敌人攻击范围，停下脚步攻击
            {
                xVelocity = 0;
                r2d.velocity = new Vector2(0, r2d.velocity.y);
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
                xVelocity = Mathf.Sign(aimDistance);
            else
                xVelocity = 0;
            r2d.velocity = new Vector2(xVelocity * speed, r2d.velocity.y);
        }
        if (xVelocity != 0)
        {
            if (xVelocity < 0)//方向控制
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
        anim.SetBool("isRun", xVelocity != 0);//动画控制
    }

    protected float attackTime = 0;//攻击速度
    public float attackIntervalTime = 3f;//攻击速度
    protected Vector3 attackPoint;
    ///<summary>
    ///攻击
    ///</summary>
    protected virtual void Attack()
    {
        if (attackTime < Time.time)
        {
            isAttack = true;
            attackTime = Time.time + attackIntervalTime;
            anim.SetBool("isAttack", true);
        }
    }

    ///<summary>
    ///停止攻击
    ///</summary>
    protected virtual void EventStopAttack()
    {
        isAttack = false;
        anim.SetBool("isAttack", false);
    }

    ///<summary>
    ///发现玩家
    ///</summary>
    protected virtual bool FoundPlayer()
    {
        var player = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), findPlayerRange, LayerMask.GetMask("PlayerLayer"));
        if (player.Length != 0)
        {
            aimPoint = player[0].transform.position;
            return true;
        }
        else
        {
            player = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), findPlayerRange, LayerMask.GetMask("角色无敌状态"));
            if (player.Length != 0)
            {
                aimPoint = player[0].transform.position;
                return true;
            }
            return false;
        }
    }

    ///<summary>
    ///辅助画图
    ///</summary>
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, findPlayerRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(originPoint, patrolDistance);
    }

    ///<summary>
    ///获取随机点
    ///</summary>
    protected virtual Vector3 GetRandomPoint()
    {
        return new Vector3(Random.Range(originPoint.x - patrolDistance, originPoint.x + patrolDistance), originPoint.y, 0);
    }

    ///<summary>
    ///受伤
    ///</summary>
    public void Hurt()
    {
        isHited = true;
        anim.SetBool("isHited", true);
    }

    ///<summary>
    ///掉落物
    ///</summary>
    protected virtual void DropItem()
    {
        if (Items.Count > 0)
        {
            GameObject ItemDrop = Instantiate(Items[(int)Mathf.Round(Random.Range(0, Items.Count - 1))], r2d.position, Quaternion.identity);
        }
    }

    ///<summary>
    ///死亡动画
    ///</summary>
    protected override void EventDeath()
    {
        DropItem();
        base.EventDeath();
    }

    ///<summary>
    ///攻击
    ///</summary>
    protected void SetAttackTrue(int index)
    {
        if (!isDeath)
            pc2D[index].enabled = true;
    }
    ///<summary>
    ///攻击
    ///</summary>
    protected void SetAttackFalse(int index)
    {
        pc2D[index].enabled = false;
    }

    public void EndNotify()
    {

    }
}
