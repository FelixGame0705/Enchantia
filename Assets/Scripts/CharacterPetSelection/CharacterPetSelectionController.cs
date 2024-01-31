using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CharacterPetSelectionController : MonoBehaviour
{
    private IState currentState;
    private List<IState> stateList;

    [Header("Select Info")]
    [SerializeField] private GameObject _selectedCharacter;
    [SerializeField] private GameObject _selectedPet;

    public GameObject SelectedCharacter {get => this._selectedCharacter;}
    public GameObject SelectedPet {get => this._selectedPet;}

    public void SetSelectedCharacter(GameObject selectedCharacter){
        this._selectedCharacter = selectedCharacter;
        var characterBaseInfo = selectedCharacter.GetComponent<CharacterBaseInfo>();
        CharacterSelectionControllerManagement.Instance.CharacterWeaponInfoDisplayController.LoadData(characterBaseInfo);
    }

    public void SetSelectedPet(GameObject selectedPet){
        this._selectedPet = selectedPet;
    }

    public void Awake()
    {
        this.stateList = new List<IState>(){
            new CharacterSelectState(this),
            new PetBuyState(this),
            new PetSelectState(this),
            new PlayerReadyState(this),
            new CharacterUIExitState(this)
        };
    }

    public void OnEnable()
    {
        UIFirstLoad();
    }

    public void OnDisable()
    {

    }

    public void SetState(IState setState)
    {
        this.currentState = setState;
    }
    //Pet Select State Happen When Character Is Selected
    //Pet Buy State Happen When Clicked Pet Not Owned
    public IState GetStateConfig(CHARACTER_SELECT_STATES state)
    {
        IState stateNeed = null;
        switch (state)
        {
            case CHARACTER_SELECT_STATES.CHARACTER_SELECT:
                stateNeed = stateList.OfType<CharacterSelectState>().FirstOrDefault();
                break;

            case CHARACTER_SELECT_STATES.PET_SELECT:
                stateNeed = stateList.OfType<PetSelectState>().FirstOrDefault();
                break;

            case CHARACTER_SELECT_STATES.PET_BUY:
                stateNeed = stateList.OfType<PetBuyState>().FirstOrDefault();
                break;

            case CHARACTER_SELECT_STATES.PLAYER_READY:
                stateNeed = stateList.OfType<PlayerReadyState>().FirstOrDefault();
                break;
            case CHARACTER_SELECT_STATES.DISABLE:
                stateNeed = stateList.OfType<CharacterUIExitState>().FirstOrDefault();
                break;
        }
        return stateNeed;
    }

    public CHARACTER_SELECT_STATES GetCurrentStateEnum()
    {
        if (currentState is CharacterSelectState)
            return CHARACTER_SELECT_STATES.CHARACTER_SELECT;
        else if (currentState is PetSelectState)
            return CHARACTER_SELECT_STATES.PET_SELECT;
        else if (currentState is PetBuyState)
            return CHARACTER_SELECT_STATES.PET_BUY;
        else if (currentState is PlayerReadyState)
            return CHARACTER_SELECT_STATES.PLAYER_READY;
        else if (currentState is CharacterUIExitState)
            return CHARACTER_SELECT_STATES.DISABLE;
        throw new Exception("State not found");
    }


    public void StateSwitchClicked()
    {
        this.currentState.ExitState();
        this.currentState.UpdateState();
        this.currentState.DoState();
    }

    public void UIFirstLoad(){
        if(currentState == null){
            currentState = GetStateConfig(CHARACTER_SELECT_STATES.CHARACTER_SELECT);
            currentState.DoState();
        }
    }

}