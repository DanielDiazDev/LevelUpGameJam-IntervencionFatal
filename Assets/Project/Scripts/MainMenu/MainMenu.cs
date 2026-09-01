using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Button optionsBackButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(Options);
        exitButton.onClick.AddListener(Exit);
        optionsBackButton.onClick.AddListener(OptionsBack);
        optionsButton.SetActive(false);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Options()
    {
        optionsPanel.SetActive(true);
    }

    private void OptionsBack()
    {
        optionsPanel.SetActive(false);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Project/Scenes/Scene_General");
    }

}