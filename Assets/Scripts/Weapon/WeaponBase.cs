using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] public WeaponData WeaponDataConfig;
    [SerializeField] protected Animator WeaponAnimator;
    [SerializeField] protected GameObject Target;
    protected bool [] IsStates = new bool[4];
    protected ATTACK_STAGE currentState;
    protected abstract void Attack();
    public abstract void SetPlayerPosition(Transform player);
    [SerializeField] protected int ID;
    public virtual int GetID()
    {
        return ID;
    }
    
    public virtual void SetID(int id)
    {
        ID = id;
    }
    protected virtual void Rotate() 
    {
        if (Target == null) return;
        Vector2 direction = Target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    public abstract void SetTargetForAttack(Transform target);
    public ATTACK_STAGE PlayerAttackStage;

    [SerializeField] private ParticleSystem _attackParticle = null;
    protected ParticleSystem AttackParticle => _attackParticle;

    [SerializeField] private AudioClip _attackSound = null;
    protected AudioClip AttackSound => _attackSound;

    public bool CheckIsAttack(ATTACK_STAGE attackStage)
    {
        return IsStates[(int)attackStage];
    }

    public void SetStateAttacking(ATTACK_STAGE currentState, bool isActive)
    {
        IsStates[(int)currentState] = isActive;
    }

    public void SetWeaponDataConfig(WeaponData config)
    {
        WeaponDataConfig = config;
    }

    public virtual float DealWithCritChance()
    {
        return WeaponDataConfig.WeaponConfig.CritChance;
    }

    public virtual float DealWitnCritDamage()
    {
        return WeaponDataConfig.WeaponConfig.Damage + WeaponDataConfig.WeaponConfig.CritDamage*WeaponDataConfig.WeaponConfig.Damage;
    }
}
