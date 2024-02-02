using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectDisplayController : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] private Image _characterSelectedDisplay;
    [SerializeField] private CharacterInfoListController characterInfoListController;
    [SerializeField] Character_Mod _selectedCharacterData;
    [SerializeField] private Sprite _characterSprite;
    [SerializeField] private Button _functionBtn;
    [SerializeField] private TMP_Text _functionBtnText;

    [Header ("Configs")]
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color activeColor;


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

    public void HiddenUI(){
        
    }

    public void ConfigFunctionBtnStyle(bool isActive, string context){
        if(isActive){
            _functionBtn.targetGraphic.color = activeColor;
            _functionBtn.interactable = true;
        }else{
            _functionBtn.targetGraphic.color = inactiveColor;
            _functionBtn.interactable = false;
        }
        _functionBtnText.text = context;
    }
}
