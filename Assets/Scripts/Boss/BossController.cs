using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public EnemyData BossDataConfig;
    public Transform target;
    public Animator animator;
    [SerializeField] private BoxCollider2D _attackCollider;
    public BoxCollider2D GetAttackCollider()
    {
        return _attackCollider;
    }
    public bool TakeDamage()
    {
        return false;
    }
}
