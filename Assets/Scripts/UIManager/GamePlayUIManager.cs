using DG.Tweening;
using UnityEngine;

public class GamePlayUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _waveShopPanel;

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private GameObject _settingPanel;

    [SerializeField]
    private GameOverController _gameOverControllerPanel;

    [SerializeField]
    private GameSettingController _gameSettingController;

    private void OnEnable()
    {
        this._gameOverControllerPanel = _waveShopPanel.GetComponent<GameOverController>();
        this._gameSettingController = _settingPanel.GetComponent<GameSettingController>();
    }

    public void RenderGameOverPanel(GAME_OVER_TYPE gameOverType)
    {
        RectTransform rect = _gameOverPanel.GetComponent<RectTransform>();
        GameOverController gameOverController = _gameOverPanel.GetComponent<GameOverController>();
        gameOverController.RenderUI(gameOverType);
        //rect.DOScale(Vector3.one, 0.5f)
        //    .SetEase(Ease.Linear)
        //    .OnPlay(() => Debug.Log("Animation started"))
        //    .OnKill(() => Debug.Log("Animation killed"))
        //    .OnComplete(() => Debug.Log("Animation complete!"));
        //rect.DOScale(Vector3.one, 0.5f)
        //    .SetEase(Ease.Linear)
        //    .OnComplete(() =>
        //    {
        //        gameOverController.RenderUI(gameOverType);
        //        Debug.Log("Animation complete!");
        //    });
    }

    public void RenderGameSettingPanel()
    {
        RectTransform rect = _settingPanel.gameObject.GetComponent<RectTransform>();
        var panelStatus = _gameSettingController.SettingPanelStatus;
        Vector3 currentLocation = _gameSettingController.OriginLocation;
        if (panelStatus == true)
        {
            _settingPanel.SetActive(false);
            //rect.DOAnchorPos3DY(currentLocation.y, 0.2f)
            //    .SetEase(Ease.InSine)
            //    .OnComplete(() => _settingPanel.SetActive(false));
        }
        else
        {
            _settingPanel.transform.localPosition = currentLocation;
            _settingPanel.SetActive(true);
            //rect.DOAnchorPos3DY(0, 0.2f).SetEase(Ease.InSine);
        }
    }

    public void RenderWaveShopPanel()
    {
        RectTransform rect = _waveShopPanel.GetComponent<RectTransform>();
        bool panelStatus = _waveShopPanel.gameObject.activeInHierarchy;
        float scale = panelStatus ? 0f : 1f;
        WaveShopMainController.Instance.UpdateMoney();
        rect.DOScale(scale, 0.5f)
            .SetEase(Ease.InElastic)
            .OnComplete(() => _waveShopPanel.SetActive(!panelStatus));
    }
}
