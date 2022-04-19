using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///关卡信息
///</summary>
[CreateAssetMenu(fileName = "New GameLevel", menuName = "GameLevel/GameLevel")]
public class GameLevel : ScriptableObject
{
    // 传送点
    public Vector3[] points;
    public string sceneName;
}
