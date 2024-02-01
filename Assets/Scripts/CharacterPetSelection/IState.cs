using UnityEngine;

public abstract class IState: MonoBehaviour
{
    private CharacterPetSelectionController characterPetSelectionController;

    public CharacterPetSelectionController CharacterPetSelectionController {get => characterPetSelectionController;}

    public IState (CharacterPetSelectionController characterPetSelectionController){
        this.characterPetSelectionController = characterPetSelectionController;
    }
    public abstract void UpdateState();
    public abstract void DoState();
    public abstract void ExitState();
}