using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

///<summary>
///玩家信息
///</summary>
public class PlayerInfo : MonoBehaviour
{
    Text levelText;
    Image healthSlider;
    Image healthSliderCache;
    Text healthText;
    Image expSlider;
    void Start()
    {
        levelText = transform.GetChild(0).GetComponent<Text>();
        healthSliderCache = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        healthSlider = transform.GetChild(1).GetChild(1).GetComponent<Image>();
        healthText = transform.GetChild(1).GetChild(2).GetComponent<Text>();
        expSlider = transform.GetChild(2).GetChild(0).GetComponent<Image>();
        healthSliderCache.fillAmount = 1;
        healthSlider.fillAmount = 1;
        expSlider.fillAmount = 1;
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.player != null)
        {
            CharacterStats playerData = GameManager.Instance.player.GetComponent<CharacterStats>();
            levelText.text = "等级：" + playerData.CurrentLevel;

            healthSlider.fillAmount = (float)playerData.CurrentHealth / playerData.MaxHealth;
            healthText.text = "" + playerData.CurrentHealth + "/" + playerData.MaxHealth;
            if (healthSlider.fillAmount < healthSliderCache.fillAmount)
                healthSliderCache.fillAmount = Mathf.Lerp(healthSliderCache.fillAmount, healthSlider.fillAmount, 0.05f);
            else
                healthSliderCache.fillAmount = healthSlider.fillAmount;

            if (expSlider.fillAmount < (float)playerData.CurrentExp / playerData.LevelUpExp)
                expSlider.fillAmount = Mathf.Lerp(expSlider.fillAmount, (float)playerData.CurrentExp / playerData.LevelUpExp, 0.3f);
            else
                expSlider.fillAmount = (float)playerData.CurrentExp / playerData.LevelUpExp;
        }
    }
}
