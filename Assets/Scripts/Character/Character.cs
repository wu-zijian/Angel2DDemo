using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D r2d;//刚体
    protected CapsuleCollider2D cc2d;//身体碰撞体
    protected BoxCollider2D bc2d;// 脚碰撞体
    protected Animator anim;//动画
    [HideInInspector] public CharacterStats characterStats;// 角色数据

    [Header("移动参数")]
    public float speed = 4f;//速度

    [Header("跳跃参数")]
    public float jumpFore = 6f;
    public int jumpTopCount = 2;//最高跳跃次数
    public int jumpCount = 2;

    [Header("状态")]
    public bool isOnGround;//地面判断
    public bool isOnOWG;//单向地面判断
    public bool isJump;
    public bool isAttack;
    public bool isHited;
    public bool isDeath;

    protected virtual void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CapsuleCollider2D>();
        bc2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
    }

    protected virtual void FixedUpdate()
    {
        GroundCheck();
    }

    ///<summary>
    ///地面检测
    ///</summary>
    protected void GroundCheck()
    {
        isOnGround = bc2d.IsTouchingLayers(LayerMask.GetMask("GroundLayer")) ||
                        bc2d.IsTouchingLayers(LayerMask.GetMask("OneWayGroundLayer")) ||
                        bc2d.IsTouchingLayers(LayerMask.GetMask("GizmosLayer"));
        isOnOWG = bc2d.IsTouchingLayers(LayerMask.GetMask("OneWayGroundLayer"));
    }

    ///<summary>
    ///死亡检测
    ///</summary>
    public virtual bool DeathCheck()
    {
        if (characterStats != null && characterStats.CurrentHealth <= 0)
        {
            isDeath = true;
            anim.SetBool("isDeath", true);
            r2d.velocity = new Vector2(0f, 0f);
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            return true;
        }
        return false;
    }

    ///<summary>
    ///死亡消失
    ///</summary>
    protected virtual void EventDeath()
    {
        Destroy(this.gameObject);
    }

    ///<summary>
    ///受击结束
    ///</summary>
    protected virtual void EventIsHited()
    {
        isHited = false;
        anim.SetBool("isHited", false);
    }
}
