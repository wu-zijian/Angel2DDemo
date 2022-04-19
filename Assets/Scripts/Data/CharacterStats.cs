using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///代理
///</summary>
public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO templateData;
    [HideInInspector] public CharacterData_SO characterData_SO;
    private void OnEnable()
    {
        if (characterData_SO == null && templateData != null)
        {
            characterData_SO = Instantiate(templateData);
        }
    }
    public int MaxHealth
    {
        get
        {
            if (characterData_SO != null)
                return characterData_SO.maxHealth + characterData_SO.addHealth * (CurrentLevel - 1);
            else
                return 0;
        }
        set
        {
            characterData_SO.maxHealth = value;
        }
    }

    public int CurrentHealth
    {
        get
        {
            if (characterData_SO != null)
                return characterData_SO.currentHealth;
            else
                return 0;
        }
        set
        {
            characterData_SO.currentHealth = value;
        }
    }

    public int BaseDefence
    {
        get
        {
            if (characterData_SO != null)
                return characterData_SO.baseDefence + characterData_SO.addDefence * (CurrentLevel - 1);
            else
                return 0;
        }
        set
        {
            characterData_SO.baseDefence = value;
        }
    }

    public int CurrentDefence
    {
        get
        {
            if (characterData_SO != null)
                return BaseDefence;
            else
                return 0;
        }
    }

    public int BaseAttack
    {
        get
        {
            if (characterData_SO != null)
                return characterData_SO.baseAttack + characterData_SO.addAttack * (CurrentLevel - 1);
            else
                return 0;
        }
        set
        {
            characterData_SO.baseAttack = value;
        }
    }

    public int CurrentAttack
    {
        get
        {
            if (characterData_SO != null)
                return BaseAttack;
            else
                return 0;
        }
    }

    public int CurrentLevel
    {
        get
        {
            if (characterData_SO != null)
                return characterData_SO.currentLevel;
            else
                return 0;
        }
        set
        {
            characterData_SO.currentLevel = value;
        }
    }

    public int MaxLevel
    {
        get
        {
            if (characterData_SO != null)
                return characterData_SO.maxLevel;
            else
                return 0;
        }
        set
        {
            characterData_SO.maxLevel = value;
        }
    }

    public int CurrentExp
    {
        get
        {
            if (characterData_SO != null)
                return characterData_SO.currentExp;
            else
                return 0;
        }
        set
        {
            characterData_SO.currentExp = value;
        }
    }

    public int LevelUpExp
    {
        get
        {
            if (characterData_SO != null)
                return characterData_SO.levelUpExp + characterData_SO.addLevelUpExp * (CurrentLevel - 1);
            else
                return 0;
        }
        set
        {
            characterData_SO.levelUpExp = value;
        }
    }

    public int DeathExp
    {
        get
        {
            if (characterData_SO != null)
                return characterData_SO.deathExp;
            else
                return 0;
        }
        set
        {
            characterData_SO.deathExp = value;
        }
    }

    ///<summary>
    ///受伤
    ///</summary>
    ///<param name="attacker">攻击者信息</param>
    public int TakeDamage(CharacterStats attacker)
    {
        int demage = Mathf.Max(attacker.CurrentAttack - CurrentDefence, 5);
        CurrentHealth = Mathf.Max(CurrentHealth - demage, 0);
        return demage;
    }

    ///<summary>
    ///受伤
    ///</summary>
    ///<param name="attacker">伤害值</param>
    public int TakeDamage(int attacker)
    {
        int demage = Mathf.Max(attacker - CurrentDefence, 5);
        CurrentHealth = Mathf.Max(CurrentHealth - demage, 0);
        return demage;
    }

    ///<summary>
    ///升级
    ///</summary>
    ///<param name="exp">经验值</param>
    public void UpdataExp(int exp)
    {
        CurrentExp += exp;
        while (true)
        {
            if (CurrentExp >= LevelUpExp)//升级
            {
                if (CurrentLevel < MaxLevel)//未到达最高级
                {
                    CurrentExp -= LevelUpExp;
                    CurrentLevel++;
                    CurrentHealth = MaxHealth;
                }
                else//到达最高级
                {
                    CurrentExp = LevelUpExp;
                    break;
                }
            }
            else
            {
                break;
            }
        }
    }
}
