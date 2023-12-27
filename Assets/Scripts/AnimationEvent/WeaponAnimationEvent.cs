using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationEvent : MonoBehaviour
{
    [SerializeField] private Collider2D _colAttack;

    public void ActivableColliderAttack()
    {
        _colAttack.enabled = true;
    }
    public void DisableColliderAttack()
    {
        _colAttack.enabled = false;
    }
}
