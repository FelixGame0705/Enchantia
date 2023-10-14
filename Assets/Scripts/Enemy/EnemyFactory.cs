using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFactory : MonoBehaviour
{
    [SerializeField] protected GameObject EnemyPattern;
    public abstract void CreateEnemy();
}
