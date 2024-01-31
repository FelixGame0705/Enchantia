using System.Linq.Expressions;
using UnityEngine;

public class CharImageController : IItemImageController
{
    public override void ChangeBackgroundToDefault()
    {
        Background.sprite = CharacterSelectionControllerManagement.Instance.CharacterSelectionListController.BackgroundDefault;
    }

    public override void LoadData(int index, Sprite image, bool isValid)
    {
        Index= index;
        SpriteImage = image;
        IsValid = isValid;
        Render();
    }

    public override void OnImageClicked()
    {
        var controller = CharacterSelectionControllerManagement.Instance.CharacterSelectionListController;
        controller.HandleCharacterClicked(Index);
        Background.sprite = controller.BackgroundSelected;
        controller.ResetBackgroundSelectedCharacterToDefault();
        CharacterSelectionControllerManagement.Instance.CharacterWeaponInfoDisplayController.CharacterSelectDisplayController.ConfigFunctionBtnStyle(true, "Select Character");
    }
}