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
    virtual public void CreateBoss(Transform target) { }
    public virtual GameObject CreateEnemy(GameObject target) { return null; }
    public virtual GameObject CreateEnemy(GameObject target, Vector2 position) { return null; }
    public virtual GameObject CreateEnemyBaseOnPool(GameObject target, Vector2 position) { return null; }
    public virtual void ReturnSignalToPool(GameObject gameObject) { }
    public virtual void SetEnemyModelPool() { }
    public virtual void SetBossModelPool() { }
    public virtual void ReturnEnemToPool(GameObject gameObject) { }
    public virtual void SetTarget(GameObject target)
    {
        TargetForEnemy = target;
    }

    public virtual GameObject GetTarget()
    {
        return TargetForEnemy;
    }

    public virtual void SetIsSpawned(bool isSpawned)
    {
        this.isSpawned = isSpawned;
    }

    public virtual HashSet<GameObject> GetEnemies()
    {
        return Enemies;
    }

    public virtual void ResetEnemiesPool() { }
    public virtual void ResetBossesPool() { }
    public virtual void SetCurrentWave(int currentWave)
    {
    }

    public virtual void SetTimeAppearEnemies()
    {
    }

    public virtual WaveGameData GetWaveGameData() { return null; }

    public virtual List<WaveGameData> WaveGameDataList(){ return null;}
}
