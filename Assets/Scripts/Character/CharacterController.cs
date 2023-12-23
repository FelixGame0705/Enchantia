using CarterGames.Assets.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected float MoveX;
    protected float MoveY;
    [SerializeField] public CharacterData CharacterDataConfig;
    [SerializeField] protected WeaponSystem WeaponSystemInCharacter;
    [SerializeField] protected GameObject Target;
    [SerializeField] protected float CurrentHealth;
    [SerializeField] private UIPlayerController _uiPlayerController;
    [SerializeField] protected Transform Model;
    [SerializeField] protected Animator AnimatorPlayer;
    [SerializeField] protected int CurrentGold = 0;// this is number of gold that character harvesting in game
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
        HarvestDroppedItem();
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

    public GameObject GetTarget()
    {
        return Target;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Dodge value: " + CharacterModStats.Dodge.Value);
        if (Random.value < CharacterModStats.Dodge.Value)//dodge
        {
            return;
        }
        CurrentHealth -= (damage - damage*DealWithArmor());
        _uiPlayerController.SetCurrentHealthValue(CurrentHealth);
        if(CurrentHealth <= 0) 
        // MenuController.Instance.ReturnToMenu();
        GamePlayController.Instance.UpdateState(GAME_STATES.END_GAME);
    }

    private void HarvestDroppedItem()
    {
        //HashSet<GameObject> golds = GamePlayController.Instance.GetCurrencyController().GetGolds();
        HashSet<GameObject> droppedItems = GamePlayController.Instance.GetDroppedItemController().GetDroppedItems();
        if (droppedItems != null)
            foreach (GameObject droppedItem in droppedItems)
            {
                if (DistanceGold(droppedItem.transform.position) <= CharacterModStats.HarvestRange.Value && droppedItem.activeSelf == true)
                {
                    MoveDroppedItemToPlayer(droppedItem);
                }
            }

    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    private void MoveDroppedItemToPlayer(GameObject droppedItem)
    {
        droppedItem.transform.position = Vector2.MoveTowards(droppedItem.transform.position, Model.position, 15 * Time.deltaTime);
        if (Vector2.Distance(droppedItem.transform.position, Model.position) < 0.01f)
        {
            AudioManager.instance.Play("collectItem", null, GameData.Instance.GetVolumeAudioGame());
            HarvestDroppedItemType(droppedItem.GetComponent<DroppedItemType>().DroppedItemData.DroppedItemType);
            GamePlayController.Instance.GetDroppedItemController().ReturnDroppedItemToPool(droppedItem);
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
        CurrentGold += (int)CharacterModStats.Harvesting.Value * CurrentGold;
        return CurrentGold;
    }

    public void ResetCurrentGold()
    {
        CurrentGold = 0;
        _uiPlayerController.ResetCurrentGold();
    }

    public float DealWithArmor()
    {
        float damageTakenAfterArmor = 1 / (1 + (CharacterModStats.Armor.Value/15));
        float damageReduction = (1 - (damageTakenAfterArmor));
        return damageReduction;
    }

    private float _regerationHP = 0f;
    private float _hpRegenerationEvery1s = 1f;
    public void RegenerateHealth()
    {
        float HPEveryXSeconds = 5.0f / (1.0f + ((CharacterModStats.HPRegeneration.Value-1) / 2.25f));
        if (CurrentHealth < CharacterModStats.MaxHP.Value)
        {
            _hpRegenerationEvery1s /= HPEveryXSeconds;
            _regerationHP += _hpRegenerationEvery1s*Time.deltaTime;
            if (_regerationHP >= 1)
            {
                CurrentHealth += 1;
                _regerationHP = 0f;
            }
        }
    }

    public void AddCurrentHealth(int value)
    {
        CurrentHealth += value;
        _uiPlayerController.SetCurrentHealthValue(CurrentHealth);
    }

    public void HarvestDroppedItemType(DROPPED_ITEM_TYPE type)
    {
        switch (type)
        {
            case DROPPED_ITEM_TYPE.GOLD:
                HarvestGold();
                break;
            case DROPPED_ITEM_TYPE.FRUIT:
                HarvestFruit();
                break;
        }
    }

    private void HarvestFruit()
    {
        GamePlayController.Instance.GetCharacterController().AddCurrentHealth(3);
    }

    private void HarvestGold()
    {
        _uiPlayerController.AddCurrentGoldValue(1);
        CurrentGold += 1;
    }

    public UIPlayerController GetUIPlayerController()
    {
        return _uiPlayerController;
    }
}
