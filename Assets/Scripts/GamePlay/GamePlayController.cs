using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePlayController : Singleton<GamePlayController>
{
    public GAME_STATES GameState;
    [SerializeField] private GameObject _character;
    [SerializeField] private GameObject _characterPattern;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private GameObject TargetCharacter;
    [SerializeField] private int _currentWave;
    [SerializeField] private GameObject _waveShop;
    [SerializeField] private WaveTimeController _waveTimeController;
    [SerializeField] private DroppedItemController _droppedItemController;
    [SerializeField] private GameOverController _gameOverController;
    [SerializeField] private float _timePlay = 0;

    private bool _isSpawnedBulletPool = false;
    public int CurrentWave{get => _currentWave;}
    public GameObject Character { get => _character; set => _character = value; }
    public float TimePlay { get => _timePlay; set => _timePlay = value; }

    //[SerializeField] private Con

    // Start is called before the first frame update
    void Start()
    {
        _characterPattern = GameData.Instance.SelectedCharacter;
        _character = Instantiate(_characterPattern);
        _currentWave = 1;
    }

    private void OnEnable()
    {
        UpdateState(GAME_STATES.START);
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestTarget();
    }

    //public void Spawning

    public void SetState(GAME_STATES state)
    {
        GameState = state;
    }

    public void UpdateState(GAME_STATES state)
    {
        SetState(state);
        switch (GameState)
        {
            case GAME_STATES.START:
                StartCoroutine(WaitSpawnPlayer());
                break;
            case GAME_STATES.WAVE_SHOP:
                _waveShop.SetActive(true);
                _waveTimeController.SetCoundownTime(_enemyFactory.GetWaveGameData().TimeWave);
/*                WaveShopMainController.Instance.AddGoldValue(GetCharacterController().Harvesting());*/
                WaveShopMainController.Instance.UpdateMoney();
                GetCharacterController().ResetCurrentGold();
                ResetEnemiesInWave();
                _waveTimeController.SetWave(_currentWave);
                SetTimeForEnemyFactory();
                _enemyFactory.SetEnemyModelPool();
                Time.timeScale = 0;
                break;
            case GAME_STATES.PLAYING:
                Time.timeScale = 1;
                SetBulletPrefabBulletFactory();
                StartCoroutine(_waveTimeController.Countdown());
                StartCoroutine(TimePlayCounting());
                _waveShop.SetActive(false);
                _enemyFactory.SetIsSpawned(true);
                _enemyFactory.SetTarget(Character);
                break;
            case GAME_STATES.GAME_OVER:
                _isSpawnedBulletPool = false;
                UpWave();
                UpdateState(GAME_STATES.WAVE_SHOP);
                break;
            case GAME_STATES.END_GAME:
                Time.timeScale = 0;
                _enemyFactory.SetIsSpawned(false);
                _gameOverController.RenderUI();
                break;
        }
    }

    private void SetTimeForEnemyFactory()
    {
        _enemyFactory.SetCurrentWave(_currentWave-1);
        _enemyFactory.SetTimeAppearEnemies();
    }

    private IEnumerator WaitSpawnPlayer()
    {
        yield return new WaitUntil(() => Character != null);
        CameraFollow.Instance.target = Character.transform;
        UpdateState(GAME_STATES.WAVE_SHOP);
        UpdateState(GAME_STATES.PLAYING);
        //_enemies.Add(_enemyFactory.CreateEnemy(_character));
        Debug.Log("Play");
    }
    private IEnumerator TimePlayCounting()
    {
        while (GAME_STATES.PLAYING == GameState) // Stop after 10 seconds (you can change this condition)
        {
            yield return new WaitForSeconds(1.0f);
            TimePlay++;
        }
    }


    public BulletFactory GetBulletFactory()
    {
        return _bulletFactory;
    }

    public EnemyFactory GetEnemyFactory()
    {
        return _enemyFactory;
    }

    private void FindNearestTarget()
    {
        if (_enemyFactory.GetEnemies().Count == 0) return;
        if(TargetCharacter==null)
            TargetCharacter = _enemyFactory.GetEnemies().ToArray()[0];
        for (int i = 0; i < _enemyFactory.GetEnemies().Count; i++)
        {
            if (Vector2.Distance(_enemyFactory.GetEnemies().ToArray()[i].transform.position, Character.transform.position) <= Vector2.Distance(TargetCharacter.transform.position, Character.transform.position) && _enemyFactory.GetEnemies().ToArray()[i].activeSelf)
            {
                TargetCharacter = _enemyFactory.GetEnemies().ToArray()[i];
            }
        }
        if (TargetCharacter.activeSelf)
            Character.GetComponent<CharacterController>().SetTarget(TargetCharacter);
        else Character.GetComponent<CharacterController>().SetTarget(null);
    }

    public void UpWave()
    {
        _currentWave += 1;
    }

    public int GetCurrentWave()
    {
        return _currentWave;
    }

    public DroppedItemController GetDroppedItemController()
    {
        return _droppedItemController;
    }

    public WeaponSystem GetWeaponSystem()
    {
        return Character.GetComponent<CharacterController>().GetWeaponSystem();
    }

    public CharacterController GetCharacterController()
    {
        return Character.GetComponent<CharacterController>();
    }

    public void RemoveEnemies()
    {
        for(int i = 0; i < GetEnemyFactory().GetEnemies().ToArray().Length; i++)
        {
            Destroy(GetEnemyFactory().GetEnemies().ToArray()[i]);
        }
        GetEnemyFactory().GetEnemies().Clear();
    }

    public void ResetEnemiesInWave()
    {
        RemoveEnemies();
        _enemyFactory.ResetEnemiesPool();
    }

    private List<GameObject> GetBulletModelPrefab()
    {
        List<GameObject> weapons = new List<GameObject>();
        for(int i = 0; i < GetWeaponSystem().GetCountWeapon(); i++)
        {
            if(GetCharacterController().GetWeaponSystem().GetWeapon(i) as WeaponRanged)
            {
                WeaponRanged wp = (WeaponRanged) GetCharacterController().GetWeaponSystem().GetWeapon(i);
                weapons.Add(wp.GetBulletPrefab());
            }
        }
        return weapons;
    }

    private void SetBulletPrefabBulletFactory()
    {
        if (_isSpawnedBulletPool == false)
        {
            _bulletFactory.SetBulletModelPrefab(GetBulletModelPrefab());
            _isSpawnedBulletPool = true;
        }
    }
}
