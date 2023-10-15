using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use to modify spawning enemy, can spawn boss instead of basic enemy
public abstract class EnemyFactory : MonoBehaviour
{
    [SerializeField] protected GameObject EnemyPattern;
    public abstract GameObject CreateEnemy(GameObject target);
}
