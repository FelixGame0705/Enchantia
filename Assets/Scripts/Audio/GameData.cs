using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    private const string PLAY_AUDIO_BG = "playAudioBg";
    private const string PLAY_AUDIO_GAME = "playAudioGame";
    private const string VOLUME_AUDIO_BG = "volumeAudioBG";
    private const string VOLUME_AUDIO_GAME = "volumeAudioGame";
    private float _volumeAudioGame = 0f;

    private GameObject _selectedCharacter;


    public float VolumeAudioGame { get => _volumeAudioGame; set => _volumeAudioGame = value; }
    public GameObject SelectedCharacter { get => _selectedCharacter; set => _selectedCharacter = value; }

    public bool GetPlayAudioBg()
    {
        return PlayerPrefs.GetInt(PLAY_AUDIO_BG, 1) == 1;
    }

    public void SetPlayAudioBg(bool isPlay)
    {
        PlayerPrefs.SetInt(PLAY_AUDIO_BG, isPlay ? 1 : 0);
    }

    public bool GetPlayAudioGame()
    {
        return PlayerPrefs.GetInt(PLAY_AUDIO_GAME, 1) == 1;
    }

    public void SetPlayAudioGame(bool isPlay)
    {
        PlayerPrefs.SetInt(PLAY_AUDIO_GAME, isPlay ? 1 : 0);
    }

    public void SetVolumeAudioGame(float volume)
    {
        PlayerPrefs.SetFloat(VOLUME_AUDIO_GAME, volume);
    }

    public float GetVolumeAudioGame()
    {
        return PlayerPrefs.GetFloat(VOLUME_AUDIO_GAME, 1);
    }

    public void SetVolumeAudioBG(float volume)
    {
        PlayerPrefs.SetFloat(VOLUME_AUDIO_BG, volume);
    }

    public float GetVolumeAudioBG()
    {
        return PlayerPrefs.GetFloat(VOLUME_AUDIO_BG, 1);
    }
}
