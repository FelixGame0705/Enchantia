using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using UnityEngine;

public class GamePlayCurrentData : MonoBehaviour
{
    [Header("Base Information")]
    [SerializeField] private int _currentWave ;
    [SerializeField] private int _currentGold;
    [SerializeField] private int _totalGoldHarvest;
    [SerializeField] private int _totalReroll;
    [SerializeField] private int _totalBuy;
    [SerializeField] private int _totalSpend;
    [SerializeField] private int _totalRecycle;
    [SerializeField] private int _totalKill;
    [SerializeField] private float _timeSurvive;
    [SerializeField] private float _damageTaken;
    [Header("Character Inventory")]
    [SerializeField] private List<ItemImageController> _itemControllerList;
    [SerializeField] private List<ItemImageController> _weaponControllerList;
    [Header("Character In Game")]
    [SerializeField] private GameObject _characterClone;

    private void Awake()
    {
        _itemControllerList = new List<ItemImageController>();
        _weaponControllerList = new List<ItemImageController>();
    }

    public int CurrentWave{set { _currentWave = value; } get { return _currentWave; } }
    public int CurrentGold{set { _currentGold = value;} get { return _currentGold;}}
    public List<ItemImageController> ItemControllerList {get { return _itemControllerList; }  set { _itemControllerList = value;}}
    public List<ItemImageController> WeaponControllerList {get { return _weaponControllerList;} set { _weaponControllerList = value;}}
    public GameObject Character{get { return _characterClone;} set { _characterClone = value;}}
    public int TotalGoldHarvest{get { return _totalGoldHarvest;} set { _totalGoldHarvest += value; } }
    public int TotalKill {get { return _totalKill;} set {_totalKill += value; } }
    public int TotalBuy {get { return _totalBuy;} set { _totalBuy += value; } }
    public int TotalSpend {get { return _totalSpend;} set {_totalSpend += value; } }
    public int TotalRecycle {get { return _totalRecycle;} set {_totalRecycle = value;}}
    public int TotalReroll {get { return _totalReroll;} set {_totalReroll += value;}}
    public float TimeSurvive {get { return _timeSurvive;} set {_timeSurvive = value;}}
    public float DamageTaken {get {return _damageTaken;} set {_damageTaken += value;}}
}