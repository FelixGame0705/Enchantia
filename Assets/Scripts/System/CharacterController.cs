using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected float MoveX;
    protected float MoveY;
    [SerializeField] protected CharacterData characterData;
    [SerializeField] protected Character character;
    [SerializeField] protected WeaponSystem WeaponSystemInCharacter;

    protected void Start()
    {
        character = characterData.CharacterStats;
    }

    protected void Update()
    {
        CheckInput();
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
        transform.Translate(moveDir * character.Speed  * Time.deltaTime);
    }

    private void Flip(bool isFlip)
    {
        //_model.transform.localScale = new Vector3(isFlip ? -1 : 1, 1, 1);
    }
}
