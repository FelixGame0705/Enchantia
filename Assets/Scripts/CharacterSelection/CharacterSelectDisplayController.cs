using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectDisplayController : MonoBehaviour
{
    [SerializeField] private Image _characterSelectedDisplay;
    [SerializeField] private CharacterInfoListController characterInfoListController;
    [SerializeField] Character_Mod _selectedCharacterData;
    [SerializeField] private Sprite _characterSprite;

    private bool isFirstRender = false;

    public void Render()
    {
        if(isFirstRender == false)
        {
            this.gameObject.SetActive(true);
            isFirstRender = true;
        }
        _characterSelectedDisplay.sprite = _characterSprite;
        characterInfoListController.LoadData(_selectedCharacterData);
        
    }

    public void LoadData(CharacterBaseInfo data)
    {
        _selectedCharacterData = data.CharacterStats;
        _characterSprite = data.CharacterSprite;
        Render();
    }
}
