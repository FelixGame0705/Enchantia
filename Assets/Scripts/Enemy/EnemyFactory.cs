using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use to modify spawning enemy, can spawn boss instead of basic enemy
public abstract class EnemyFactory : MonoBehaviour
{
    [SerializeField] protected GameObject EnemyPattern;
    public HashSet<GameObject> Enemies = new HashSet<GameObject>();
    [SerializeField] protected GameObject TargetForEnemy;
    protected bool isSpawned = false;
    public virtual GameObject CreateEnemy(GameObject target) { return null; }
    public virtual GameObject CreateEnemy(GameObject target, Vector2 position) { return null; }
    public abstract void ReturnEnemToPool(GameObject gameObject);
    public abstract List<GameObject> SpawnRandomEnemy(GameObject target);
    public virtual void SetTarget(GameObject target)
    {
        TargetForEnemy = target;
    }

    public virtual void SetIsSpawned(bool isSpawned)
    {
        this.isSpawned = isSpawned;
    }

    public virtual HashSet<GameObject> GetEnemies()
    {
        return Enemies;
    }
}
