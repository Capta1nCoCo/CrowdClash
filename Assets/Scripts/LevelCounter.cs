using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    private int _currentSceneIndex;

    private TextMeshProUGUI _counterText;

    private void Awake()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _counterText = GetComponent<TextMeshProUGUI>();
        _counterText.text = "Level " + _currentSceneIndex;

        GameEvents.GameOver += OnGameOver;
        GameEvents.Victory += OnVictory;
    }

    private void OnDestroy()
    {
        GameEvents.GameOver -= OnGameOver;
        GameEvents.Victory -= OnVictory;
    }

    private void OnGameOver()
    {
        gameObject.SetActive(false);
    }

    private void OnVictory()
    {
        gameObject.SetActive(false);
    }
}
