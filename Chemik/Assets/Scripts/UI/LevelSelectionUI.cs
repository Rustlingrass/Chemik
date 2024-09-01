using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour
{
    public static LevelSelectionUI Instance { get; private set; }
    [SerializeField] private Transform mainMenuUI;
    [SerializeField] private Button basicsExperimentButton;
    [SerializeField] private Button acidsExperimentButton;
    [SerializeField] private Button amphotericsExperimentButton;
    [SerializeField] private Button backButton;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        basicsExperimentButton.onClick.AddListener(() => {
            Loader.LoadingScreen(2);
        });
        acidsExperimentButton.onClick.AddListener(() => {
            Loader.LoadingScreen(3);
        });
        amphotericsExperimentButton.onClick.AddListener(() => {
            Loader.LoadingScreen(4);
        });
        backButton.onClick.AddListener(() => {
            mainMenuUI.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
    }

    public void SetSelection() {
        basicsExperimentButton.Select();
    }
}
