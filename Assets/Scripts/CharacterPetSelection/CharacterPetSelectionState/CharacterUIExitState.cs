using UnityEngine;

public class CharacterUIExitState : IState
{
    public CharacterUIExitState(CharacterPetSelectionController characterPetSelectionController) : base(characterPetSelectionController)
    {
    }

    public override void DoState()
    {
        var controller = this.CharacterPetSelectionController;
        var currentState = controller.GetCurrentStateEnum();
        IState currentConfig = null;
        switch(currentState){
            case CHARACTER_SELECT_STATES.PLAYER_READY:
                currentConfig = controller.GetStateConfig(CHARACTER_SELECT_STATES.PET_SELECT);
                controller.SetState(currentConfig);
            break;
            case CHARACTER_SELECT_STATES.PET_SELECT:
            case CHARACTER_SELECT_STATES.PET_BUY:
                currentConfig = controller.GetStateConfig(CHARACTER_SELECT_STATES.CHARACTER_SELECT);
                controller.SetState(currentConfig);
            break;

            case CHARACTER_SELECT_STATES.CHARACTER_SELECT:
                currentConfig = controller.GetStateConfig(CHARACTER_SELECT_STATES.DISABLE);
                controller.SetState(currentConfig);
            break;
        }
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        
    }
}