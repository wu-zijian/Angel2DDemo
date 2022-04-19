using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///角色信息
///</summary>
[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("基本信息")]
    // 最大生命
    public int maxHealth;
    // 当前生命
    public int currentHealth;
    // 升级增加生命
    public int addHealth;
    // 基础防御
    public int baseDefence;
    // 升级增加防御
    public int addDefence;
    // 基础攻击
    public int baseAttack;
    // 升级增加攻击
    public int addAttack;

    [Header("等级")]
    // 当前等级
    public int currentLevel;
    // 最大等级
    public int maxLevel;
    // 当前经验
    public int currentExp;
    // 最大经验    
    public int levelUpExp;
    // 升级所需经验
    public int addLevelUpExp;
    // 死亡经验
    public int deathExp;
}
