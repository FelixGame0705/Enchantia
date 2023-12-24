using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverController : MonoBehaviour
{
    [SerializeField] private ResultItemListController _resultController;
    [SerializeField] private GameOverStatsDisplayController _gameOverStatsDisplayController;
    [SerializeField] private TMP_Text _waveText;

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
        this.gameObject.SetActive(true);
    }

    private void UpdateWaveDisplay(){
        _waveText.text = string.Concat("Wave ",GamePlayController.Instance.CurrentWave.ToString());
    }
    
}
