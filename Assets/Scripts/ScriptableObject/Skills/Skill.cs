using UnityEngine;
public abstract class Skill : ScriptableObject
{
    public new string name;
    [TextAreaAttribute]
    public string description;
    public int damage;
    public float cooldown;
    public float timeInUsing;//thoi gian tac dung
    public Sprite icon;

    public virtual void Execute(GameObject player, ref bool isUsingSkill ,ref ATTACK_STAGE stage)
    {
    }
}