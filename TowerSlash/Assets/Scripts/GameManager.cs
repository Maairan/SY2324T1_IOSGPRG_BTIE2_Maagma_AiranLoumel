using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Start()
    {
        GameManager.instance = this;
    }

    public void StartGame()
    {
       GoToScene("GameplayScene");
    }

    public void GoToMainMenu()
    {
        GoToScene("MainMenuScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
