using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionController : Singleton<CharacterSelectionController>
{
    
    [SerializeField] private CharacterSelectionListController _characterListController;
    [SerializeField] private CharacterSelectDisplayController _characterSelectDisplayController;
    [SerializeField] private GameObject _characterSelected;
    [SerializeField] private List<GameObject> _characterList;

    public CharacterSelectionListController CharacterSelectionListController { get => _characterListController; }

    private void Awake()
    {
        _characterListController.CharacterList = _characterList;
        CharacterSelectionListController.CharacterListInit();
    }

    public void SetSelectedCharacter(GameObject characterSelected)
    {
        _characterSelected = characterSelected;
    }

    public void OnBackBtnClicked()
    {
        this.gameObject.SetActive(false);
    }
}
