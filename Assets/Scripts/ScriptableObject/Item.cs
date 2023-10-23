using System;
using UnityEngine;

[Serializable]
public class Item 
{
    [SerializeField] private float _rangedDamage;
    [SerializeField] private float _meleeDamage;
    [SerializeField] private float _elementalDamage;
    [SerializeField] private float _armor;
    [SerializeField] private float _engineeringStat;
    [SerializeField] private float _rangeStat;
    [SerializeField] private float _luck;
    [SerializeField] private float _heal;

    public float MeleeDamage { get => _meleeDamage; set => _meleeDamage = value; }
    public float RangedDamage { get => _rangedDamage; set => _rangedDamage = value; }
    public float ElementalDamage { get => _elementalDamage; set => _elementalDamage = value; }
    public float Armor { get => _armor; set => _armor = value; }
    public float EngineeringStat { get => _engineeringStat; set => _engineeringStat = value; }
    public float RangeStat { get => _rangeStat; set => _rangeStat = value; }
    public float Luck { get => _luck; set => _luck = value; }
    public float Heal { get => _heal; set => _heal = value; }
}
