using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected float MoveX;
    protected float MoveY;
    [SerializeField] protected CharacterData CharacterDataConfig;
    [SerializeField] protected WeaponSystem WeaponSystemInCharacter;
    [SerializeField] protected GameObject Target;
    [SerializeField] protected float CurrentHealth;
    [SerializeField] private UIPlayerController _uiPlayerController;
    [SerializeField] protected Transform Model;
    [SerializeField] protected Animator AnimatorPlayer;
    [SerializeField] protected int CurrentGold;// this is number of gold that character harvesting in game
    public Character_Mod CharacterModStats;
    private Vector3 initModelScale;

    protected void Start()
    {
        CharacterModStats = CharacterDataConfig.Character_Mod;
        CurrentHealth = CharacterModStats.MaxHP.Value;
        _uiPlayerController.SetMaxHealthValue(CurrentHealth);
        _uiPlayerController.SetCurrentHealthValue(CurrentHealth);
        initModelScale = Model.transform.localScale;
        if (AnimatorPlayer == null)
        {
            AnimatorPlayer = GetComponent<Animator>();
        }
    }

    protected void Update()
    {
        HarvestGold();
        CheckInput();
        RegenerateHealth();
        if (Target == null) return;
        WeaponSystemInCharacter.ExcuteAttack(Target.transform, gameObject.transform, true);
        
    }

    public void CheckInput()
    {
        MoveX = 0;
        MoveY = 0;

        if (Input.GetKey(KeyCode.W))
        {
            MoveY = +1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveY = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveX = +1f;
            Flip(true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MoveX = -1f;
            Flip(false);
        }

        if (MoveX == 0 && MoveY == 0)
        {
            AnimatorPlayer.SetBool("isMoving", false);
        }
        else
        {
            AnimatorPlayer.SetBool("isMoving", true);
        }

        Vector2 moveDir = new Vector2(MoveX, MoveY).normalized;
        transform.Translate(moveDir * DeathWithSpeed() * Time.deltaTime); ;
    }

    private void Flip(bool isFlip)
    {
        Model.transform.localScale = new Vector3(isFlip ? initModelScale.x : -initModelScale.x, initModelScale.y, initModelScale.z);
    }

    public void SetTarget(GameObject target)
    {
        Target = target;
    }

    public void TakeDamage(float damage)
    {
        if (Random.value < CharacterModStats.Dodge.Value)//dodge
        {
            return;
        }
        CurrentHealth -= (damage - damage*DealWithArmor());
        _uiPlayerController.SetCurrentHealthValue(CurrentHealth);
        if(CurrentHealth <= 0) MenuController.Instance.ReturnToMenu();
    }

    private void HarvestGold()
    {
        HashSet<GameObject> golds = GamePlayController.Instance.GetCurrencyController().GetGolds();
        if (golds != null)
            foreach (GameObject gold in golds)
            {
                if (DistanceGold(gold.transform.position) <= CharacterModStats.HarvestRange.Value && gold.activeSelf == true)
                {
                    MoveGoldToPlayer(gold);
                }
            }

    }

    private void MoveGoldToPlayer(GameObject gold)
    {
        gold.transform.position = Vector2.MoveTowards(gold.transform.position, Model.position, 15 * Time.deltaTime);
        if (gold.transform.position == Model.position)
        {
            GamePlayController.Instance.GetCurrencyController().ReturnGoldToPool(gold);
            _uiPlayerController.AddCurrentGoldValue(1);
            //GamePlayController.Instance.GetCurrencyController().AddGold(1);
        }
    }

    private float DistanceGold(Vector2 target)
    {
        return Vector2.Distance(Model.position, target);
    }

    public WeaponSystem GetWeaponSystem() { return WeaponSystemInCharacter; }

    public CharacterData GetCharacterData()
    {
        return this.CharacterDataConfig;
    }

    private float DeathWithSpeed()
    {
        return CharacterModStats.Speed.Value;
    }

    public void LifeSteal()
    {
        if(Random.value < CharacterModStats.LifeSteal.Value)
        {
            CurrentHealth += 1;
            _uiPlayerController.SetCurrentHealthValue(CurrentHealth);
        }
    }

    public int Harvesting()
    {
        CurrentGold = (int)CharacterModStats.Harvesting.Value * CurrentGold;
        return CurrentGold;
    }

    public float DealWithArmor()
    {
        float damageTakenAfterArmor = 1 / (1 + (CharacterModStats.Armor.Value/15));
        float damageReduction = (1 - (damageTakenAfterArmor));
        return damageReduction;
    }

    private float _regerationHP = 0f;
    public void RegenerateHealth()
    {
        float HPEveryXSeconds = 5.0f / (1.0f + ((CharacterModStats.HPRegeneration.Value-1) / 2.25f));
        if (CurrentHealth < CharacterModStats.MaxHP.Value)
        {
            _regerationHP += HPEveryXSeconds;
            if (_regerationHP >= 1)
            {
                CurrentHealth += 1;
                _regerationHP = 0f;
            }
        }
    }

}
