using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///鸟怪
///</summary>
public class EnemyBirdMonster : Enemy
{

    protected override void Start()
    {
        base.Start();
        pc2D = transform.GetChild(0).gameObject.GetComponents<PolygonCollider2D>();
    }

}
