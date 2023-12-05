using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverController : MonoBehaviour
{
    [SerializeField] private ResultItemListController _resultController;
    [SerializeField] private bool _isOver = false;
    private void Update()
    {
        if (_isOver) RenderUI();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void OnClickedBackToMenu()
    {

    }
    public void RenderUI()
    {
        _resultController.Render();
        this.gameObject.SetActive(true);
    }
}
