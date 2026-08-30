using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button optionsButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(Options);
        exitButton.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Options()
    {
        
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Project/Scenes/Scene_General");
    }
    
}