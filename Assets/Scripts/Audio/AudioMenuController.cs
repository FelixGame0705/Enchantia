using CarterGames.Assets.AudioManager;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenuController : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;
    public Button BackBtn;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private AudioPlayer _audioPlayer;

    private void Start()
    {
        BGMSlider.value = GameData.Instance.GetVolumeAudioBG();
        GameData.Instance.SetVolumeAudioBG(GameData.Instance.GetVolumeAudioBG()); 
        SFXSlider.value = GameData.Instance.GetVolumeAudioGame();
        GameData.Instance.SetVolumeAudioGame(GameData.Instance.GetVolumeAudioGame());
    }

    // Start is called before the first frame update
    public void SliderAudioBG()
    {
        GameData.Instance.SetVolumeAudioBG(BGMSlider.value);
        _musicPlayer.SetVolume(GameData.Instance.GetVolumeAudioBG());
    }

    public void SliderAudioGame()
    {
        GameData.Instance.SetVolumeAudioGame(SFXSlider.value);
        GameData.Instance.VolumeAudioGame = GameData.Instance.GetVolumeAudioGame();
    }

    public void OnClickSetting()
    {
        gameObject.SetActive(true);
    }

    public void OnClickBack()
    {
        gameObject.SetActive(false);
    }
}
