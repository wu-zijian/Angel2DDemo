using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///鼠标管理
///</summary>
public class MouseManager : Singleton<MouseManager>
{
    public Texture2D arrow;
    protected override void Awake()
    {
        base.Awake();
        Cursor.SetCursor(arrow, new Vector2(0, 0), CursorMode.Auto);
        DontDestroyOnLoad(gameObject);
    }
}
