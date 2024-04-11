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
    [SerializeField] private GameObject TargetCharacter;
    [SerializeField] private GameObject _waveShop;
    [SerializeField] private WaveTimeController _waveTimeController;
    [SerializeField] private DroppedItemController _droppedItemController;
    [SerializeField] private GameOverController _gameOverController;
    [SerializeField] private GamePlayUIManager gamePlayUIManager;
    [SerializeField] private float _timePlay = 0;
    [SerializeField] private bool _isEndLess;

    private bool _isSpawnedBulletPool = false;
    // public int CurrentWave{get => _currentWave;}
    private int CurrentWave{get => GameDataController.Instance.CurrentGamePlayData.CurrentWave;}
    public GameObject Character { get => _character; set => _character = value; }
    public float TimePlay { get => _timePlay; set => _timePlay = value; }


    // Start is called before the first frame update
    void Start()
    {
        _characterPattern = GameData.Instance.SelectedCharacter;
        _character = Instantiate(_characterPattern);
        GameDataController.Instance.CurrentGamePlayData.Character = _character;
        GameDataController.Instance.CurrentGamePlayData.CurrentWave = 1;
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
                _waveTimeController.SetCountdownTime(_enemyFactory.GetWaveGameData().TimeWave);
                WaveShopMainController.Instance.UpdateMoney();
                GetCharacterController().ResetCurrentGold();
                ResetEnemiesInWave();
                _bulletFactory.ResetPoolObject();
                _waveTimeController.SetWave(GameDataController.Instance.CurrentGamePlayData.CurrentWave);
                SetTimeForEnemyFactory();
                _enemyFactory.SetEnemyModelPool();
                _enemyFactory.SetBossModelPool();
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
                var maxWave = _enemyFactory.WaveGameDataList().Count;
                if(_isEndLess) gamePlayUIManager.RenderGameOverPanel(GAME_OVER_TYPE.ENDLESS);
                else if(CurrentWave >= maxWave) gamePlayUIManager.RenderGameOverPanel(GAME_OVER_TYPE.WON);
                else gamePlayUIManager.RenderGameOverPanel(GAME_OVER_TYPE.LOST);
                break;
        }
    }

    private void SetTimeForEnemyFactory()
    {
        _enemyFactory.SetCurrentWave(GameDataController.Instance.CurrentGamePlayData.CurrentWave-1);
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
            GameDataController.Instance.CurrentGamePlayData.TimeSurvive = TimePlay;
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
        GameDataController.Instance.CurrentGamePlayData.CurrentWave +=1;
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
        HashSet<GameObject> enemiesHashSet = GetEnemyFactory().GetEnemies();

        // Chuy?n ??i HashSet th�nh m?ng ?? c� th? s? d?ng index
        GameObject[] enemiesArray = enemiesHashSet.ToArray();

        for (int i = enemiesArray.Length - 1; i >= 0; i--)
        {
            GameObject enemy = enemiesArray[i];
            EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();

            if (enemyBase != null && !enemyBase.isSurviveNextWave)
            {
                Destroy(enemy);
                enemiesHashSet.Remove(enemy);
            }
        }
    }


    public void ResetEnemiesInWave()
    {
        RemoveEnemies();
        _enemyFactory.ResetEnemiesPool();
    }

    private Dictionary<int,GameObject> GetBulletModelPrefab()
    {
        Dictionary<int,GameObject> weapons = new Dictionary<int,GameObject>();
        for(int i = 0; i < GetWeaponSystem().GetCountWeapon(); i++)
        {
            if(GetCharacterController().GetWeaponSystem().GetWeapon(i) as WeaponRanged)
            {
                WeaponRanged wp = (WeaponRanged) GetCharacterController().GetWeaponSystem().GetWeapon(i);
                weapons.Add(wp.GetID(),wp.GetBulletPrefab());
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

    public bool CheckGameWonCondition(){
        if(!_isEndLess){
            var maxWave = _enemyFactory.WaveGameDataList().Count;
            if(maxWave <= CurrentWave) return true;
        }
        return false;
    }
}
