using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkillController : MonoBehaviour
{
    [SerializeField] private Image skillImg;
    [SerializeField] private Image skillPanelCooldown;

    private float _timeCooldown;
    private float _currentTimeCooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        skillImg.sprite = GamePlayController.Instance.Character.GetComponent<CharacterBaseInfo>().MainSkill.icon;
        skillPanelCooldown.fillAmount = 0;
    }

    public void SetTimeCooldown(float timeCooldown)
    {
        _timeCooldown = timeCooldown;
        _currentTimeCooldown = _timeCooldown;
    }

    public void UpdateCooldownTime()
    {
        _currentTimeCooldown -= Time.deltaTime;
        UpdateTimeOnIcon();
    }

    public void UpdateTimeOnIcon()
    {
        skillPanelCooldown.fillAmount = _currentTimeCooldown/_timeCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
