using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    //Use flyweigt pattern to optimize
    [SerializeField] protected EnemyData EnemyDataConfig;
    [SerializeField] protected GameObject Target;
    [SerializeField] protected float CurrentHealth;
    [SerializeField] protected float CurrentSpeed;
    [SerializeField] protected Animator AnimatorEnemy;
    [SerializeField] public ENEMY_TYPE EnemyType;
    public ATTACK_STAGE _attackStage;

    protected void Start()
    {
        
        CurrentSpeed = EnemyDataConfig.EnemyStats.Speed;
    }

    abstract protected void Attack();
    abstract protected void Rotate();
    abstract protected void Move();
    abstract public void TakeDamage(float health);
    
    public void Flip()
    {
        transform.localScale = transform.position.x < Target.transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }

    private void OnEnable()
    {
        CurrentHealth = EnemyDataConfig.EnemyStats.MaxHealth;
    }
    
    public void SetTarget(GameObject target)
    {
        Target = target;
    }
}
