using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] private TextMeshProUGUI _resultText;

    void Update()
    {   
        if (_resultText == null)
            return;
        
        if(GameManager.instance.player._isWinner)
            _resultText.text = "Winner winner, chimken dinner!";
        else
            _resultText.text = "You lost. Better luck next time!";       
    }

    private void Awake()
    {
        MenuManager.instance = this;
    }

    public void OnStartGameButtonPressed()
    {
        LoadScene("GameplayScene");
    }

    public void OnExitGameButtonPressed()
    {
        Application.Quit();
    }

    public void OnMainMenuButtonPressed()
    {
        LoadScene("MainMenuScene");
    }

    public void OnRetryButtonPressed()
    {
        LoadScene("GameplayScene");
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
