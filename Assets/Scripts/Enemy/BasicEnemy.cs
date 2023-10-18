using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
    protected override void Attack()
    {
    }

    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, EnemyDataConfig.EnemyStats.Speed * Time.deltaTime);
    }

    protected override void Rotate()
    {
    }

    public void SetTarget(GameObject target)
    {
        Target = target;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void TakeDamage(float health)
    {
        CurrentHealth -= health;
        Debug.Log("Current health: " + CurrentHealth);
        if(CurrentHealth <= 0)
        {
            GamePlayController.Instance.GetEnemyFactory().ReturnEnemToPool(gameObject);
        }
    }
}
