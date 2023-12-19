using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillWithKey
{
    public Skill skill;
    public KeyCode activationKey;
}
public class SkillManager : MonoBehaviour
{
    public List<SkillWithKey> skillsWithKeys;
    public List<SkillInstance> activeSkillInstances;

    void Start()
    {
        activeSkillInstances = new List<SkillInstance>();
        ConfigureSkills();
    }

    void Update()
    {
        HandleInput();
        UpdateCooldowns();
        HandleExecute();
    }
    void ConfigureSkills()
    {
        foreach (SkillWithKey skillWithKey in skillsWithKeys)
        {
            Skill it = Instantiate(skillWithKey.skill);
            SkillInstance instance = new SkillInstance(it);
            instance.Initialize(skillWithKey.skill);
            activeSkillInstances.Add(instance);
            // Gán phím kích ho?t t? SkillWithKey
            instance.activationKey = skillWithKey.activationKey;
        }
    }

    void HandleInput()
    {
        foreach (var skillInstance in activeSkillInstances)
        {
            if (Input.GetKeyDown(skillInstance.activationKey))
            {
                TryUseSkill(skillInstance);
            }
        }
    }

    void HandleExecute()
    {
        foreach (var skillInstance in activeSkillInstances)
        {
            ExecuteSkill(skillInstance);
        }
    }

    void TryUseSkill(SkillInstance skillInstance)
    {
        if (skillInstance.CanUse())
        {
            Debug.Log("Can use");
            skillInstance.PlugInSkill();
        }
    }

    void ExecuteSkill(SkillInstance skillInstance)
    {
        Debug.Log("Is use: " + skillInstance.isUsing);
        if (skillInstance.isUsing == true)
            skillInstance.Use(gameObject);
    }

    void UpdateCooldowns()
    {
        foreach (SkillInstance skillInstance in activeSkillInstances)
        {
            skillInstance.UpdateCooldown();
        }
    }

    void UpdateStateSkill()
    {

    }
}
