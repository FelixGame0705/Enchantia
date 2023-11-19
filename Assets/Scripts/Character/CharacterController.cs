using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected float MoveX;
    protected float MoveY;
    [SerializeField] protected CharacterData CharacterData;
    [SerializeField] protected Character Character;
    [SerializeField] protected WeaponSystem WeaponSystemInCharacter;
    [SerializeField] protected GameObject Target;
    [SerializeField] protected float CurrentHealth;
    [SerializeField] private UIPlayerController _uiPlayerController;
    [SerializeField] protected Transform Model;
    [SerializeField] protected Animator AnimatorPlayer;

    private Vector3 initModelScale;

    protected void Start()
    {
        Character = CharacterData.CharacterStats;
        CurrentHealth = CharacterData.CharacterStats.MaxHP;
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
        transform.Translate(moveDir * Character.Speed  * Time.deltaTime);
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
        CurrentHealth -= damage;
        _uiPlayerController.SetCurrentHealthValue(CurrentHealth);
        if(CurrentHealth <= 0) MenuController.Instance.ReturnToMenu();
    }

    private void HarvestGold()
    {
        HashSet<GameObject> golds = GamePlayController.Instance.GetCurrencyController().GetGolds();
        if (golds != null)
            foreach (GameObject gold in golds)
            {
                if (DistanceGold(gold.transform.position) <= CharacterData.CharacterStats.HavestRange && gold.activeSelf == true)
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
            GamePlayController.Instance.GetCurrencyController().AddGold(1);
        }
    }

    private float DistanceGold(Vector2 target)
    {
        return Vector2.Distance(Model.position, target);
    }

    public WeaponSystem GetWeaponSystem() { return WeaponSystemInCharacter; }

    public CharacterData GetCharacterData()
    {
        return this.CharacterData;
    }
}
