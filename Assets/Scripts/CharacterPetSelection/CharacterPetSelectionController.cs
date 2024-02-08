using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CharacterPetSelectionController : MonoBehaviour
{

    [Header("Select Info")]
    [SerializeField] private GameObject _selectedCharacter;
    [SerializeField] private GameObject _selectedPet;

    public CHARACTER_SELECT_STATES currentState;

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

    public void OnEnable()
    {
        currentState = CHARACTER_SELECT_STATES.SELECT_STATE;
        var controller = CharacterSelectionControllerManagement.Instance;
        controller.CharacterSelectionListController.CharacterListInit();
        controller.PetSelectionController.PetListInit();
        CheckIsReady();
    }

    public void OnDisable()
    {

    }
    public void FunctionClicked()
    {
        switch(currentState){
            case CHARACTER_SELECT_STATES.SELECT_STATE:
                currentState = CHARACTER_SELECT_STATES.READY_STATE;
                CharacterSelectionControllerManagement.Instance.CharacterWeaponInfoDisplayController.CharacterSelectDisplayController.ConfigFunctionBtnStyle(true, "GO");
                CharacterSelectionControllerManagement.Instance.CharacterSelectionListController.GetBaseSelected?.ChangeStatusColor(true);
                CharacterSelectionControllerManagement.Instance.PetSelectionController.SelectedImage?.ChangeStatusColor(true);
                CharacterSelectionControllerManagement.Instance.PetSelectionController.ItemsDisableEnableByBool(false);
                CharacterSelectionControllerManagement.Instance.CharacterSelectionListController.ItemsDisableEnableByBool(false);
            break;
            case CHARACTER_SELECT_STATES.READY_STATE:
                MenuController.Instance.HandleOnClickPlay();
            break;
        }
    }

    public void CheckIsReady(){
        if(_selectedPet != null && _selectedCharacter != null){
            CharacterSelectionControllerManagement.Instance.CharacterWeaponInfoDisplayController.CharacterSelectDisplayController.ConfigFunctionBtnStyle(true, "SELECTED");
        }else{
            CharacterSelectionControllerManagement.Instance.CharacterWeaponInfoDisplayController.CharacterSelectDisplayController.ConfigFunctionBtnStyle(false, "PLEASE SELECTED");
        }
    }

    public void OnBackBtnClicked(){
        switch(currentState){
            case CHARACTER_SELECT_STATES.READY_STATE:
            currentState = CHARACTER_SELECT_STATES.SELECT_STATE;
            CharacterSelectionControllerManagement.Instance.CharacterSelectionListController.GetBaseSelected?.ChangeStatusColor(false);
            CharacterSelectionControllerManagement.Instance.PetSelectionController.SelectedImage?.ChangeStatusColor(false);
            CharacterSelectionControllerManagement.Instance.PetSelectionController.ItemsDisableEnableByBool(true);
            CharacterSelectionControllerManagement.Instance.CharacterSelectionListController.ItemsDisableEnableByBool(true);
            CheckIsReady();
            break;

            case CHARACTER_SELECT_STATES.SELECT_STATE:
            MenuController.Instance.HandleSelectCharBack();
            break;
        }
    }

    public void ChangeStateCharSelectUI(bool state){
        this.gameObject.SetActive(state);
    }

}