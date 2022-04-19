using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///伤害数值
///</summary>
public class DamageNum : MonoBehaviour
{
    private TextMesh textMesh;
    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    public void EventEnter()
    {
        Destroy(transform.parent.gameObject);
    }

    public void SetText(int value)
    {
        textMesh.text = "" + value;
    }
}
