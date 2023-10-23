using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    [SerializeField] private int _maxHP;
    [SerializeField] private float _hpRegeneration;
    [SerializeField] private float _lifeSteal;
    [SerializeField] private float _damage;
    [SerializeField] private float _meleeDamage;
    [SerializeField] private float _rangedDamage;
    [SerializeField] private float _elementalDamage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _critChance;
    [SerializeField] private float _engineering;
    [SerializeField] private float _range;
    [SerializeField] private float _armor;
    [SerializeField] private float _dodge;
    [SerializeField] private int _speed;
    [SerializeField] private float _luck;
    [SerializeField] private float havesting;
    public int MaxHP { get => _maxHP; set => SetHp(value); }
    public float HpRegeneration { get => _hpRegeneration; set => _hpRegeneration = value; }
    public float LifeSteal { get => _lifeSteal; set => _lifeSteal = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public float MeleeDamage { get => _meleeDamage; set => _meleeDamage = value; }
    public float RangedDamage { get => _rangedDamage; set => _rangedDamage = value; }
    public float ElementalDamage { get => _elementalDamage; set => _elementalDamage = value; }
    public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
    public float CritChance { get => _critChance; set => _critChance = value; }
    public float Engineering { get => _engineering; set => _engineering = value; }
    public float Range { get => _range; set => _range = value; }
    public float Armor { get => _armor; set => _armor = value; }
    public float Dodge { get => _dodge; set => _dodge = value; }
    public int Speed { get => _speed; set => _speed = value; }
    public float Luck { get => _luck; set => _luck = value; }
    public float Havesting { get => havesting; set => havesting = value; }

    private void SetHp(int hp)
    {
        if (hp <= 0)
            _maxHP = 1;
        else
        {
            _maxHP = hp;
        }
    }
}
