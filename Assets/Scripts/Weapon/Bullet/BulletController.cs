using CarterGames.Assets.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private BulletData _bulletData;
    [SerializeField] private LayerMask _layerMask;
    protected float Damage;
    private float _distance;
    private Vector3 _direction;
    private bool isMeleeWeapon;

    public bool IsMeleeWeapon { get => isMeleeWeapon; set => isMeleeWeapon = value; }

    protected virtual void FixedUpdate()
    {
        transform.position += _direction.normalized * _bulletData.BulletConfig.Speed * Time.deltaTime;
    }

    public virtual void SetDirection(Vector3 direction)
    {
        _direction = direction;
        transform.eulerAngles = new Vector3(0,0,Utils.Instance.GetAnglesFromVector(direction));
        StartCoroutine(DelayReturnPool());
    }

    public virtual void SetDamage(float damage)
    {
        Damage = damage;
    }

    public virtual void SetDistance(float distance)
    {
        _distance = distance;
    }

    IEnumerator DelayReturnPool()
    {
        yield return new WaitForSeconds(_distance/_bulletData.BulletConfig.Speed);
        GamePlayController.Instance.GetBulletFactory().ReturnObjectToPool(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Damage " + collision.gameObject.layer);
        if (collision.gameObject.layer == 6)
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            if(enemy!=null)
            enemy.TakeDamage(Damage);
            Debug.Log("Damage " + Damage);
            GamePlayController.Instance.GetBulletFactory().CreateHitEffect(transform.position, HIT_EFFECT_TYPE.DAMAGE_EFFECT);
            GamePlayController.Instance.GetBulletFactory().ReturnObjectToPool(gameObject);
            AudioManager.instance.Play("Hit", GameData.Instance.GetVolumeAudioGame());

            DynamicTextManager.CreateText2D(collision.transform.position, Damage.ToString(), DynamicTextManager.defaultData);
        }
    }
}
