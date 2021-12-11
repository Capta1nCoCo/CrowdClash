using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int _currentScore;
    
    private void Awake()
    {        
        GameEvents.Victory += OnVictory;
        DisplayCurrentScore();
    }

    private void OnDestroy()
    {
        GameEvents.Victory -= OnVictory;
    }

    private void DisplayCurrentScore()
    {
        _currentScore = PlayerPrefs.GetInt("score");
        scoreText.text = _currentScore.ToString();
    }

    private void OnVictory()
    {
        DisplayCurrentScore();
    }
}
