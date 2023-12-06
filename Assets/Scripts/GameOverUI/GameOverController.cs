using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverController : MonoBehaviour
{
    [SerializeField] private ResultItemListController _resultController;
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
        this.gameObject.SetActive(true);
    }
    
}
