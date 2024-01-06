using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameSettingController : MonoBehaviour
{
    [SerializeField] private Slider _bgm;
    [SerializeField] private Slider _sfx;

    private void Start()
    {
        _bgm.onValueChanged.AddListener(this.OnModifyBGMValue);
        _sfx.onValueChanged.AddListener(this.OnModifySFXValue);
    }

    private void OnModifyBGMValue(float bfmValue)
    {
        Debug.Log("Change BGM value " + bfmValue);
    }

    private void OnModifySFXValue(float sfxVlue)
    {
        Debug.Log("Change BGM value " + sfxVlue);
    }

    public void OnClickBackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnCickRestart()
    {

    }

    public void OnClickResume()
    {

    }
    private void OnDestroy()
    {
        _bgm.onValueChanged.RemoveAllListeners();
        _sfx.onValueChanged.RemoveAllListeners();
    }
}
