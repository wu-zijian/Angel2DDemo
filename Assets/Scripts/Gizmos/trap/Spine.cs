using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///地刺
///</summary>
public class Spine : MonoBehaviour
{
    public int attack = 30;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))//判断角色
        {
            PlayerControl player = other.gameObject.GetComponent<PlayerControl>();
            player.characterStats.TakeDamage(attack);
            player.Hurt();
        }
    }
}
