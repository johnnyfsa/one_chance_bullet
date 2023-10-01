using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameWinUI;

    [SerializeField] Button restartButton;

    private void Start() {
        GameManager.Instance.OnGameOver += ShowGameOverUI;
        GameManager.Instance.OnGameRestart += HideGameOverUI;
        restartButton.onClick.AddListener(GameManager.Instance.RestartGame);

        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
    }

    private void OnDisable() {
        GameManager.Instance.OnGameOver -= ShowGameOverUI;
        GameManager.Instance.OnGameRestart -= HideGameOverUI;
    }

    private void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
        
        // if (GameManager.Instance.Score >= 100)
        // {
        //     gameWinUI.SetActive(true);
        // }
        // else
        // {
        //     gameOverUI.SetActive(true);
        // }
    }

    private void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
    }
}
