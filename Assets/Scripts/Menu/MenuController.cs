using CarterGames.Assets.AudioManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : Singleton<MenuController>
{
    [SerializeField] private AudioManager _soundSetting;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private AudioClip _musicMenuBG;
    [SerializeField] private AudioClip _musicGamePlayBG;
    [SerializeField] private GameObject _characterSelectUI;
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private AudioMenuController _audioMenuController;

    private void Awake()
    {
        _audioMenuController.Init();
    }
    public void OnClickSoundSetting()
    {
        //_soundSetting.SetActive(true);
    }

    public void OnCloseSoundSetting()
    {
        //_soundSetting.SetActive(false);
    }

    public void OnClickStart()
    {
        /*SceneManager.LoadScene("GamePlay");*/
        // AudioManager.Instance.PlayMusic("ThemeGamePlay");
        //MusicPlayer.instance.PlayTrack(_musicGamePlayBG);
        /*_characterSelected.SetActive(true);*/
        Debug.Log("Start");
        
        _characterSelectUI.SetActive(true);
        //_menuUI.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("menu");
        //Debug.Log("Start");
        // AudioManager.Instance.PlayMusic("ThemeGamePlay");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
    public void OnClickScore()
    {

    }

    public void OnClickSetting()
    {
        Debug.Log("Setting Btn Clicked");
    }

    private void OnEnable()
    {
        _musicPlayer.PlayTrack(_musicMenuBG);
    }

    public void HandleSelectCharBack()
    {
        CharacterSelectionController.Instance.ChangeStateCharSelectUI(false);
        _menuUI.SetActive(true);
        MusicPlayer.instance.PlayTrack();
    }

    public void HandleOnClickPlay()
    {
        GameData.Instance.SelectedCharacter = CharacterSelectionController.Instance.CharacterSelected;
        SceneManager.LoadScene("GamePlay");
    }
}
