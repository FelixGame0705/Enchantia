using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponRanged : WeaponBase
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;

    private GameObject _player;
    protected override void Attack()
    {

    }

    public override void SetTargetForAttack(Transform target)
    {
        Target = target.gameObject;
        if(Target.activeSelf == false) Target = null;
    }

    public override void SetPlayerPosition(Transform player)
    {
        _player = player.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetStateAttacking(ATTACK_STAGE.START, true);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        AttackMechanism();
    }

    public bool CanPerformAttack()
    {
        if (Target != null && Target.activeSelf && Vector3.Distance(_player.transform.position, Target.transform.position) <= WeaponDataConfig.WeaponConfig.Range) return true;
        return false;
    }

    public void AttackMechanism()
    {
        switch (PlayerAttackStage)
        {
            case ATTACK_STAGE.START:
                if (CheckIsAttack(PlayerAttackStage))
                {
                    if (CanPerformAttack())
                    {
                        PlayerAttackStage = ATTACK_STAGE.DELAY;
                        SetStateAttacking(ATTACK_STAGE.START, false);
                        SetStateAttacking(ATTACK_STAGE.DELAY, true);
                    }
                }
                break;
            case ATTACK_STAGE.DELAY:
                if (CheckIsAttack(PlayerAttackStage))
                {
                    AnimationClip clip = GetAnimationClip("Attack");
                    float attackAnimationTime = 1.0f / WeaponDataConfig.WeaponConfig.AttackSpeed;
                    WeaponAnimator.speed = clip.length / attackAnimationTime;
                    WeaponAnimator.Play("Attack");
                    StartCoroutine(DelayAttack(Target.transform));
                }
                break;
            case ATTACK_STAGE.DURATION:
                if (CheckIsAttack(PlayerAttackStage) && WeaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    Debug.Log("Induration");
                    GamePlayController.Instance.GetCharacterController().LifeSteal();
                    WeaponAnimator.Play("Idle");
                    WeaponAnimator.speed = 1;
                    PlayerAttackStage = ATTACK_STAGE.FINISHED;
                    SetStateAttacking(ATTACK_STAGE.FINISHED, true);
                    SetStateAttacking(ATTACK_STAGE.DURATION, false);
                }
                break;
            case ATTACK_STAGE.FINISHED:
                if (Target == null) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                PlayerAttackStage = ATTACK_STAGE.START;
                SetStateAttacking(ATTACK_STAGE.START, true);
                SetStateAttacking(ATTACK_STAGE.FINISHED, false);
                break;
        }
    }

    private float animationTime;
    private IEnumerator DelayAttack(Transform target)
    {
        
        SetStateAttacking(ATTACK_STAGE.DELAY, false);
        SetStateAttacking(ATTACK_STAGE.DURATION, true);
        
        yield return new WaitUntil(() => WeaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        
        if (target != null) GamePlayController.Instance.GetBulletFactory().CreateBulletBaseOnPool(GetID(),target.position - _firePoint.position, WeaponDataConfig.WeaponConfig.Range, _firePoint.position, DealWithDamage());
        //SetStateAttacking(ATTACK_STAGE.DURATION, true);
        PlayerAttackStage = ATTACK_STAGE.DURATION;
        
        Debug.Log("Nooooo");
    }

    private float DealWithDamage()
    {
        if(Random.value <= DealWithCritChance())
        {
            return DealWitnCritDamage();
        }
        return WeaponDataConfig.WeaponConfig.Damage + GamePlayController.Instance.GetCharacterController().CharacterModStats.RangedDamage.Value;
    }

    public void SpawnBullet()
    {
        //GameObject bullet = 
    }

    private AnimationClip GetAnimationClip(string animationName)
    {
        // L?y AnimationClip d?a trên tên
        AnimationClip[] clips = WeaponAnimator.runtimeAnimatorController.animationClips;

        foreach (var animClip in clips)
        {
            if (animClip.name == animationName)
            {
                return animClip;
            }
        }

        return null;
    }

    public GameObject GetBulletPrefab()
    {
        return _bulletPrefab;
    }
}
