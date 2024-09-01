using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance { get; private set; }

    [SerializeField] private GameObject levelSelectionMenu;
    [SerializeField] private GameObject aboutWindow;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button aboutButton;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        startButton.onClick.AddListener(() => {
            levelSelectionMenu.SetActive(true);
            gameObject.SetActive(false);
            LevelSelectionUI.Instance.SetSelection();
        });
        settingsButton.onClick.AddListener(() => {
            settingsMenu.SetActive(true);
            gameObject.SetActive(false);
        });
        aboutButton.onClick.AddListener(() => {
            aboutWindow.SetActive(true);
            gameObject.SetActive(false);
        });
        exitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
