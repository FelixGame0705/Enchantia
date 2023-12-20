using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/EvasiveManeuvers")]
public class EvasiveManeuvers : Skill
{
    [SerializeField] private float _percentDodgeAddMultiplePercent;
    [SerializeField] private float _percentSpeedAddMultiplePercent;
    [SerializeField] private GameObject _skillEffect;
    [SerializeField] private float skillEffectDuration = 5f;
    [SerializeField] private CharacterController _characterController;
    private GameObject _vfxInstance;

    public override void Execute(GameObject player, ref bool isUsingSkill, ref ATTACK_STAGE stage)
    {
        switch (stage)
        {
            case ATTACK_STAGE.START:
                _characterController = player.GetComponent<CharacterController>();
                _vfxInstance = Instantiate(_skillEffect, player.transform);
                stage = ATTACK_STAGE.DELAY;
                break;
            case ATTACK_STAGE.DELAY:
                _characterController.CharacterModStats.Dodge.AddModifier(new StatModifier(_percentDodgeAddMultiplePercent, StatModType.Flat));
                _characterController.CharacterModStats.Speed.AddModifier(new StatModifier(_percentSpeedAddMultiplePercent, StatModType.PercentAdd));
                stage = ATTACK_STAGE.DURATION;
                break;
            case ATTACK_STAGE.DURATION:
                timeInUsing += Time.deltaTime;
                if(timeInUsing >= skillEffectDuration)
                {
                    timeInUsing = 0f;
                    stage = ATTACK_STAGE.FINISHED;
                }
                break;
            case ATTACK_STAGE.FINISHED:
                Destroy(_vfxInstance);
                _characterController.CharacterModStats.Dodge.AddModifier(new StatModifier(-_percentDodgeAddMultiplePercent, StatModType.Flat));
                _characterController.CharacterModStats.Speed.AddModifier(new StatModifier(_percentSpeedAddMultiplePercent, StatModType.PercentAdd));
                isUsingSkill = false;
                stage = ATTACK_STAGE.START;
                break;
        }
    }
}
