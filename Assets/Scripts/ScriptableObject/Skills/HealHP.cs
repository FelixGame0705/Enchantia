using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/HealSkill")]
public class HealHP : Skill
{
    [SerializeField] private float hpHealPerSecond;
    [SerializeField] private float cooldownTime = 5f;
    [SerializeField] private float timeEffectSkill = 3f;
    [SerializeField] private GameObject _healEffect;
    [SerializeField] CharacterController _characterController;
    float timeDurationCoolDown = 0f;
    float timeUsingSkill = 0f;
    float timeCountdownHealEachSecond = 0f;
    GameObject _vfxInstance;

    public override void Execute(GameObject player, ref bool isUsing, ref ATTACK_STAGE stage)
    {
        Debug.Log("Skill execute");
        switch (stage)
        {
            case ATTACK_STAGE.START:
                _characterController = player.GetComponent<CharacterController>();
                if (CheckFullHp() == true)
                {
                    isUsing = false;
                    return;
                }
                timeDurationCoolDown = cooldownTime;
                _vfxInstance = Instantiate(_healEffect, player.transform, false);
                stage = ATTACK_STAGE.DELAY;
                break;
            case ATTACK_STAGE.DELAY:
                stage = ATTACK_STAGE.DURATION;
                break;
            case ATTACK_STAGE.DURATION:
                
                timeUsingSkill += Time.deltaTime;
                timeCountdownHealEachSecond -= Time.deltaTime;
                if(IsInTimeUsing() && CheckFullHp() == false && timeCountdownHealEachSecond <= 0f)
                {
                    _characterController.AddCurrentHealth(1);
                    timeCountdownHealEachSecond = 1f;
                    Debug.Log("Heal + 1");
                    timeDurationCoolDown = 0;
                }
                if (timeUsingSkill >= timeEffectSkill)
                {
                    timeDurationCoolDown += Time.deltaTime;
                    if (timeDurationCoolDown >= 5f)
                    {
                        timeUsingSkill = 0;
                        stage = ATTACK_STAGE.FINISHED;
                    }
                }
                break;
            case ATTACK_STAGE.FINISHED:
                isUsing = false;
                Destroy(_vfxInstance);
                stage = ATTACK_STAGE.START;
                break;
        }
    }

    private bool CheckFullHp()
    {
        if(_characterController.GetCurrentHealth() >= _characterController.CharacterModStats.MaxHP.Value)
        {
            return true;
        }
        return false;
    }

    private bool IsInTimeUsing()
    {
        if(timeUsingSkill < timeEffectSkill)
        {
            return true;
        }
        return false;
    }
}
