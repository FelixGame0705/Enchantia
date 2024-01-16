using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    //Use flyweigt pattern to optimize
    [SerializeField] protected EnemyData EnemyDataConfig;
    [SerializeField] protected GameObject Target;
    [SerializeField] protected float CurrentHealth;
    [SerializeField] protected float CurrentSpeed;
    [SerializeField] protected float CurrentDamage;
    [SerializeField] protected Animator AnimatorEnemy;
    [SerializeField] public ENEMY_TYPE EnemyType;
    public bool isSurviveNextWave;
    public ATTACK_STAGE _attackStage;
    public Dictionary<int, bool> dic = new Dictionary<int, bool>();

    protected void Start()
    {
        
        CurrentSpeed = EnemyDataConfig.EnemyStats.Speed;
    }

    abstract protected void Attack();
    abstract protected void Rotate();
    abstract protected void Move();
    abstract public void TakeDamage(float health);
    
    public virtual void Flip()
    {
        transform.localScale = transform.position.x < Target.transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }

    private void OnEnable()
    {
        //CurrentHealth = EnemyDataConfig.EnemyStats.MaxHealth + EnemyDataConfig.EnemyStats.HealthIncrease;
    }
    
    public void SetTarget(GameObject target)
    {
        Target = target;
    }

    public virtual void SetEnemyConfigStats(int currentWave) { }
}
