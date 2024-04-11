using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class GameOverController : MonoBehaviour
{
    [SerializeField] private ResultItemListController _resultController;
    [SerializeField] private GameOverStatsDisplayController _gameOverStatsDisplayController;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private TMP_Text timeCount;
    [SerializeField] private Image _characterImage;
    [SerializeField] private TMP_Text _resultWaveText;
    [SerializeField] private Color _winColor;
    [SerializeField] private Color _lostColor;
    [SerializeField] private string _textWon;
    [SerializeField] private string _textLost;
    [SerializeField] private GAME_OVER_TYPE _gameOverType;

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
    public void RenderUI(GAME_OVER_TYPE type)
    {
        _resultController.Render();
        var character = GameDataController.Instance.CurrentGamePlayData.Character;
        _gameOverStatsDisplayController.LoadData(character.GetComponent<CharacterController>().CharacterModStats);
        // _gameOverStatsDisplayController.LoadData(GamePlayController.Instance.GetCharacterController().CharacterModStats);
        LoadTime();
        // var charBaseInfo = GamePlayController.Instance.Character.GetComponent<CharacterBaseInfo>();
        var charBaseInfo = character.GetComponent<CharacterBaseInfo>();
        charBaseInfo.Load();
        _characterImage.sprite = charBaseInfo.CharacterSprite;
        switch(type){
            case GAME_OVER_TYPE.ENDLESS:
            case GAME_OVER_TYPE.LOST:
                _resultWaveText.color = _lostColor;
                _resultWaveText.text = _textLost;
            break;
            case GAME_OVER_TYPE.WON:
                _resultWaveText.color = _winColor;
                _resultWaveText.text = _textWon;  
            break;
        }  
        this.gameObject.SetActive(true);
        UpdateWaveDisplay();
    }

    private void UpdateWaveDisplay(){
        var result = _gameOverType == GAME_OVER_TYPE.ENDLESS ? string.Concat("Wave ",GameDataController.Instance.CurrentGamePlayData.CurrentWave.ToString(), " - Endless") : string.Concat("Wave ",GameDataController.Instance.CurrentGamePlayData.CurrentWave.ToString());
        _waveText.text = result;
    }

    private void LoadTime()
    {
        int minutes = Mathf.FloorToInt(GamePlayController.Instance.TimePlay / 60);
        int seconds = Mathf.FloorToInt(GamePlayController.Instance.TimePlay % 60);
        timeCount.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
