using System.Linq.Expressions;
using System.Net.Security;
using UnityEngine;

public class PetImageController : IItemImageController
{
    public override void ChangeBackgroundToDefault()
    {
        Background.sprite = CharacterSelectionControllerManagement.Instance.CharacterSelectionListController.BackgroundDefault;
    }

    public override void LoadData(int index, Sprite image, bool isValid)
    {
        Index = index;
        SpriteImage = image;
        IsValid = isValid;
        Render();
    }

    public override void OnImageClicked()
    {
        var controller = CharacterSelectionControllerManagement.Instance.PetSelectionController;
        controller.HandlePetClicked(Index);
        Background.sprite = controller.BackgroundSelected;
        controller.ResetBackgroundSelectedCharacterToDefault();
    }
}