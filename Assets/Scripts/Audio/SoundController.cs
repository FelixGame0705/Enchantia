using CarterGames.Assets.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [SerializeField] private AudioClip _musicGamePlayBG;
    public void PlaySound(string soundName)
    {
        bool isPlay = GameData.Instance.GetPlayAudioGame();
        AudioManager.instance.Play(soundName, volume: (isPlay ? 1 : 0));
    }

    public AudioClip GetMusicGamePlayBG()
    {
        return _musicGamePlayBG;
    }

    
}
