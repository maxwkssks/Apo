using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image[] HealthImages = new Image[3];
    public Image RepairSkill;
    public Image BombSkill;
    public Slider FuelSlider;

    public TextMeshProUGUI SkillCooldownNoticeText;

    private Dictionary<EnumTypes.PlayerSkill, TextMeshProUGUI> _coolDownTexts = new Dictionary<EnumTypes.PlayerSkill, TextMeshProUGUI>();

    private void Start()
    {
        _coolDownTexts[EnumTypes.PlayerSkill.Repair] = RepairSkill.GetComponentInChildren<TextMeshProUGUI>();
        _coolDownTexts[EnumTypes.PlayerSkill.Bomb] = BombSkill.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateHealth();
        UpdateSkills();
        UpdateFuel();
    }

    private void UpdateHealth()
    {
        int health = GameManager.Instance.GetPlayerCharacter().GetComponent<PlayerHPSystem>().Health;

        for (int i = 0; i < HealthImages.Length; i++)
        {
            HealthImages[i].gameObject.SetActive(i < health);
        }
    }

    private void UpdateSkills()
    {
        UpdateSkill(EnumTypes.PlayerSkill.Repair);
        UpdateSkill(EnumTypes.PlayerSkill.Bomb);
    }

    private void UpdateSkill(EnumTypes.PlayerSkill skill)
    {
        bool isCoolDown = GameManager.Instance.GetPlayerCharacter().Skills[skill].bIsCoolDown;
        float currentTime = GameManager.Instance.GetPlayerCharacter().Skills[skill].CurrentTime;

        _coolDownTexts[skill].gameObject.SetActive(isCoolDown);
        _coolDownTexts[skill].text = $"{Mathf.RoundToInt(currentTime)}";
    }

    private void UpdateFuel()
    {
        FuelSlider.GetComponent<Slider>().value = GameManager.Instance.GetPlayerCharacter().GetComponent<PlayerFuelSystem>().Fuel / 100f;
    }

    public void NoticeSkillCooldown(EnumTypes.PlayerSkill playerSkill)
    {
        StartCoroutine(NoticeText(playerSkill));
    }

    IEnumerator NoticeText(EnumTypes.PlayerSkill playerSkill)
    {
        SkillCooldownNoticeText.gameObject.SetActive(true);
        SkillCooldownNoticeText.text = $"{playerSkill.ToString()} Skill is Cooldown";

        yield return new WaitForSeconds(3);

        SkillCooldownNoticeText.gameObject.SetActive(false);
    }    
}
