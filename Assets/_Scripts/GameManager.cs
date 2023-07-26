using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private bool _isGamePaused;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are more than one GameManager");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnGamePaused += GameInput_OnGamePaused;
    }

    private void GameInput_OnGamePaused(object sender, EventArgs e)
    {
        ToggleGamePause();
    }

    public void ToggleGamePause()
    {
        _isGamePaused = !_isGamePaused;

        if (_isGamePaused)
        {
            OnGamePaused?.Invoke(this,EventArgs.Empty);
            Time.timeScale = 0;
        }
        else
        {
            OnGameUnpaused?.Invoke(this,EventArgs.Empty);
            Time.timeScale = 1;
        }
    }
}
