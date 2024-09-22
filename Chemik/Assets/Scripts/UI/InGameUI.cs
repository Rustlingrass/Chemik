using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    private const string IN_GAME_HINTS_UI_TASK_CLEARED = "TaskCleared";
    private const string IN_GAME_UI_EXPERIMENT_FINISHED_MESSAGE = "ExperimentFinished";
    public static InGameUI Instance { get; private set; }
    
    [SerializeField] private GameObject inGameKeyHintsUI;
    [SerializeField] private GameObject inGameHintsUI;
    [SerializeField] private GameObject experimentFinishedMessageUI;
    [SerializeField] private GameObject[] inGameHintsUIArray;
    private int hintCounter = 0;
    private Animator hintsUIAnimator;
    private Animator inGameUIAnimator;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        inGameUIAnimator = GetComponent<Animator>();
        hintsUIAnimator = inGameHintsUI.GetComponent<Animator>();
        ChemikManager.Instance.OnGamePaused += ChemikManager_OnGamePaused;
        ChemikManager.Instance.OnGameUnpaused += ChemikManager_OnGameUnpaused;
        ChemikManager.Instance.OnStateChanged += ChemikManager_OnStateChanged;
    }

    private void ChemikManager_OnStateChanged(object sender, ChemikManager.OnStateChangedEventArgs e) {
        ChangeHintText();
        if (ChemikManager.Instance.IsExperimentFinished()) {
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
        hintsUIAnimator.SetTrigger(IN_GAME_HINTS_UI_TASK_CLEARED);
        inGameHintsUIArray[hintCounter].SetActive(false);
        hintCounter++;
    }

    private void ShowFinishedMessage() {
        //experimentFinishedMessageUI.SetActive(true);
        inGameUIAnimator.SetTrigger(IN_GAME_UI_EXPERIMENT_FINISHED_MESSAGE);
    }
}
