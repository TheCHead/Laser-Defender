using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;

    private void Start()
    {

    }

    public void LoadStartGameScene()
    {
        FindObjectOfType<MusicPlayer>().ResetMusic();
        SceneManager.LoadScene(0);
    }

    public void LoadMainScene()
    {
        FindObjectOfType<MusicPlayer>().ResetMusic();
        FindObjectOfType<GameState>().ResetScore();
        SceneManager.LoadScene("Main");
    }
    public void LoadGameOverScene()
    {
        StartCoroutine(LoadGameOverWithDelay());  
    }

    IEnumerator LoadGameOverWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
