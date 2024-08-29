using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    public static PauseMenuUI Instance { get; private set; }
    
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        resumeButton.onClick.AddListener(() => {
            Hide();
            ChemikManager.Instance.TogglePauseGame();
            ChemikManager.Instance.ToggleMouseState();
            InGameUI.Instance.ShowHints();
        });
        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(0);
        });
        ChemikManager.Instance.OnGamePaused += ChemikManager_OnGamePaused;
        ChemikManager.Instance.OnGameUnpaused += ChemikManager_OnGameUnpaused;
        
        Hide();
    }

    private void ChemikManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void ChemikManager_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    public void Hide() {
        gameObject.SetActive(false);
        ChemikManager.Instance.ToggleMouseState();
    }
    public void Show() {
        gameObject.SetActive(true);
        ChemikManager.Instance.ToggleMouseState();
    }
}
