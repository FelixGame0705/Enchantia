using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/HealSkill")]
public class HealHP : Skill
{
    [SerializeField] private float hpHealPerSecond;
    [SerializeField] private float timeDuration = 5f;
    float timeDurationCoolDown = 0f;
    float timeUsingSkill = 0f;

    public override void Execute(GameObject player, ref bool isUsing, ref ATTACK_STAGE stage)
    {
        Debug.Log("Skill execute");
        switch (stage)
        {
            case ATTACK_STAGE.START:
                timeDurationCoolDown = timeDuration;
                stage = ATTACK_STAGE.DELAY;
                break;
            case ATTACK_STAGE.DELAY:
                stage = ATTACK_STAGE.DURATION;
                break;
            case ATTACK_STAGE.DURATION:
                timeDurationCoolDown += Time.deltaTime;
                timeUsingSkill += Time.deltaTime;
                if(timeDurationCoolDown >= 1f)
                {
                    player.GetComponent<CharacterController>().AddCurrentHealth(1);
                    Debug.Log("Heal + 1");
                    timeDurationCoolDown = 0;
                }
                if (timeUsingSkill >= timeDuration)
                {
                    stage = ATTACK_STAGE.FINISHED;
                    timeUsingSkill = 0f;
                }
                break;
            case ATTACK_STAGE.FINISHED:
                isUsing = false;
                stage = ATTACK_STAGE.START;
                break;
        }
    }
}
