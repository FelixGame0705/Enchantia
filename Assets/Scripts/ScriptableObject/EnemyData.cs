using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyConfig", order = 1)]
public class EnemyData : ScriptableObject
{
    public Enemy EnemyStats;
    [SerializeField] private string _enemyName;
    [SerializeField] private ParticleSystem _particleAttack;
    [SerializeField] private ParticleSystem _particleHurt;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _hurtSound;

    public ParticleSystem ParticleAttack { get => _particleAttack; set => _particleAttack = value; }
    public AudioClip AttackSound { get => _attackSound; set => _attackSound = value; }
    public string EnemyName { get => _enemyName; set => _enemyName = value; }
    public ParticleSystem ParticleHurt { get => _particleHurt; set => _particleHurt = value; }
    public AudioClip HurtSound { get => _hurtSound; set => _hurtSound = value; }
}
