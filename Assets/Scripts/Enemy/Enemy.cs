using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Enemy
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _hpAddPerWave;
    [SerializeField] private float _speed;
    [SerializeField] private float _damagePerWave;
    [SerializeField] private float _materialsDropped;
    [SerializeField] private float _lootCrateDropRate;
    [SerializeField] private float _firstWaveAppearing;
    [SerializeField] private float _id;

    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float HpAddPerWave { get => _hpAddPerWave; set => _hpAddPerWave = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float DamagePerWave { get => _damagePerWave; set => _damagePerWave = value; }
    public float MaterialsDropped { get => _materialsDropped; set => _materialsDropped = value; }
    public float LootCrateDropRate { get => _lootCrateDropRate; set => _lootCrateDropRate = value; }
    public float FirstWaveAppearing { get => _firstWaveAppearing; set => _firstWaveAppearing = value; }
    public float Id { get => _id; set => _id = value; }
}
