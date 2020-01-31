using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    GameState myGameState;
    TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        myGameState = FindObjectOfType<GameState>();
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = myGameState.GetScore().ToString();
    }
}
