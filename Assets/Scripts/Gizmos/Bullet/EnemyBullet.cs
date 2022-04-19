using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///敌人子弹
///</summary>
public class EnemyBullet : Bullet
{
    protected Enemy parent;
    public float hurtForce = 3f;
    private void Start()
    {
        parent = master.GetComponent<Enemy>();
    }
    protected override void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))//判断角色
        {
            base.OnTriggerStay2D(other);
            PlayerControl player = other.gameObject.GetComponent<PlayerControl>();
            float hurtDirection = other.gameObject.transform.position.x - parent.transform.position.x > 0 ? 1 : -1;
            player.characterStats.TakeDamage(player.characterStats);
            player.r2d.AddForce(new Vector2(hurtDirection * hurtForce, 1f), ForceMode2D.Impulse);
            player.Hurt();
        }
    }
}
