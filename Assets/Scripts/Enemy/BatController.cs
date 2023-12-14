using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using BehaviorTree;

public class BatController : EnemyBase
{
    [SerializeField] LayerMask _attackPlayer;
    private float _delayCounter = 0f;

    private void Update()
    {
        Flip();
        Move();
        AttackMechanism();
    }
    protected virtual void AttackMechanism()
    {
        switch (_attackStage)
        {
            case ATTACK_STAGE.START:
                //MoveToPlayer();
                if(Vector2.Distance(transform.position, Target.transform.position) > 8f) return;
               
                _attackStage = ATTACK_STAGE.DELAY;
                
                break;
            case ATTACK_STAGE.DELAY:
                UpdateCurrentPosition();
                UpdateDestination();
                UpdateDirection(previousTarget);
                _delayCounter += Time.deltaTime;
                if (_delayCounter >= 2f)
                {
                    AnimatorEnemy.Play("Attack");
                    _attackStage = ATTACK_STAGE.DURATION;
                    _delayCounter = 0f;
                }
                break;
            case ATTACK_STAGE.DURATION:
                float distanceToTarget = 10f; // Đặt khoảng cách mong muốn
                
                LerpToTarget(distanceToTarget);

                if (Vector2.Distance(transform.position, finalDes) < 1f)
                {
                    if(Vector2.Distance(transform.position, Target.transform.position) < 2f)
                    Attack();
                    _attackStage = ATTACK_STAGE.FINISHED;
                }
                break;
            case ATTACK_STAGE.FINISHED:
                FinishedAttack();
                _attackStage = ATTACK_STAGE.START;
                break;
        }
    }

    protected override void Attack()
    {
        StartCoroutine(CheckFinishedAttack());
    }

    protected override void Move()
    {
        if(Vector2.Distance(transform.position, Target.transform.position) > EnemyDataConfig.EnemyStats.RangeAttack)
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, EnemyDataConfig.EnemyStats.Speed * Time.deltaTime);
    }

    protected override void Rotate()
    {
    }

    private Vector2 previousTarget;
    private void UpdateDestination()
    {
        previousTarget = new Vector2(Target.transform.position.x, Target.transform.position.y);
    }

    private Vector2 currentPos;

    private void UpdateCurrentPosition()
    {
        currentPos = new Vector2(transform.position.x, transform.position.y);
    }

    private Vector2 direction;
    private void UpdateDirection(Vector2 targetPosition)
    {
        direction = (targetPosition - currentPos).normalized;
        Debug.Log("Direction: " + direction);
    }

    private Vector2 finalDes;
    private void LerpToTarget(float distance)
    {

        // Xác định vị trí mới dựa trên hướng và khoảng cách
        finalDes = currentPos + direction * distance;
        
        // Lerp tới vị trí mới
        transform.position = Vector2.MoveTowards(transform.position, finalDes, 10f * Time.deltaTime);
    }

    public override void TakeDamage(float health)
    {
        CurrentHealth -= health;
        Debug.Log("Current health: " + CurrentHealth);

        GamePlayController.Instance.GetBulletFactory().CreateHitEffect(transform.position, HIT_EFFECT_TYPE.BLOOD_EFFECT);

        if (CurrentHealth <= 0)
        {
            //GamePlayController.Instance.GetCurrencyController().SpawnGold(new Vector2(transform.position.x, transform.position.y));
            GamePlayController.Instance.GetDroppedItemController().SpawnDroppedItem(DROPPED_ITEM_TYPE.GOLD, new Vector2(transform.position.x, transform.position.y));
            GamePlayController.Instance.GetEnemyFactory().ReturnEnemToPool(gameObject);
        }
    }

    private void FinishedAttack()
    {
        AnimatorEnemy.Play("idle");
        _attackStage = ATTACK_STAGE.FINISHED;
    }

    private IEnumerator CheckFinishedAttack()
    {
        AnimatorEnemy.speed = 0.5f;
        AnimatorEnemy.Play("Attack");
        Target.GetComponent<CharacterController>().TakeDamage(EnemyDataConfig.EnemyStats.DamagePerWave);
        yield return new WaitUntil(() => AnimatorEnemy.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        _attackStage = ATTACK_STAGE.FINISHED;
    }
}
