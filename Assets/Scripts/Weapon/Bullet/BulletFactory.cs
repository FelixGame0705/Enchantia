using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] protected GameObject BulletPattern;
    [SerializeField] protected GameObject BulletMeleePattern;
    [SerializeField] protected GameObject HitEffectPattern;
    [SerializeField] private ObjectPool _bulletPool = new ObjectPool();
    [SerializeField] private ObjectPool _bulletMeleePool = new ObjectPool();
    [SerializeField] private ObjectPool _hitEffectPool = new ObjectPool();
    private BulletController bulletController;
    public virtual GameObject CreateBullet(Vector3 target)
    {
        _bulletPool.objectPrefab = BulletPattern;
        GameObject bullet = _bulletPool.GetObjectFromPool();
        //enemy.transform.position = RandomPositionSpawn();
        //enemy.GetComponent<BasicEnemy>().SetTarget(target);
        bullet.GetComponent<BulletController>().SetDirection(target);
        return bullet;
    }

    public virtual GameObject CreateBullet(Vector3 direction, float distance, Vector3 positionBullet, float damage)
    {
        _bulletPool.objectPrefab = BulletPattern;
        GameObject bullet = _bulletPool.GetObjectFromPool();
        bullet.transform.position = positionBullet;
        bulletController = bullet.GetComponent<BulletController>();
        bulletController.SetDistance(distance);
        bulletController.SetDirection(direction);
        bulletController.SetDamage(damage);
        return bullet;
    }
    
    public virtual GameObject CreateBulletMelee(Vector3 direction, float distance, Vector3 positionBullet, float damage)
    {
        _bulletMeleePool.objectPrefab = BulletMeleePattern;
        GameObject bullet = _bulletMeleePool.GetObjectFromPool();
        bullet.transform.position = positionBullet;
        bulletController = bullet.GetComponent<BulletController>();
        bulletController.IsMeleeWeapon = true;
        bulletController.SetDistance(distance);
        bulletController.SetDirection(direction);
        bulletController.SetDamage(damage);
        return bullet;
    }
    
    public virtual GameObject CreateHitEffect(Vector3 initPosition)
    {
        _hitEffectPool.objectPrefab = HitEffectPattern;
        GameObject hitEffect = _hitEffectPool.GetObjectFromPool();
        hitEffect.transform.position = initPosition;
        var par = hitEffect.GetComponentInChildren<ParticleSystem>();
        if (par != null)
        {
            par.Play();
            StartCoroutine(DelayHitEffectReturnPool(hitEffect, par.main.startLifetime.constant));
        }
        return hitEffect;
    }

    public virtual void ReturnObjectToPool(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out BulletController bulletController))
        {
            if (bulletController.IsMeleeWeapon)
            {
                bulletController.IsMeleeWeapon = false;
                _bulletMeleePool.ReturnObjectToPool(gameObject);
            }
            else
            {
                _bulletPool.ReturnObjectToPool(gameObject);
            }
        }
    }
    
    private IEnumerator DelayHitEffectReturnPool(GameObject hitEff, float delay)
    {
        yield return new WaitForSeconds(delay);

        ReturnHitEffToPool(hitEff);
    }

    public virtual void ReturnHitEffToPool(GameObject gameObject)
    {
        _hitEffectPool.ReturnObjectToPool(gameObject);
    }
    
}
