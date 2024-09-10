using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemikManager : MonoBehaviour {

    public static ChemikManager Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs {
        public BasicsExperimentState basicsExperimentState;
        public AmphotericsExperimentState amphotericsExperimentState;
    }
    private bool isGamePaused = false;
    private bool mouseLocked = true;
    public enum Experiment {
        BasicsExperiment,
        AcidsExperiment,
        AmphotericsExperiment
    }
    public enum BasicsExperimentState {
        ExperimentStarted,
        AddingWaterToTheTube,
        AddingCaOToTheTube,
        CheckingTheEnvironment,
        ExperimentFinished
    }
    /*public enum AcidsExperimentState {
        ExperimentStarted,
        ExperimentFinished
    }*/
    public enum AmphotericsExperimentState {
        ExperimentStarted,
        AddingNaOH,
        AddingAcidToTheFirstTube,
        AddingNaOHToTheSecondTube,
        CheckingFirstTubeEnvironmentToBeAcid,
        CheckingSecondTubeEnvironmentToBeBasic,
        ExperimentFinished
    }
    [SerializeField] private BasicsExperimentState basicsExperimentState;
    [SerializeField] private AmphotericsExperimentState amphotericsExperimentState;
    [SerializeField] private Experiment experimentName;
    private void Awake() {
        Instance = this;
        basicsExperimentState = BasicsExperimentState.ExperimentStarted;
        amphotericsExperimentState = AmphotericsExperimentState.ExperimentStarted;
        ChangeExperimentStageState();
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            basicsExperimentState = basicsExperimentState,
            amphotericsExperimentState = amphotericsExperimentState
        });
    }
    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        TubeHolder.Instance.OnModelSwapped += TubeHolder_OnModelSwapped;
        ToggleMouseState();
    }

    private void TubeHolder_OnModelSwapped(object sender, EventArgs e) {
        ChangeExperimentStageState();
        Debug.Log(basicsExperimentState);
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {  
            basicsExperimentState = basicsExperimentState,
            amphotericsExperimentState = amphotericsExperimentState
        });
    }


    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            Time.timeScale = 0f;

            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;

            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
    public void ToggleMouseState() {
        if (mouseLocked) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseLocked = false;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouseLocked = true;
        }
    }
    private void ChangeExperimentStageState() {
        switch (experimentName) {
            default: break;
            case Experiment.BasicsExperiment: {
                    switch (basicsExperimentState) {
                        default: break;
                        case BasicsExperimentState.ExperimentStarted:
                            basicsExperimentState = BasicsExperimentState.AddingWaterToTheTube;
                            break;
                        case BasicsExperimentState.AddingWaterToTheTube:
                            basicsExperimentState = BasicsExperimentState.AddingCaOToTheTube;
                            break;
                        case BasicsExperimentState.AddingCaOToTheTube:
                            basicsExperimentState = BasicsExperimentState.CheckingTheEnvironment;
                            break;
                        case BasicsExperimentState.CheckingTheEnvironment:
                            basicsExperimentState = BasicsExperimentState.ExperimentFinished;
                            break;
                    }
                    break;
            }
            case Experiment.AmphotericsExperiment: {
                    switch (amphotericsExperimentState) {
                        default: break;
                        case AmphotericsExperimentState.ExperimentStarted:
                            amphotericsExperimentState = AmphotericsExperimentState.AddingNaOH;
                            break;
                        case AmphotericsExperimentState.AddingNaOH:
                            amphotericsExperimentState = AmphotericsExperimentState.AddingAcidToTheFirstTube; 
                            break;
                        case AmphotericsExperimentState.AddingAcidToTheFirstTube:
                            amphotericsExperimentState = AmphotericsExperimentState.AddingNaOHToTheSecondTube;
                            break;
                        case AmphotericsExperimentState.AddingNaOHToTheSecondTube:
                            amphotericsExperimentState = AmphotericsExperimentState.CheckingFirstTubeEnvironmentToBeAcid;
                            break;
                        case AmphotericsExperimentState.CheckingFirstTubeEnvironmentToBeAcid:
                            amphotericsExperimentState = AmphotericsExperimentState.CheckingSecondTubeEnvironmentToBeBasic;
                            break;
                        case AmphotericsExperimentState.CheckingSecondTubeEnvironmentToBeBasic:
                            amphotericsExperimentState = AmphotericsExperimentState.ExperimentFinished;
                            break;
                    }
                    break;
                }
    }
    }

    public bool IsBasicsCheckingTheEnvironmentState() {
        return basicsExperimentState == BasicsExperimentState.CheckingTheEnvironment;
    }

    public Experiment GetExperimentName() {
        return experimentName;
    }
    public AmphotericsExperimentState GetAmphotericsExperimentState() {
        return amphotericsExperimentState;
    }
    public BasicsExperimentState GetBasicsExperimentState() {
        return basicsExperimentState;
    }
}