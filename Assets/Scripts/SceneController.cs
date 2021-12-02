using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;
    private int _currentSceneIndex;
    
    private void Awake()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        
        GameEvents.Victory += Victory;
        GameEvents.GameOver += GameOver;
    }

    private void OnDestroy()
    {
        GameEvents.Victory -= Victory;
        GameEvents.GameOver -= GameOver;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(_currentSceneIndex + 1);
    }
    
    public void TryAgain()
    {
        SceneManager.LoadScene(_currentSceneIndex);
    }

    private void Victory()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }
    
    private void GameOver()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
