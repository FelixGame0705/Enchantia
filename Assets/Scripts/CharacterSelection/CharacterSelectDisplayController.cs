using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectDisplayController : MonoBehaviour
{
    [SerializeField] private Image _characterSelectedDisplay;
    [SerializeField] private CharacterInfoListController characterInfoListController;
    [SerializeField] CharacterData _selectedCharacterData;

    public void Render()
    {

    }
}
