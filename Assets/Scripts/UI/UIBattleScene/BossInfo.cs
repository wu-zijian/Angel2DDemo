using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

///<summary>
///Boss血条等信息
///</summary>
public class BossInfo : MonoBehaviour
{
    Text text;
    Image healthSlider;
    void Start()
    {
        text = transform.GetChild(0).GetComponent<Text>();
        healthSlider = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        healthSlider.fillAmount = 0;
        if (GameManager.Instance != null && GameManager.Instance.boss != null)
        {
            text.text = "" + GameManager.Instance.boss.name;
        }
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.boss != null)
        {
            CharacterStats bossData = GameManager.Instance.boss.GetComponent<CharacterStats>();
            healthSlider.fillAmount = Mathf.Lerp(healthSlider.fillAmount, (float)bossData.CurrentHealth / bossData.MaxHealth, 0.05f);
        }
        if (GameManager.Instance.boss.GetComponent<EnemyStoneMan>().Life - GameManager.Instance.boss.GetComponent<EnemyStoneMan>().DeathCount <= 0)
        {
            Invoke("End", 3f);
            this.gameObject.SetActive(false);
        }
    }

    public void End()
    {
        UIManager.Instance.Vectory();
    }
}
