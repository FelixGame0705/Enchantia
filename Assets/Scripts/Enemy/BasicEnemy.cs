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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
