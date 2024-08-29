using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance { get; private set; }
    
    [SerializeField] private GameObject inGameKeyHintsUI;
    [SerializeField] private GameObject inGameHintsUI;
    [SerializeField] private TextMeshProUGUI inGameHintsUIText;
    [SerializeField] private ExperimentHintsSO experimentHintsSO;
    private int hintCounter = 0;
    private void Awake() {
        Instance = this;
        hintCounter++;
    }
    private void Start() {
        ChemikManager.Instance.OnGamePaused += ChemikManager_OnGamePaused;
        ChemikManager.Instance.OnGameUnpaused += ChemikManager_OnGameUnpaused;
        ChemikManager.Instance.OnStateChanged += ChemikManager_OnStateChanged;
    }

    private void ChemikManager_OnStateChanged(object sender, ChemikManager.OnStateChangedEventArgs e) {
        ChangeHintText();
    }

    private void ChemikManager_OnGameUnpaused(object sender, System.EventArgs e) {
        ShowHints();
    }

    private void ChemikManager_OnGamePaused(object sender, System.EventArgs e) {
        HideHints();
    }

    public void ShowHints() {
        inGameKeyHintsUI.SetActive(true);
        inGameHintsUI.SetActive(true);
    }
    private void HideHints() {
        inGameKeyHintsUI.SetActive(false);
        inGameHintsUI.SetActive(false);
    }

    private void ChangeHintText() {
        inGameHintsUIText.text = experimentHintsSO.experimentHintsList[hintCounter];
        hintCounter++;
    }
}
