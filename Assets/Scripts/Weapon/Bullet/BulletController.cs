using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private BulletData _bulletData;
    [SerializeField] private LayerMask _layerMask;
    private float _distance;
    private Vector3 _direction;
    private float _damage;
    private bool isMeleeWeapon;

    public bool IsMeleeWeapon { get => isMeleeWeapon; set => isMeleeWeapon = value; }

    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        transform.position += _direction.normalized * _bulletData.BulletConfig.Speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
        transform.eulerAngles = new Vector3(0,0,Utils.Instance.GetAnglesFromVector(direction));
        StartCoroutine(DelayReturnPool());
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void SetDistance(float distance)
    {
        _distance = distance;
    }

    IEnumerator DelayReturnPool()
    {
        yield return new WaitForSeconds(_distance/_bulletData.BulletConfig.Speed);
        GamePlayController.Instance.GetBulletFactory().ReturnObjectToPool(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Damage " + collision.gameObject.layer);
        if (collision.gameObject.layer == 6)
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(_damage);
            Debug.Log("Damage " + _damage);
            GamePlayController.Instance.GetBulletFactory().CreateHitEffect(transform.position, HIT_EFFECT_TYPE.DAMAGE_EFFECT);
            GamePlayController.Instance.GetBulletFactory().ReturnObjectToPool(gameObject);

            DynamicTextManager.CreateText2D(collision.transform.position, _damage.ToString(), DynamicTextManager.defaultData);
        }
    }
}
