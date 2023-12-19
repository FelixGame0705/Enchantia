using UnityEngine;

[System.Serializable]
public class SkillInstance
{
    public Skill skill;
    public float cooldownTimer;
    public float timeInUsing;
    public KeyCode activationKey;

    public ATTACK_STAGE stage;

    public SkillInstance(Skill skill)
    {
        this.skill = skill;
    }
    public virtual void Initialize(Skill baseSkill)
    {
        skill = baseSkill;
        cooldownTimer = 0f;
    }

    public virtual bool CanUse()
    {
        return cooldownTimer <= 0f;
    }

    public bool isUsing = false;

    public virtual void PlugInSkill()
    {
        if (CanUse())
        {
            isUsing = true;
        }
        else
        {
            Debug.Log("Skill is on cooldown!");
        }
    }
    public virtual void Use(GameObject target)
    {
            skill.Execute(target, ref isUsing, ref stage);
            cooldownTimer = skill.cooldown;
        
    }

    public virtual void UpdateCooldown()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public virtual void LogicSkill()
    {
        switch (stage)
        {
            case ATTACK_STAGE.START:
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