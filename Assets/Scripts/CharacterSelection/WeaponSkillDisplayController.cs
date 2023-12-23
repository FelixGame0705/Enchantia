using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSkillDisplayController : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI weaponTitleText;
    [SerializeField] private TextMeshProUGUI weaponDescriptionText;

    [Header("Skill")]
    [SerializeField] private Image skillImage;
    [SerializeField] private TextMeshProUGUI skillTitleText;
    [SerializeField] private TextMeshProUGUI skillDescriptionText;

    [Header("Parameter Weapon/Skill")]
    [SerializeField] private ItemData weaponData;
    [SerializeField] private Skill skillData;

    private bool isFirstRender = false;

    public void Render()
    {
        weaponImage.sprite = weaponData.ItemImg;
        weaponTitleText.text = weaponData.ItemName;
        weaponDescriptionText.text = weaponData.ItemDescription;
        skillImage.sprite = skillData.icon;
        skillTitleText.text = skillData.name;
        skillDescriptionText.text = skillData.description;
        if(isFirstRender == false)
        {
            this.gameObject.SetActive(true);
            isFirstRender = true;
        }
    }

    public void LoadData(CharacterBaseInfo data)
    {
        weaponData = data.BaseWeapon;
        skillData = data.MainSkill;
        Render();
    }
}
