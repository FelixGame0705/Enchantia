using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class Item 
{
    [SerializeField] private String _name;
    [SerializeField] private int _maxHP;
    [SerializeField] private float _hpRegeneration;
    [SerializeField] private float _lifeSteal;
    [SerializeField] private float _rangedDamage;
    [SerializeField] private float _meleeDamage;
    [SerializeField] private float _elementalDamage;
    [SerializeField] private float _armor;
    [SerializeField] private float _engineeringStat;
    [SerializeField] private float _rangeStat;
    [SerializeField] private float _luck;
    [SerializeField] private float _heal;
    [SerializeField] private float _harvestRange;
    [SerializeField] private ITEM_TYPE TYPE = ITEM_TYPE.ITEM;
    [Header("Stats of weapon")]
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _level;
    [SerializeField] private GameObject _weaponBaseModel;

    public float MeleeDamage { get => _meleeDamage; set => _meleeDamage = value; }
    public float RangedDamage { get => _rangedDamage; set => _rangedDamage = value; }
    public float ElementalDamage { get => _elementalDamage; set => _elementalDamage = value; }
    public float Armor { get => _armor; set => _armor = value; }
    public float EngineeringStat { get => _engineeringStat; set => _engineeringStat = value; }
    public float RangeStat { get => _rangeStat; set => _rangeStat = value; }
    public float Luck { get => _luck; set => _luck = value; }
    public float Heal { get => _heal; set => _heal = value; }

    
    public ITEM_TYPE TYPE1 { get => TYPE; set => TYPE = value; }
    public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
    public int Level { get => _level; set => _level = value; }
    public GameObject WeaponBaseModel { get => _weaponBaseModel; set => _weaponBaseModel = value; }
    public string Name { get => _name; set => _name = value; }
    public int MaxHP { get => _maxHP; set => _maxHP = value; }
    public float HpRegeneration { get => _hpRegeneration; set => _hpRegeneration = value; }
    public float LifeSteal { get => _lifeSteal; set => _lifeSteal = value; }
    public float HarvestRange { get => _harvestRange; set => _harvestRange = value; }

    StatModifier mod_MeleeDamage, mod_RangedDamage,
        mod_ElementalDamage, mod_Armor,
        mod_EngineeringStat, mod_RangeStat,
        mod_Luck, mod_Heal,
        mod_MaxHP, mod_HpRegeneration,
        mod_LifeSteal, mod_HarvestRange;
    public void Equip(Character_Mod c)
    {
        //Store modifier before add to stat
        mod_MaxHP = new StatModifier(MaxHP, StatModType.Flat);
        mod_HpRegeneration = new StatModifier(HpRegeneration, StatModType.Flat);
        mod_LifeSteal = new StatModifier(LifeSteal, StatModType.Flat);
        mod_MeleeDamage = new StatModifier(MeleeDamage, StatModType.Flat);
        mod_RangedDamage = new StatModifier(RangedDamage, StatModType.Flat);
        mod_ElementalDamage = new StatModifier(ElementalDamage, StatModType.Flat);
        mod_Armor = new StatModifier(Armor, StatModType.Flat);
        mod_EngineeringStat = new StatModifier(EngineeringStat, StatModType.Flat);
        mod_RangeStat = new StatModifier(RangeStat, StatModType.Flat);
        mod_Luck = new StatModifier(Luck, StatModType.Flat);
        mod_Heal = new StatModifier(Luck, StatModType.Flat);
        mod_HarvestRange = new StatModifier(HarvestRange, StatModType.Flat);

        c.MaxHP.AddModifier(mod_MaxHP);
        c.HPRegeneration.AddModifier(mod_HpRegeneration);
        c.LifeSteal.AddModifier(mod_LifeSteal);
        c.MeleeDamage.AddModifier(mod_MeleeDamage);
        c.RangedDamage.AddModifier(mod_RangedDamage);
        c.ElementalDamage.AddModifier(mod_ElementalDamage);
        c.Armor.AddModifier(mod_Armor);
        c.Engineering.AddModifier(mod_EngineeringStat);
        c.Range.AddModifier(mod_RangeStat);
        c.Luck.AddModifier(mod_Luck);
        c.HarvestRange.AddModifier(mod_HarvestRange);
        //c.Strength.AddModifier(mod2);
    }

    public void Unequip(Character_Mod c)
    {
        //c.Strength.RemoveAllModifiersFromSource(this);
    }
}

[Serializable]
public class Character_Mod
{
    [Tooltip("Flat")]
    public CharacterStat MaxHP;
    [Tooltip("Flat")]
    public CharacterStat HPRegeneration;
    [Tooltip("Percent 0.01 = 1% follow 0.01")]
    public CharacterStat LifeSteal;
    [Tooltip("Flat")]
    public CharacterStat Damage;
    [Tooltip("Flat")]
    public CharacterStat MeleeDamage;
    [Tooltip("Flat")]
    public CharacterStat RangedDamage;
    [Tooltip("Flat")]
    public CharacterStat ElementalDamage;
    [Tooltip("Flat")]
    public CharacterStat AttackSpeed;
    [Tooltip("Percent 0.01 = 1% follow 0.01")]
    public CharacterStat CritChance;
    [Tooltip("Percent 0.01 = 1% follow 0.01")]
    public CharacterStat Engineering;
    [Tooltip("Flat")]
    public CharacterStat Range;
    [Tooltip("Flat")]
    public CharacterStat Armor;
    [Tooltip("Percent 0.01 = 1% follow 0.01")]
    public CharacterStat Dodge;
    [Tooltip("Flat")]
    public CharacterStat Speed;
    [Tooltip("Percent 0.01 = 1% follow 0.01")]
    public CharacterStat Luck;
    [Tooltip("Percent 0.01 = 1% follow 0.01")]
    public CharacterStat Harvesting;
    [Tooltip("Flat")]
    public CharacterStat HarvestRange;

    public Dictionary<string, CharacterStat> GetProperties(){
        var probList = new Dictionary<string, CharacterStat>();
        probList.Add("Max HP", MaxHP);
        probList.Add("HP Regenerate", HPRegeneration);
        probList.Add("Life Steal", LifeSteal);
        probList.Add("Damage", Damage);
        probList.Add("Melee Damage", MeleeDamage);
        probList.Add("Range Damage", RangedDamage);
        probList.Add("Element Damage", ElementalDamage);
        probList.Add("Attack Speed", AttackSpeed);
        probList.Add("Crit Rate", CritChance);
        probList.Add("Engineering", Engineering);
        probList.Add("Range", Range);
        probList.Add("Armor", Armor);
        probList.Add("Dodge", Dodge);
        probList.Add("Speed", Speed);
        probList.Add("Luck", Luck);
        probList.Add("Harvesting", Harvesting);
        probList.Add("Harvest Range", HarvestRange);
        return probList;
    }
}