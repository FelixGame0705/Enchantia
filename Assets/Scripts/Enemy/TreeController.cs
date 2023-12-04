using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : EnemyBase
{
    public override void TakeDamage(float health)
    {
        CurrentHealth -= health;
        Debug.Log("Current health: " + CurrentHealth);

        if (CurrentHealth <= 0)
        {
            GamePlayController.Instance.GetDroppedItemController().SpawnDroppedItem(DROPPED_ITEM_TYPE.FRUIT, new Vector2(transform.position.x, transform.position.y));
            GamePlayController.Instance.GetEnemyFactory().ReturnEnemToPool(gameObject);
        }
    }

    protected override void Attack()
    {
    }

    protected override void Move()
    {
    }

    protected override void Rotate()
    {
    }
}
