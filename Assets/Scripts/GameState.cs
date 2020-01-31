using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameState : MonoBehaviour
{
    int score = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore(int enemyScore)
    {
        score += enemyScore;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }

}
