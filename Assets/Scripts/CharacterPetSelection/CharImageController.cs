using System.Linq.Expressions;
using UnityEngine;

public class CharImageController : IItemImageController
{
    public override void ChangeBackgroundToDefault()
    {
        Background.color = CharacterSelectionControllerManagement.Instance.unselectedColor;
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
        controller.ResetBackgroundSelectedCharacterToDefault();
        Background.color = CharacterSelectionControllerManagement.Instance.clickedColor;
        controller.HandleCharacterClicked(Index);
        
    }
}