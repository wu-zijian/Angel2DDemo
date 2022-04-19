using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    protected Character parent;
    public float hurtForce = 3f;
    private void Start()
    {
        parent = transform.parent.gameObject.GetComponent<Enemy>();
    }
    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))//判断角色
        {
            PlayerControl player = other.gameObject.GetComponent<PlayerControl>();
            player.characterStats.TakeDamage(player.characterStats);
            float hurtDirection = Mathf.Sign(other.gameObject.transform.position.x - parent.transform.position.x);
            player.r2d.AddForce(new Vector2(hurtDirection * hurtForce, 1f), ForceMode2D.Impulse);
            player.Hurt();
        }
    }
}
