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
        public State state;
    }
    private bool isGamePaused = false;
    private bool mouseLocked = true;
    public enum State {
        ExperimentStarted,
        AddingWaterToTheTube,
        AddingCaOToTheTube,
        CheckingTheEnvironment,
        ExperimentFinished
    }
    public State experimentState;
    private void Awake() {
        Instance = this;
        experimentState = State.ExperimentStarted;
        ChangeExperimentStageState();
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            state = experimentState
        });
    }
    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        TubeHolder.Instance.OnModelSwapped += TubeHolder_OnModelSwapped;
        ToggleMouseState();
    }

    private void TubeHolder_OnModelSwapped(object sender, EventArgs e) {
        ChangeExperimentStageState();
        Debug.Log(experimentState);
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {  
            state = experimentState 
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
        switch (experimentState) {
            default: break;
            case State.ExperimentStarted:
                experimentState = State.AddingWaterToTheTube;
                break;
            case State.AddingWaterToTheTube:
                experimentState = State.AddingCaOToTheTube;
                break;
            case State.AddingCaOToTheTube:
                experimentState = State.CheckingTheEnvironment;
                break;
            case State.CheckingTheEnvironment:
                experimentState = State.ExperimentFinished;
                break;
        }
    }

    public bool IsCheckingTheEnvironmentState() {
        return experimentState == State.CheckingTheEnvironment;
    }

}