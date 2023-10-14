using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon
{
    [SerializeField]private float _damage;
    [SerializeField]private float _attackSpeed;
    [SerializeField]private float _DPS;//Damage per second
    [SerializeField]private float _critDamage;
    [SerializeField]private float _range;
    [SerializeField]private float _knockBack;
    [SerializeField]private float _lifeSteal;
    [SerializeField]private float _specialEffects;
    [SerializeField]private float _basePrice;
    [SerializeField]private float _unlockedBy;
    [SerializeField]private String _id;

    public float Damage { get => _damage; set => _damage = value; }
    public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
    public float DPS { get => _DPS; set => _DPS = value; }
    public float CritDamage { get => _critDamage; set => _critDamage = value; }
    public float Range { get => _range; set => _range = value; }
    public float KnockBack { get => _knockBack; set => _knockBack = value; }
    public float LifeSteal { get => _lifeSteal; set => _lifeSteal = value; }
    public float SpecialEffects { get => _specialEffects; set => _specialEffects = value; }
    public float BasePrice { get => _basePrice; set => _basePrice = value; }
    public float UnlockedBy { get => _unlockedBy; set => _unlockedBy = value; }
    public String ID { get => _id; set => _id = value; }
}
