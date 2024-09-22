using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskHolder : Flask, IChemicalObjectParent
{
    public static FlaskHolder Instance { get; private set; }
    public event EventHandler OnModelChanged;

    [SerializeField] FlaskSwapSO[] flaskSwapSOArray;
    [SerializeField] ChemicalObjectSO heatedSpoonSO;
    [SerializeField] private Transform currentVisualHolder;
    [SerializeField] private Transform heatedSpoonHolder;
    [SerializeField] private GameObject reactedSpoonHolder;
    [SerializeField] private GameObject currentFlaskGameObject;
    
    private ChemicalObject currentChemicalObject;
    private Flask currentFlask;
    private float seraReactionTimerMax = 4f;
    private float seraReactionTimer = 0f;
    private bool isSeraPlaced = false;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        currentFlask = currentFlaskGameObject.GetComponent<Flask>();
    }
    private void Update() {
        if (isSeraPlaced) {
            seraReactionTimer += Time.deltaTime;
            if (seraReactionTimer >= seraReactionTimerMax) {
                isSeraPlaced=false;
                seraReactionTimer = 0f;
                currentChemicalObject.gameObject.SetActive(false);
                currentChemicalObject = null;
                reactedSpoonHolder.SetActive(true);
                OnModelChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void SetChemicalObjectParent(IChemicalObjectParent chemicalObjectParent) {
        //Nothing
    }
    public void InteractAlternateFlaskWithO2(ChemicalObject chemicalObject) {
        if (ChemikManager.Instance.GetAcidsExperimentState() == ChemikManager.AcidsExperimentState.PlacingSeraToFlaskWithO2) {
            if (chemicalObject.GetChemicalObjectSO() == heatedSpoonSO) {
                chemicalObject.SetChemicalObjectParent(this);
                isSeraPlaced = true;
            }
        } else {
            foreach (FlaskSwapSO flaskSwapSO in flaskSwapSOArray) {
                if (TryCheckAndChangeFlask(flaskSwapSO, chemicalObject, currentFlask, currentFlaskGameObject, currentVisualHolder, out currentFlask, out currentFlaskGameObject)) {
                    OnModelChanged?.Invoke(this, EventArgs.Empty);
                    break;
                }
            }
        }
    }
    private bool TryCheckAndChangeFlask(FlaskSwapSO flaskSwapSO, ChemicalObject chemicalObject, Flask currentFlask, GameObject currentFlaskGameObject, Transform flaskHolderPlace, out Flask currentFlaskOut, out GameObject currentFlaskGameObjectOut) {
        currentFlaskOut = currentFlask;
        currentFlaskGameObjectOut = currentFlaskGameObject;
        if (flaskSwapSO.keyObjectSO == chemicalObject.GetChemicalObjectSO()) {
            //Player holds a keyObject
            if (currentFlaskOut.GetChemicalObjectSO() == flaskSwapSO.inputChemicalObjectSO) {
                //The TubeOne is matching the ModelSwapSO's inputObject
                Destroy(currentFlaskGameObjectOut);
                currentFlaskGameObjectOut = Instantiate(flaskSwapSO.outputChemicalObjectSO.objectPrefab, flaskHolderPlace);
                currentFlaskOut = currentFlaskGameObjectOut.GetComponent<Flask>();
                return true;
            }
        }
        return false;
    }
    public void ClearChemicalObject() {
        currentChemicalObject = null;
    }

    public ChemicalObject GetChemicalObject() {
        return currentChemicalObject;
    }

    public Transform GetChemicalObjectFollowTransform() {
        return heatedSpoonHolder;
    }

    public bool HasChemicalObject() {
        return currentChemicalObject != null;
    }

    public void SetChemicalObject(ChemicalObject chemicalObject) {
        currentChemicalObject = chemicalObject;
    }

    public float GetReactionTimerFill() {
        float clampMin = 0f;
        float clampMax = 1f;
        return Math.Clamp(seraReactionTimer / seraReactionTimerMax, clampMin, clampMax);
    }
}
