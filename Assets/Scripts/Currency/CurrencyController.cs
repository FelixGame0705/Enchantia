using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    [SerializeField] private GameObject _goldModel;
    //[SerializeField] private CurrencyUI _currencyUI;
    [SerializeField] private ObjectPool _goldPool;
    [SerializeField] private HashSet<GameObject> _golds = new HashSet<GameObject>();
    public void SpawnGold(Vector2 position)
    {
        GameObject gold = _goldPool.GetObjectFromPool();
        gold.transform.position = position;
        AddUniqueGold(gold);
    }

    public HashSet<GameObject> GetGolds()
    {
        return _golds;
    }

    public void AddGold(int amount)
    {
        WaveShopMainController.Instance.AddGoldValue(amount);
        //_currencyUI.AddGoldValue(amount);
    }

    public void AddUniqueGold(GameObject gold)
    {
        if (!_golds.Contains(gold)) _golds.Add(gold);
    }

    public void ReturnGoldToPool(GameObject gold)
    {
        _goldPool.ReturnObjectToPool(gold);
    }
}
