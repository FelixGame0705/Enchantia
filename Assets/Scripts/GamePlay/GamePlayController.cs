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
    [SerializeField] private CurrencyController _currencyController;

    // Start is called before the first frame update
    void Start()
    {
        _character = Instantiate(_characterPattern);
        _currentWave = 0;
        
    }

    private void OnEnable()
    {
        //SetState();
        //UpdateState(GAME_STATES.WAVE_SHOP);
        UpdateState(GAME_STATES.START);
       
        
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestTarget();
        //_character.GetComponent<CharacterController>().SetTarget(TargetCharacter);
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
                _waveTimeController.SetCoundownTime(60);
                Time.timeScale = 0;
                break;
            case GAME_STATES.PLAYING:
                Time.timeScale = 1;
                StartCoroutine(_waveTimeController.Countdown());
                _waveShop.SetActive(false);
                _enemyFactory.SetIsSpawned(true);
                _enemyFactory.SetTarget(_character);
                break;
            case GAME_STATES.GAME_OVER:
                UpdateState(GAME_STATES.WAVE_SHOP);
                break;
        }
    }

    private IEnumerator WaitSpawnPlayer()
    {
        yield return new WaitUntil(() => _character != null);
        CameraFollow.Instance.target = _character.transform;
        UpdateState(GAME_STATES.WAVE_SHOP);
        UpdateState(GAME_STATES.PLAYING);
        //_enemies.Add(_enemyFactory.CreateEnemy(_character));
        Debug.Log("Play");
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
            if (Vector2.Distance(_enemyFactory.GetEnemies().ToArray()[i].transform.position, _character.transform.position) <= Vector2.Distance(TargetCharacter.transform.position, _character.transform.position) && _enemyFactory.GetEnemies().ToArray()[i].activeSelf)
            {
                TargetCharacter = _enemyFactory.GetEnemies().ToArray()[i];
            }
        }
        if (TargetCharacter.activeSelf)
            _character.GetComponent<CharacterController>().SetTarget(TargetCharacter);
        else _character.GetComponent<CharacterController>().SetTarget(null);
    }

    public void LevelWave()
    {
        _currentWave += 1;
    }

    public CurrencyController GetCurrencyController()
    {
        return _currencyController;
    }

    public WeaponSystem GetWeaponSystem()
    {
        return _character.GetComponent<CharacterController>().GetWeaponSystem();
    }

    public CharacterController GetCharacterController()
    {
        return _character.GetComponent<CharacterController>();
    }
}
