using CarterGames.Assets.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class WeaponMelee : WeaponBase
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Collider2D _colAttack;
    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private List<Collider2D> _result;
    [SerializeField] private Animator _weaponAnimator;
    [SerializeField] private GameObject Model;
    private GameObject _player;
    private Vector3 initWeaponPosition;

    protected override void Attack()
    {
        StartCoroutine(CheckFinishAttack());
    }

    public override void SetTargetForAttack(Transform target)
    {
        Target = target.gameObject;
        if (Target.activeSelf == false) Target = null;
    }

    public override void SetPlayerPosition(Transform player)
    {
        _player = player.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetStateAttacking(ATTACK_STAGE.START, true);
        initWeaponPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ColliderAttacking();
        Flip();
        Rotate();
        if (Target != null)
            AttackMachanism(Target.transform);


    }

    private void ColliderAttacking()
    {
        if (_colAttack != null)
            _colAttack.OverlapCollider(_contactFilter, _result);
        if (_result.Count > 0)
            foreach (Collider2D collision in _result)
            {
                EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
                if(enemy!=null)
                enemy.TakeDamage(WeaponDataConfig.WeaponConfig.Damage);
                GamePlayController.Instance.GetBulletFactory().CreateHitEffect(transform.position, HIT_EFFECT_TYPE.DAMAGE_EFFECT);
                GamePlayController.Instance.GetBulletFactory().ReturnObjectToPool(gameObject);
                AudioManager.instance.Play("Hit", GameData.Instance.GetVolumeAudioGame());

                DynamicTextManager.CreateText2D(collision.transform.position, WeaponDataConfig.WeaponConfig.Damage.ToString(), DynamicTextManager.defaultData);
            }
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

                PlayerAttackStage = ATTACK_STAGE.DELAY;
                SetStateAttacking(ATTACK_STAGE.DELAY, true);
                transform.position = Target.transform.position;
                break;
            case ATTACK_STAGE.DELAY:
                if (CheckIsAttack(PlayerAttackStage))
                {
                    AnimationClip clip = GetAnimationClip("Attack");
                    float attackAnimationTime = 1.0f / WeaponDataConfig.WeaponConfig.AttackSpeed;
                    WeaponAnimator.speed = clip.length / attackAnimationTime;
                    StartCoroutine(DelayAttack(Target.transform));
                }
                break;
            case ATTACK_STAGE.DURATION:
                PlayerAttackStage = ATTACK_STAGE.FINISHED;
                SetStateAttacking(ATTACK_STAGE.FINISHED, true);


                transform.position = initWeaponPosition;
                break;
            case ATTACK_STAGE.FINISHED:
                if (Target == null) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                PlayerAttackStage = ATTACK_STAGE.START;
                SetStateAttacking(ATTACK_STAGE.START, true);
                transform.position = initWeaponPosition;
                break;
        }
    }

    public void AttackMachanism(Transform target)
    {
        switch (base.PlayerAttackStage)
        {
            case ATTACK_STAGE.START:
                if (CheckIsAttack(PlayerAttackStage) && Target.activeSelf == true)
                {
                    if (Vector2.Distance(_player.transform.position, Target.transform.position) <= WeaponDataConfig.WeaponConfig.Range)
                    {

                        Flip();
                        if (Target != null)
                        {
                            WeaponAnimator.enabled = false;
                            MoveToEnemy(Target.transform);
                            base.PlayerAttackStage = ATTACK_STAGE.DELAY;
                        }
                    }
                }

                break;
            case ATTACK_STAGE.DELAY:
                if (CheckIsAttack(PlayerAttackStage))
                {
                    Debug.Log("Attack delay");
                    WeaponAnimator.enabled = true;
                    //AnimateAttack();
                    Attack();

                    //DamageEnemy();
                    base.PlayerAttackStage = ATTACK_STAGE.DURATION;
                }
                break;
            case ATTACK_STAGE.DURATION:
                if (CheckIsAttack(PlayerAttackStage) && WeaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    Debug.Log("DURATION");
                    WeaponAnimator.Play("Idle");
                    SetStateAttacking(ATTACK_STAGE.DURATION, false);
                    SetStateAttacking(ATTACK_STAGE.FINISHED, true);

                    base.PlayerAttackStage = ATTACK_STAGE.FINISHED;
                }
                break;
            case ATTACK_STAGE.FINISHED:
                EndAttack();
                base.PlayerAttackStage = ATTACK_STAGE.START;
                break;
        }
    }

    private IEnumerator DelayAttack(Transform target)
    {
        SetStateAttacking(ATTACK_STAGE.DELAY, false);
        _weaponAnimator.Play("Attack");

        yield return new WaitForSecondsRealtime(WeaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        PlayerAttackStage = ATTACK_STAGE.DURATION;
        SetStateAttacking(PlayerAttackStage, true);
        Debug.Log("Nooooo");
    }

    public void Flip()
    {
        if (Target != null)
            transform.localScale = transform.position.x < Target.transform.position.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        CheckDirectionRotate();
    }

    public void Rotate()
    {
        if (Target == null) return;
        Vector3 direction = Target.transform.position - transform.position;
        direction.z = 0;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (RotateDirectionX > 0) angle += 180f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
    }

    private IEnumerator CheckFinishAttack()
    {
        WeaponAnimator.speed = WeaponDataConfig.WeaponConfig.AttackSpeed;
        WeaponAnimator.Play("Attack");
        yield return new WaitUntil(() => WeaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        SetStateAttacking(ATTACK_STAGE.DELAY, false);
        SetStateAttacking(ATTACK_STAGE.DURATION, true);
    }

    public float RotateDirectionX = 1;
    public float RotateDirectionY = 1;
    public void CheckDirectionRotate()
    {
        if (Target != null)
        {
            RotateDirectionX = transform.position.x > Target.transform.position.x ? 1 : -1;
            RotateDirectionY = transform.position.y > Target.transform.position.y ? 1 : -1;
        }
    }

    private void MoveToEnemy(Transform target)
    {
        //Body.transform.DOLocalMove(Vector3.zero, 1f);
        Vector3 newTarget = target.position + new Vector3(GetHalfSizeXColliderEnemy() * RotateDirectionX, GetHalfSizeYColliderEnemy() * RotateDirectionY, 0f);
        float distance = Vector3.Distance(newTarget, Model.transform.position);
        //Model.transform.position = newTarget;
        //Model.transform.DOMove(newTarget, distance / 5f).OnComplete(() => SetStateAttacking(ATTACK_STAGE.DELAY, true));
        SetStateAttacking(ATTACK_STAGE.DELAY, true);
        SetStateAttacking(ATTACK_STAGE.START, false);
        Debug.Log("Start attack");
    }

    private void EndAttack()
    {
        Model.transform.DOLocalMove(Vector2.zero, 0.5f).OnComplete(() => { SetStateAttacking(ATTACK_STAGE.START, true); SetStateAttacking(ATTACK_STAGE.FINISHED, false); });
    }

    public float GetHalfSizeXColliderEnemy()
    {
        return Target.GetComponent<BoxCollider2D>().size.x / 2;
    }

    public float GetHalfSizeYColliderEnemy()
    {
        return Target.GetComponent<BoxCollider2D>().size.y / 2;
    }

    AnimationClip GetAnimationClip(string animationName)
    {
        // L?y AnimationClip d?a tr�n t�n
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
}
