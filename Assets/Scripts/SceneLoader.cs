using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Initializations
    MusicPlayer musicReset;
    GameSession gameSession;

    private void Start()
    {
        musicReset = FindObjectOfType<MusicPlayer>();
        gameSession = FindObjectOfType<GameSession>();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1); // Still up for editting
    }

    public void LoadPlayAgain()
    {
        SceneManager.LoadScene(1);
        gameSession.ResetGameSession();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        gameSession.ResetGameSession();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game Over");

    }
}
