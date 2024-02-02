using UnityEngine;

public class CharacterSelectionControllerManagement : Singleton<CharacterSelectionControllerManagement>
{

    [Header ("Controller Lists")]
    [SerializeField] private CharacterSelectionListController characterSelectionListController;
    [SerializeField] private PetSelectionController petSelectionController;
    [SerializeField] private CharacterWeaponInfoDisplayController characterWeaponInfoDisplayController;
    [SerializeField] private CharacterPetSelectionController characterPetSelectionController;

    [Header ("Configs")]
    [SerializeField] public Color inactiveColor;
    public Color clickedColor;
    public Color unselectedColor;
    public Color selectedColor;

    public CharacterSelectionListController CharacterSelectionListController {get => this.characterSelectionListController;}
    public PetSelectionController PetSelectionController {get => this.petSelectionController;}
    public CharacterWeaponInfoDisplayController CharacterWeaponInfoDisplayController {get => this.characterWeaponInfoDisplayController;}
    public CharacterPetSelectionController CharacterPetSelectionController {get => this.characterPetSelectionController;}
}