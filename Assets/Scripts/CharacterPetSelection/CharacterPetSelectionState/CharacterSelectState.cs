using System;
using TMPro;
using UnityEngine;

public class CharacterSelectState : IState
{
    public CharacterSelectState(CharacterPetSelectionController characterPetSelectionController) : base(characterPetSelectionController)
    {
    }

    public override void DoState()
    {
        var sumController = CharacterSelectionControllerManagement.Instance;
        sumController.CharacterSelectionListController.CharacterListInit();
        sumController.PetSelectionController.PetListInit();
        sumController.CharacterWeaponInfoDisplayController.DisableUI();
        sumController.CharacterWeaponInfoDisplayController.CharacterSelectDisplayController.ConfigFunctionBtnStyle(false, "Select Character");
    }

    public override void ExitState()
    {
        var sumController = CharacterSelectionControllerManagement.Instance;
        sumController.CharacterWeaponInfoDisplayController.CharacterSelectDisplayController.ConfigFunctionBtnStyle(false, "");
        sumController.CharacterSelectionListController.enabled = false;
    }

    public override void UpdateState()
    {
        var configState = CharacterPetSelectionController.GetStateConfig(CHARACTER_SELECT_STATES.PET_SELECT);
        CharacterPetSelectionController.SetState(configState);
    }
}