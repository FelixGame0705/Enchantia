using System;
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
        mod_LifeSteal = new StatModifier(LifeSteal, StatModType.PercentAdd);
        mod_MeleeDamage = new StatModifier(MeleeDamage, StatModType.Flat);
        mod_RangedDamage = new StatModifier(RangedDamage, StatModType.Flat);
        mod_ElementalDamage = new StatModifier(ElementalDamage, StatModType.Flat);
        mod_Armor = new StatModifier(Armor, StatModType.Flat);
        mod_EngineeringStat = new StatModifier(EngineeringStat, StatModType.PercentAdd);
        mod_RangeStat = new StatModifier(RangeStat, StatModType.PercentAdd);
        mod_Luck = new StatModifier(Luck, StatModType.PercentAdd);
        mod_Heal = new StatModifier(Luck, StatModType.PercentAdd);
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
    public CharacterStat MaxHP;
    public CharacterStat HPRegeneration;
    public CharacterStat LifeSteal;
    public CharacterStat Damage;
    public CharacterStat MeleeDamage;
    public CharacterStat RangedDamage;
    public CharacterStat ElementalDamage;
    public CharacterStat AttackSpeed;
    public CharacterStat CritChance;
    public CharacterStat Engineering;
    public CharacterStat Range;
    public CharacterStat Armor;
    public CharacterStat Dodge;
    public CharacterStat Speed;
    public CharacterStat Luck;
    public CharacterStat Harvesting;
    public CharacterStat HarvestRange;
}