using CarterGames.Assets.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionController : Singleton<CharacterSelectionController>
{
    [Header("Controller Setting")]
    [SerializeField] private CharacterSelectionListController _characterListController;
    [SerializeField] private CharacterSelectDisplayController _characterSelectDisplayController;
    [SerializeField] private WeaponSkillDisplayController weaponSkillDisplayController;
    [Header ("Param")]
    [SerializeField,Tooltip("The character selected will show here, only bring this to GamePlay")] private GameObject _characterSelected;
    [Header ("Character Prefab Insert")]
    [SerializeField] private List<GameObject> _characterList;

    public CharacterSelectionListController CharacterSelectionListController { get => _characterListController; }
    public GameObject CharacterSelected { get => _characterSelected; set => _characterSelected = value; }

    private void Awake()
    {
        _characterListController.CharacterList = _characterList;
        CharacterSelectionListController.CharacterListInit();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    public void SetSelectedCharacter(GameObject characterSelected)
    {
        CharacterSelected = characterSelected;
        var characterData = CharacterSelected.GetComponent<CharacterBaseInfo>();
        _characterSelectDisplayController.LoadData(characterData);
        weaponSkillDisplayController.LoadData(characterData);
    }

    public void OnBackBtnClicked()
    {
        MenuController.Instance.HandleSelectCharBack();
    }

    public void ChangeStateCharSelectUI(bool state)
    {
        this.gameObject.SetActive(state);
    }

    public void OnClickStart()
    {
        MenuController.Instance.HandleOnClickPlay();
        MusicPlayer.instance.PlayTrack(SoundController.Instance.GetMusicGamePlayBG());
    }
}
