using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CarterGames.Assets.AudioManager;
using DG.Tweening;

public class GameSettingController : MonoBehaviour
{
    [SerializeField] private Slider _bgm;
    [SerializeField] private Slider _sfx;
    [SerializeField] private Vector3 _originLocation;
    public bool SettingPanelStatus {get => this.gameObject.activeInHierarchy;}
    public Vector3 OriginLocation {get => this._originLocation;}
    private void Start()
    {
        _bgm.onValueChanged.AddListener(this.OnModifyBGMValue);
        _sfx.onValueChanged.AddListener(this.OnModifySFXValue);
    }

    private void OnEnable()
    {
        Init();
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
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

    public void OnClickRestart()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void OnClickResume()
    {
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
