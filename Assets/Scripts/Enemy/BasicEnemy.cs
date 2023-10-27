using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
    [SerializeField] private BoxCollider2D _attackCollider;
    protected override void Attack()
    {
        StartCoroutine(CheckFinishedAttack());
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
        Flip();
        if(Vector3.Distance(gameObject.transform.position, Target.transform.position) <= EnemyDataConfig.EnemyStats.RangeAttack)
        {
            AttackMechanism();
        }
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

    private bool CanPerformAttack()
    {
        if (!AnimatorEnemy.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return true;
        }
        return false;
    }

    protected virtual void AttackMechanism()
    {
        switch (_attackStage)
        {
            case ATTACK_STAGE.START:
                //MoveToPlayer();
                if (CanPerformAttack())
                    if (Vector2.Distance(transform.position, Target.transform.position) < 1f)
                    {
                        _attackStage = ATTACK_STAGE.DURATION;
                    }
                break;
            case ATTACK_STAGE.DURATION:
                if (CanPerformAttack())
                    Attack();
                break;
            case ATTACK_STAGE.FINISHED:
                if (CanPerformAttack())
                    FinishedAttack();
                break;
            case ATTACK_STAGE.END:
                _attackStage = ATTACK_STAGE.START;
                break;
        }
    }

    private void FinishedAttack()
    {
        AnimatorEnemy.Play("idle");
        _attackCollider.enabled = false;
        _attackStage = ATTACK_STAGE.END;
    }

    private IEnumerator CheckFinishedAttack()
    {
        AnimatorEnemy.speed = 0.5f;
        AnimatorEnemy.Play("Attack");
        Target.GetComponent<CharacterController>().TakeDamage(EnemyDataConfig.EnemyStats.DamagePerWave);
        //_playerUpDownController.TakeDamage(_enemyConfig.DamageAttack);
        _attackCollider.enabled = true;
        yield return new WaitUntil(() => AnimatorEnemy.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        _attackStage = ATTACK_STAGE.FINISHED;
    }
}
