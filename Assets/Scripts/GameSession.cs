using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    //State Changes
    [SerializeField] int totalScore = 0;

    private void Awake()
    {
        int countGameSession = FindObjectsOfType<GameSession>().Length;
        if (countGameSession > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ScoreAdder(int value)
    {
        totalScore += value;
    }

    public int GetScore()
    {
        return totalScore;
    }

    public void ResetGameSession()
    {
        Destroy(gameObject);
    }
}
