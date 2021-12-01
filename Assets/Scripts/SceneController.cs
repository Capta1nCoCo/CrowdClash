using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Canvas levelFailedCanvas;
    [SerializeField] private Canvas levelCompletedCanvas;
    private int _currentSceneIndex;
    
    private void Awake()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        
        levelCompletedCanvas.gameObject.SetActive(false);
        levelFailedCanvas.gameObject.SetActive(false);
        
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
        levelCompletedCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
    private void GameOver()
    {
        levelFailedCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
