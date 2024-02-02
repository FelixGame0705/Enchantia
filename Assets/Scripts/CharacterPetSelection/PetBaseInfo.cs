using UnityEngine;

public class PetBaseInfo : MonoBehaviour
{
    [SerializeField] private Sprite characterSprite;
    public Sprite CharacterSprite {get => this.characterSprite; set => this.characterSprite = value;}
}