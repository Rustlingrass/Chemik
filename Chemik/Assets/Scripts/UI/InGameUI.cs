using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance { get; private set; }
    
    [SerializeField] private GameObject inGameKeyHintsUI;
    [SerializeField] private GameObject inGameHintsUI;
    [SerializeField] private GameObject experimentFinishedMessageUI;
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
        HideFinishedMessage();
    }

    private void ChemikManager_OnStateChanged(object sender, ChemikManager.OnStateChangedEventArgs e) {
        ChangeHintText();
        if (e.basicsExperimentState == ChemikManager.BasicsExperimentState.ExperimentFinished) {
            ShowFinishedMessage();
            inGameHintsUI.SetActive(false);
        }
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

    private void ShowFinishedMessage() {
        experimentFinishedMessageUI.SetActive(true);
    }
    private void HideFinishedMessage() {
        experimentFinishedMessageUI.SetActive(false);
    }
}
