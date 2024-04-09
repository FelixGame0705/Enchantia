using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GamePlayUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _waveShopPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private GameOverController _gameOverControllerPanel;
    [SerializeField] private WaveShopMainController _waveShopMainController;
    [SerializeField] private GameSettingController _gameSettingController;
    
    private void OnEnable()
    {
        this._gameOverControllerPanel = _waveShopPanel.GetComponent<GameOverController>();
        this._waveShopMainController = _waveShopPanel.GetComponent<WaveShopMainController>();
        this._gameSettingController = _settingPanel.GetComponent<GameSettingController>();
    }
    
    public void RenderWaveShopPanel(){
        RectTransform rect = _waveShopPanel.GetComponent<RectTransform>();
        rect.DOScale(Vector3.one, 2f);
    }

    public void RenderGameOverPanel(GAME_OVER_TYPE gameOverType){
        RectTransform rect = _gameOverPanel.GetComponent<RectTransform>();
        GameOverController gameOverController = _gameOverPanel.GetComponent<GameOverController>();
        rect.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear).OnComplete(() => gameOverController.RenderUI(gameOverType));
    }

    public void RenderGameSettingPanel(){
        RectTransform rect = _settingPanel.gameObject.GetComponent<RectTransform>();
        var panelStatus = _gameSettingController.SettingPanelStatus;
        Vector3 currentLocation = _gameSettingController.OriginLocation;
        if(panelStatus == true){
            rect.DOAnchorPos3DY(currentLocation.y, 0.2f).SetEase(Ease.InSine).OnComplete(() => _settingPanel.SetActive(false));
        }else{
            _settingPanel.transform.localPosition = currentLocation;
            _settingPanel.SetActive(true);
            rect.DOAnchorPos3DY(0, 0.2f).SetEase(Ease.InSine);
        }
    }
}
