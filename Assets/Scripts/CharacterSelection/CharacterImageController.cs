using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CharacterImageController : MonoBehaviour
{
    [SerializeField] private Sprite characterImage;
    [SerializeField] private int index;
    [SerializeField] Image characterRender;
    [SerializeField] private Image background;

    public void Render()
    {
        characterRender.sprite = characterImage;
        this.gameObject.SetActive(true);
    }

    public void OnCharacterClicked()
    {
        CharacterSelectionController.Instance.CharacterSelectionListController.HandleCharacterClicked(index);
        background.sprite = CharacterSelectionController.Instance.CharacterSelectionListController.BackgroundSelected;
        CharacterSelectionController.Instance.CharacterSelectionListController.ResetBackgroundSelectedCharacterToDefault();
    }

    public void LoadData(int index, Sprite characterImage)
    {
        this.index = index;
        this.characterImage = characterImage;
        Render();
    }

    public void ChanageBackgroundToDefault()
    {
        background.sprite = CharacterSelectionController.Instance.CharacterSelectionListController.BackgroundDefault;
    }
}
