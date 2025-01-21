using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerSO", menuName = "ScriptableObjects/GameManagerSO", order = 1)]
public class GameManagerSO : ScriptableObject
{
    public event Action<int> OnButtonPressed;
    public event Action OnPlayerDeath;
    public event Action OnGameStart;
    public event Action OnGameEnd;

    private void OnEnable()
    {
        OnButtonPressed = null;
        OnPlayerDeath = null;
        OnGameStart = null;
        OnGameEnd = null;
    }

    public void ButtonPressed(int buttonId)
    {
        OnButtonPressed?.Invoke(buttonId);
    }

    public void PlayerDied()
    {
        OnPlayerDeath?.Invoke();
        RestartLevel();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        OnGameStart?.Invoke();
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        OnGameEnd?.Invoke();
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
