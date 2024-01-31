using UnityEngine;

public class PetBuyState : IState
{
    public PetBuyState(CharacterPetSelectionController characterPetSelectionController) : base(characterPetSelectionController)
    {
    }

    public override void DoState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        var configState = CharacterPetSelectionController.GetStateConfig(CHARACTER_SELECT_STATES.PET_SELECT);
        CharacterPetSelectionController.SetState(configState);
    }
}