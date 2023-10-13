using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] public WeaponData weaponData;

    protected abstract void Attack();
    protected abstract void Rotate();

    [SerializeField] private ParticleSystem _attackParticle = null;
    protected ParticleSystem AttackParticle => _attackParticle;

    [SerializeField] private AudioClip _attackSound = null;
    protected AudioClip AttackSound => _attackSound;
}
