using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//This class will contain basic info for showing in character selection, add this after adding character controller and skill manager
public class CharacterBaseInfo : MonoBehaviour
{
    [SerializeField] private Character_Mod characterStats;
    [SerializeField] private Skill mainSkill;
    // This will contain character head sprite
    [SerializeField] private Sprite characterSprite;
    //This will contain sprite character body
    [SerializeField] private Sprite characterBodySprite;
    [SerializeField] private ItemData baseWeapon;
    public void Load()
    {
        var characterController = GetComponent<CharacterController>();
        CharacterStats = characterController.GetCharacterData().Character_Mod;
        MainSkill = GetComponent<SkillManager>().skillsWithKeys[0] == null ? null: GetComponent<SkillManager>().skillsWithKeys[0].skill;
        BaseWeapon = characterController.GetCharacterData().FirstItems[0];
    }


    public Character_Mod CharacterStats { get => characterStats; set => characterStats = value; }
    public Skill MainSkill { get => mainSkill; set => mainSkill = value; }
    public Sprite CharacterSprite { get => characterSprite; set => characterSprite = value; }
    public ItemData BaseWeapon { get => baseWeapon; set => baseWeapon = value; }
    public Sprite CharacterBodySprite { get => characterBodySprite; set => characterBodySprite = value; }
}
