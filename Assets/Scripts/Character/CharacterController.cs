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

    protected void Start()
    {
        Character = CharacterData.CharacterStats;
        CurrentHealth = CharacterData.CharacterStats.MaxHP;
        _uiPlayerController.SetMaxHealthValue(CurrentHealth);
        _uiPlayerController.SetCurrentHealthValue(CurrentHealth);
    }

    protected void Update()
    {
        CheckInput();
        if (Target == null) return;
        WeaponSystemInCharacter.ExcuteAttack(Target.transform, gameObject.transform, true);
    }

    public void CheckInput()
    {
        MoveX = 0;
        MoveY = 0;

        if (MoveX == 0 && MoveY == 0)
        {
            //AnimatorPlayer.Play("Idle");
        }
        else
        {
            //AnimatorPlayer.Play("Run");
        }

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
        Vector2 moveDir = new Vector2(MoveX, MoveY).normalized;
        transform.Translate(moveDir * Character.Speed  * Time.deltaTime);
    }

    private void Flip(bool isFlip)
    {
        //_model.transform.localScale = new Vector3(isFlip ? -1 : 1, 1, 1);
    }

    public void SetTarget(GameObject target)
    {
        Target = target;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        _uiPlayerController.SetCurrentHealthValue(CurrentHealth);
    }
}
