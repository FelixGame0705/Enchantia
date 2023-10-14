using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    //Use flyweigt pattern to optimize
    [SerializeField] protected EnemyData EnemyData;
    [SerializeField] protected GameObject Target;
    [SerializeField] protected float CurrentHealth;
    [SerializeField] protected float CurrentSpeed;

    protected void Start()
    {
        CurrentHealth = EnemyData.EnemyStats.MaxHealth;
        CurrentSpeed = EnemyData.EnemyStats.Speed;
    }

    abstract protected void Attack();
    abstract protected void Rotate();
    abstract protected void Move();
}
