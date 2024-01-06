using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveGameConfig", order = 1)]
public class WaveGameData : ScriptableObject
{
    [SerializeField] private List<EnemyConfig> _enemiesConfig;
    [SerializeField] private List<GameObject> _bossPrefab;
    [SerializeField] private float _timeWave;
    [SerializeField] private float _timeAppearEnemies;
    [SerializeField] private int _minEnemies;
    [SerializeField] private int _maxEnemies;
    [SerializeField] private float _sumRateAppear;

    private void OnValidate()
    {
        SumRateAppearOfEnemies();
    }

    private void SumRateAppearOfEnemies()
    {
        _sumRateAppear = 0f;
        foreach(EnemyConfig enemyConfig in _enemiesConfig)
        {
            _sumRateAppear += enemyConfig.RateAppear;
        }
    }

    public List<EnemyConfig> EnemiesConfig { get => _enemiesConfig; set => _enemiesConfig = value; }
    public float TimeWave { get => _timeWave; set => _timeWave = value; }
    public float TimeAppearEnemies { get => _timeAppearEnemies; set => _timeAppearEnemies = value; }
    public int MinEnemies { get => _minEnemies; set => _minEnemies = value; }
    public int MaxEnemies { get => _maxEnemies; set => _maxEnemies = value; }
    public float SumRateAppear { get => _sumRateAppear; set => _sumRateAppear = value; }
    public List<GameObject> BossPrefab { get => _bossPrefab; set => _bossPrefab = value; }
}

[Serializable]
public class EnemyConfig
{
    public GameObject EnemyPrefab;
    public float RateAppear;
}
