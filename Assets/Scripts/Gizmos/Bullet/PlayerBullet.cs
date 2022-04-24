using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///玩家子弹
///</summary>
public class PlayerBullet : Bullet
{
    PlayerControl player;
    public GameObject DamagerNum;
    private void Start()
    {
        player = master.GetComponent<PlayerControl>();
    }
    protected override void OnTriggerStay2D(Collider2D c2d)
    {
        base.OnTriggerStay2D(c2d);
        if (aim == LayerMask.NameToLayer("EnemyLayer"))
        {
            Enemy enemy = c2d.gameObject.GetComponent<Enemy>();
            if (enemy.isDeath) return;
            int demagerNum = enemy.characterStats.TakeDamage(player.characterStats);
            Instantiate(DamagerNum, enemy.transform.position, Quaternion.identity).transform.GetChild(0).GetComponent<DamageNum>().SetText(demagerNum);
            if (enemy.DeathCheck())
            {
                player.characterStats.UpdataExp(enemy.characterStats.DeathExp);
            }
            else
            {
                enemy.Hurt();
            }
        }
    }
}
