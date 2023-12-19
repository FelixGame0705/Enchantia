using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/FireBall")]
[Serializable]
public class FireBall : Skill
{
    public override void Execute(GameObject player, ref bool isUsing, ref ATTACK_STAGE stage)
    {
        Debug.Log("Skill execute");
        switch (stage)
        {
            case ATTACK_STAGE.START:
                stage = ATTACK_STAGE.DELAY;
                break;
            case ATTACK_STAGE.DELAY:
                break;
            case ATTACK_STAGE.DURATION:
                break;
            case ATTACK_STAGE.FINISHED:
                break;
        }
    }
}
