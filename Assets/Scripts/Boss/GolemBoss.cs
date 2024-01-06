using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class GolemBoss : EnemyBase
{
    [SerializeField] private GameObject bulletRockPool;
    [SerializeField] private GameObject bigBulletRockPool;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bigBulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int numberRockToChangeSkill = 15;
    private float _delayCounter;
    private float _delayTime = 2f;
    private int countNumberRock = 0;
    private ATTACK_STAGE _stage = ATTACK_STAGE.START;
    protected void Start()
    {
        CurrentHealth = 200f;
        SpawnBulletRockPool();
        SpawnBigBulletRockPool();
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

    protected override void Attack()
    {
    }

    protected override void Move()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) > EnemyDataConfig.EnemyStats.RangeAttack && _stage != ATTACK_STAGE.DURATION)
        {
            AnimatorEnemy.Play("Run");
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, EnemyDataConfig.EnemyStats.Speed * Time.deltaTime);
        }
    }

    protected override void Rotate()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Update()
    {
        Move();
        Flip();
        if (Vector3.Distance(gameObject.transform.position, Target.transform.position) <= EnemyDataConfig.EnemyStats.RangeAttack || _stage != ATTACK_STAGE.START)
        {
            AttackMechanism();
        }
    }

    private void AttackMechanism()
    {
        switch (_stage)
        {
            case ATTACK_STAGE.START:
                _stage = ATTACK_STAGE.DELAY;
                break;
            case ATTACK_STAGE.DELAY:
                _delayCounter += Time.deltaTime;
                if (_delayCounter >= _delayTime)
                {
                    _stage = ATTACK_STAGE.DURATION;
                    _delayCounter = 0f;
                }
                break;
            case ATTACK_STAGE.DURATION:
                if(countNumberRock <numberRockToChangeSkill)
                AnimatorEnemy.Play("Attack 2");
                else
                AnimatorEnemy.Play("Attack");
                _stage = ATTACK_STAGE.FINISHED;
                break;
            case ATTACK_STAGE.FINISHED:
                _stage = ATTACK_STAGE.START;
                break;
        }
    }

    public void SpawnBullet()
    {
        
        GameObject bulletRock = bulletRockPoolInstance.GetComponent<ObjectPool>().GetObjectFromPool();
        bulletRock.transform.position = firePoint.position;
        bulletRock.GetComponent<BulletRockEnemy>().Init(firePoint, Target.transform);
        countNumberRock += 1;
    }
    public void SpawnBigBullet()
    {
        
        GameObject bigBulletRock = bigBulletRockPoolInstance.GetComponent<ObjectPool>().GetObjectFromPool();
        bigBulletRock.transform.position = firePoint.position;
        bigBulletRock.GetComponent<BigBulletRock>().Init(firePoint, Target.transform);
        bigBulletRock.GetComponent<BigBulletRock>().golemBoss = this;
    }

    public GameObject bulletRockPoolInstance;
    private void SpawnBulletRockPool()
    {
        bulletRockPoolInstance = Instantiate(bulletRockPool);
        bulletRockPoolInstance.GetComponent<ObjectPool>().objectPrefab = bulletPrefab;
    }

    public GameObject bigBulletRockPoolInstance;
    private void SpawnBigBulletRockPool()
    {
        bigBulletRockPoolInstance = Instantiate(bigBulletRockPool);
        bigBulletRockPoolInstance.GetComponent<ObjectPool>().objectPrefab = bigBulletPrefab;
    }

    public override void Flip()
    {
        transform.localScale = transform.position.x < Target.transform.position.x ? new Vector3(-1* Mathf.Abs(transform.localScale.x), transform.localScale.y, 1) : new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }
}
