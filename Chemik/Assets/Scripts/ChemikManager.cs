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
        public AcidsExperimentState acidsExperimentState;
    }
    [SerializeField] private ExperimentHintsSOList experimentHintsSOList;
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
    public enum AcidsExperimentState {
        ExperimentStarted,
        HeatingTheSeraWithAlcoholHeater,
        PlacingSeraToFlaskWithO2,
        AddingWaterToFlaskWithSO2,
        CheckingTheEnvironmentWithMetil,
        ExperimentFinished
    }
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
    [SerializeField] private AcidsExperimentState acidsExperimentState;
    [SerializeField] private AmphotericsExperimentState amphotericsExperimentState;
    [SerializeField] private Experiment experimentName;
    private void Awake() {
        Instance = this;
        basicsExperimentState = BasicsExperimentState.ExperimentStarted;
        amphotericsExperimentState = AmphotericsExperimentState.ExperimentStarted;
        acidsExperimentState = AcidsExperimentState.ExperimentStarted;
        ChangeExperimentStageState();
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            basicsExperimentState = basicsExperimentState,
            amphotericsExperimentState = amphotericsExperimentState,
            acidsExperimentState = acidsExperimentState
        });
    }
    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        if (TubeHolder.Instance != null) {
            TubeHolder.Instance.OnModelSwapped += TubeHolder_OnModelSwapped;
        }
        if (AlcoholHeater.Instance != null) {
            AlcoholHeater.Instance.OnObjectHeated += AlcoholHeater_OnObjectHeated;
        }
        if (FlaskHolder.Instance != null) {
            FlaskHolder.Instance.OnModelChanged += FlaskHolder_OnModelChanged;
        }
        ToggleMouseState();
    }

    private void FlaskHolder_OnModelChanged(object sender, EventArgs e) {
        ChangeExperimentStageState();
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            basicsExperimentState = basicsExperimentState,
            amphotericsExperimentState = amphotericsExperimentState,
            acidsExperimentState = acidsExperimentState
        });
    }

    private void AlcoholHeater_OnObjectHeated(object sender, EventArgs e) {
        ChangeExperimentStageState();
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            basicsExperimentState = basicsExperimentState,
            amphotericsExperimentState = amphotericsExperimentState,
            acidsExperimentState = acidsExperimentState
        });
    }

    private void TubeHolder_OnModelSwapped(object sender, EventArgs e) {
        ChangeExperimentStageState();
        Debug.Log(basicsExperimentState);
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {  
            basicsExperimentState = basicsExperimentState,
            amphotericsExperimentState = amphotericsExperimentState,
            acidsExperimentState = acidsExperimentState
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
            case Experiment.BasicsExperiment:
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
            case Experiment.AmphotericsExperiment:
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
            case Experiment.AcidsExperiment:
                switch (acidsExperimentState) {
                    default: break;
                    case AcidsExperimentState.ExperimentStarted:
                        acidsExperimentState = AcidsExperimentState.HeatingTheSeraWithAlcoholHeater;
                        break;
                    case AcidsExperimentState.HeatingTheSeraWithAlcoholHeater:
                        acidsExperimentState = AcidsExperimentState.PlacingSeraToFlaskWithO2;
                        break;
                    case AcidsExperimentState.PlacingSeraToFlaskWithO2:
                        acidsExperimentState = AcidsExperimentState.AddingWaterToFlaskWithSO2;
                        break;
                    case AcidsExperimentState.AddingWaterToFlaskWithSO2:
                        acidsExperimentState = AcidsExperimentState.CheckingTheEnvironmentWithMetil;
                        break;
                    case AcidsExperimentState.CheckingTheEnvironmentWithMetil:
                        acidsExperimentState = AcidsExperimentState.ExperimentFinished;
                        break;
                }
                break;
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
    public AcidsExperimentState GetAcidsExperimentState() {
        return acidsExperimentState;
    }

    public ExperimentHintsSOList GetExperimentHintsSOList() {
        return experimentHintsSOList;
    }
    public bool IsExperimentFinished() {
        return basicsExperimentState == BasicsExperimentState.ExperimentFinished || acidsExperimentState == AcidsExperimentState.ExperimentFinished || amphotericsExperimentState == AmphotericsExperimentState.ExperimentFinished;
    }
}