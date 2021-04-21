using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{

    // Initializations
    TextMeshProUGUI scoreText;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "Game Over")
        {
            scoreText.text = $"SCORE: {gameSession.GetScore()}";
        } else
        {
            scoreText.text = $"{gameSession.GetScore()}";
        }
    }
}
