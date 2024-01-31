using UnityEngine;
using CarterGames.Assets.AudioManager;

public class PlayerReadyState : IState
{
    public PlayerReadyState(CharacterPetSelectionController characterPetSelectionController) : base(characterPetSelectionController)
    {
    }

    public override void DoState()
    {
        var controller = CharacterSelectionControllerManagement.Instance;
        controller.CharacterWeaponInfoDisplayController.CharacterSelectDisplayController.ConfigFunctionBtnStyle(true, "PLAY");
    }

    public override void ExitState()
    {
        var controller = CharacterSelectionControllerManagement.Instance;
        controller.CharacterWeaponInfoDisplayController.CharacterSelectDisplayController.ConfigFunctionBtnStyle(false, "");
    }

    public override void UpdateState()
    {
        MenuController.Instance.HandleOnClickPlay();
        MusicPlayer.instance.PlayTrack(SoundController.Instance.GetMusicGamePlayBG());
    }
}