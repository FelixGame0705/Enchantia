using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameOverController : MonoBehaviour
{
    [SerializeField] private ResultItemListController _resultController;
    [SerializeField] private GameOverStatsDisplayController _gameOverStatsDisplayController;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private TMP_Text timeCount;
    [SerializeField] private Image _characterImage;

    private void OnEnable()
    {
        UpdateWaveDisplay();
    }
    private void Update()
    {
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void OnClickedBackToMenu()
    {
        MenuController.Instance.ReturnToMenu();
    }
    public void OnClickedReplay(){
        SceneManager.LoadScene("GamePlay");
    }

    public void OnClickedNewRun(){
        SceneManager.LoadScene("GamePlay");
    }
    public void RenderUI()
    {
        _resultController.Render();
        _gameOverStatsDisplayController.LoadData(GamePlayController.Instance.GetCharacterController().CharacterModStats);
        LoadTime();
        var charBaseInfo = GamePlayController.Instance.Character.GetComponent<CharacterBaseInfo>();
        charBaseInfo.Load();
        _characterImage.sprite = charBaseInfo.CharacterSprite;
            
        this.gameObject.SetActive(true);
    }

    private void UpdateWaveDisplay(){
        _waveText.text = string.Concat("Wave ",GamePlayController.Instance.CurrentWave.ToString());
    }

    private void LoadTime()
    {
        int minutes = Mathf.FloorToInt(GamePlayController.Instance.TimePlay / 60);
        int seconds = Mathf.FloorToInt(GamePlayController.Instance.TimePlay % 60);
        timeCount.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
