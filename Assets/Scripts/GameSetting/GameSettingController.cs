using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CarterGames.Assets.AudioManager;

public class GameSettingController : MonoBehaviour
{
    [SerializeField] private GameObject _gameSetting;
    [SerializeField] private Slider _bgm;
    [SerializeField] private Slider _sfx;

    private bool _isActive = false;
    private void Start()
    {
        _bgm.onValueChanged.AddListener(this.OnModifyBGMValue);
        _sfx.onValueChanged.AddListener(this.OnModifySFXValue);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _gameSetting.SetActive(!_isActive);
            _isActive = !_isActive;
            Time.timeScale = _isActive == true ?  0 : 1;
            Init();
        }
    }

    private void OnModifyBGMValue(float bfmValue)
    {
        GameData.Instance.SetVolumeAudioBG(_bgm.value);
        MusicPlayer.instance.SetVolume(GameData.Instance.GetVolumeAudioBG());
    }

    private void OnModifySFXValue(float sfxVlue)
    {
        GameData.Instance.SetVolumeAudioGame(_sfx.value);
        GameData.Instance.VolumeAudioGame = GameData.Instance.GetVolumeAudioGame();
    }

    public void OnClickBackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnCickRestart()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void OnClickResume()
    {
        _gameSetting.SetActive(!_isActive);
        Time.timeScale = 1;
    }

    public void Init()
    {
        _bgm.value = GameData.Instance.GetVolumeAudioBG();
        GameData.Instance.SetVolumeAudioBG(GameData.Instance.GetVolumeAudioBG());
        MusicPlayer.instance.SetVolume(GameData.Instance.GetVolumeAudioBG());
        _sfx.value = GameData.Instance.GetVolumeAudioGame();
        GameData.Instance.SetVolumeAudioGame(GameData.Instance.GetVolumeAudioGame());
    }
    private void OnDestroy()
    {
        _bgm.onValueChanged.RemoveAllListeners();
        _sfx.onValueChanged.RemoveAllListeners();
    }
}
