using CarterGames.Assets.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationEvent : MonoBehaviour
{
    [SerializeField] private WeaponMelee _weaponMelee;
    [SerializeField] private Collider2D _colAttack;

    public void ActivableFlagAttack()
    {
        _weaponMelee._canAttack = true;
    }

    public void DisableFlagAttack()
    {
        _weaponMelee._canAttack = false;
    }

    public void ActivableColliderAttack()
    {
        _colAttack.enabled = true;
    }
    public void DisableColliderAttack()
    {
        _colAttack.enabled = false;
    }

    private Dictionary<EnemyBase, bool> dictionaries;

    public void SetEnemiesBase(Dictionary<EnemyBase, bool> dictionaries)
    {
        this.dictionaries = dictionaries;
    }

    public void ActiveAttack()
    {
        foreach (EnemyBase enemy in new List<EnemyBase>(dictionaries.Keys))
        {
            dictionaries[enemy] = false;
        }
    }
     
    
}
