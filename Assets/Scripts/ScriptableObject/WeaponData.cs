using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public Weapon WeaponConfig;
    [SerializeField] private string weaponId;
    [SerializeField] private string weaponName;
    [SerializeField] private ParticleSystem _particleAttack;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private WeaponData _nextWeapon;

    public ParticleSystem ParticleAttack { get => _particleAttack; set => _particleAttack = value; }
    public AudioClip AttackSound { get => _attackSound; set => _attackSound = value; }
    public WeaponData NextWeapon { get => _nextWeapon; set => _nextWeapon = value; }
    public string WeaponId { get => weaponId; set => weaponId = value; }
    public string WeaponName { get => weaponName; set => weaponName = value; }
}
