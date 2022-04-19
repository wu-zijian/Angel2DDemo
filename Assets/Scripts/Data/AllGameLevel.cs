using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///所有关卡信息
///</summary>
[CreateAssetMenu(fileName = "New GameLevelList", menuName = "GameLevel/AllGameLevel")]
public class AllGameLevel : ScriptableObject
{
    public GameLevel[] gameLevels;
}
