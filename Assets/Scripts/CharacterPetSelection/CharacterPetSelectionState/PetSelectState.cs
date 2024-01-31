using UnityEngine;

public class PetSelectState : IState
{
    public PetSelectState(CharacterPetSelectionController characterPetSelectionController) : base(characterPetSelectionController)
    {
    }

    public override void DoState()
    {
        var controller = CharacterSelectionControllerManagement.Instance.PetSelectionController;
        controller.PetListInit();
        CharacterSelectionControllerManagement.Instance.CharacterWeaponInfoDisplayController.CharacterSelectDisplayController.ConfigFunctionBtnStyle(false, "Select Pet");
    }

    public override void ExitState()
    {
        var sumController = CharacterSelectionControllerManagement.Instance;
        sumController.CharacterWeaponInfoDisplayController.CharacterSelectDisplayController.ConfigFunctionBtnStyle(false, "");
        sumController.CharacterSelectionListController.enabled = false;
    }

    public override void UpdateState()
    {
        var controller = CharacterSelectionControllerManagement.Instance.PetSelectionController;
        var selectedPet = controller.SelectedImage;
        IState configState = null;
        if(!selectedPet.IsValid){
            configState = CharacterPetSelectionController.GetStateConfig(CHARACTER_SELECT_STATES.PET_BUY);
            CharacterSelectionControllerManagement.Instance.CharacterWeaponInfoDisplayController.CharacterSelectDisplayController.ConfigFunctionBtnStyle(true,"3000 $");
        }else{
            configState = CharacterPetSelectionController.GetStateConfig(CHARACTER_SELECT_STATES.PLAYER_READY);
            CharacterSelectionControllerManagement.Instance.CharacterWeaponInfoDisplayController.CharacterSelectDisplayController.ConfigFunctionBtnStyle(true,"Play");
        }       
        CharacterPetSelectionController.SetState(configState);
    }
}