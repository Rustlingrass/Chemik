using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class TubeHolder : ChemicalObject {
    public static TubeHolder Instance { get; private set; }

    public event EventHandler OnModelSwapped;
    [SerializeField] private ModelSwapSO[] basicsModelSwapSOArray;
    [SerializeField] private ModelSwapSO[] amphotericsModelSwapSOArray;
    [SerializeField] private Transform tubeHolderPlaceOne;
    [SerializeField] private Transform tubeHolderPlaceTwo;
    [SerializeField] private GameObject currentTubeGameObjectOne;
    [SerializeField] private GameObject currentTubeGameObjectTwo;

    private Tube currentTubeOne;
    private Tube currentTubeTwo;
    private LakmusIndicator lakmusIndicator;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        currentTubeOne = currentTubeGameObjectOne.GetComponent<Tube>();
        currentTubeTwo = currentTubeGameObjectTwo.GetComponent<Tube>();
    }
    public override void SetChemicalObjectParent(IChemicalObjectParent chemicalObjectParent) {
        //Debug.Log("Tube Holder cannot be picked up"); Success! It was a great idea
    }

    public void InteractAlternateTubeHolder(ChemicalObject chemicalObject) {
        switch (ChemikManager.Instance.GetExperimentName()) {
            case ChemikManager.Experiment.BasicsExperiment:
                Debug.Log(ChemikManager.Instance.GetBasicsExperimentState());
                if (ChemikManager.Instance.IsBasicsCheckingTheEnvironmentState()) {
                    if (chemicalObject.TryGetComponent<LakmusIndicator>(out lakmusIndicator)) {
                        lakmusIndicator.ChangeLakmusModelToBlue(currentTubeOne);
                        OnModelSwapped?.Invoke(this, EventArgs.Empty);
                    }
                } else {
                    foreach (ModelSwapSO basicsModelSwapSO in basicsModelSwapSOArray) {
                        //Check with every ModelSwapSO for BasicsExperiment
                        if (TryCheckAndChangeTube(basicsModelSwapSO, chemicalObject, currentTubeOne, currentTubeGameObjectOne, tubeHolderPlaceOne, out currentTubeOne, out currentTubeGameObjectOne)) {
                            OnModelSwapped?.Invoke(this, EventArgs.Empty);
                            break;
                        }
                    }
                }
                break;
            case ChemikManager.Experiment.AmphotericsExperiment:
                Debug.Log(ChemikManager.Instance.GetAmphotericsExperimentState());
                switch (ChemikManager.Instance.GetAmphotericsExperimentState()) {
                    default: break;
                    case ChemikManager.AmphotericsExperimentState.CheckingSecondTubeEnvironmentToBeBasic:
                        //Checking the environment of the basic
                        if (chemicalObject.TryGetComponent<LakmusIndicator>(out lakmusIndicator)) {
                            lakmusIndicator.ChangeLakmusModelToBlue(currentTubeTwo);
                            OnModelSwapped?.Invoke(this, EventArgs.Empty);
                        }
                        break;
                    case ChemikManager.AmphotericsExperimentState.CheckingFirstTubeEnvironmentToBeAcid:
                        // Checking the environment of the acid
                        if (chemicalObject.TryGetComponent<LakmusIndicator>(out lakmusIndicator)) {
                            lakmusIndicator.ChangeLakmusModelToRed(currentTubeOne);
                            OnModelSwapped?.Invoke(this, EventArgs.Empty);
                        }
                        break;
                    case ChemikManager.AmphotericsExperimentState.AddingNaOHToTheSecondTube:
                        foreach (ModelSwapSO amphotericsModelSwapSO in amphotericsModelSwapSOArray) {
                            //Check with every ModelSwapSO for AmphotericsExperiment
                            if (TryCheckAndChangeTube(amphotericsModelSwapSO, chemicalObject, currentTubeTwo, currentTubeGameObjectTwo, tubeHolderPlaceTwo, out currentTubeTwo, out currentTubeGameObjectTwo)) {
                                OnModelSwapped?.Invoke(this, EventArgs.Empty);
                                break;
                            }
                        }
                        break;
                    case ChemikManager.AmphotericsExperimentState.AddingAcidToTheFirstTube:
                        foreach (ModelSwapSO amphotericsModelSwapSO in amphotericsModelSwapSOArray) {
                            //Check with every ModelSwapSO for AmphotericsExperiment
                            if (TryCheckAndChangeTube(amphotericsModelSwapSO, chemicalObject, currentTubeOne, currentTubeGameObjectOne, tubeHolderPlaceOne, out currentTubeOne, out currentTubeGameObjectOne)) {
                                OnModelSwapped?.Invoke(this, EventArgs.Empty);
                                break;
                            }
                        }
                        break;
                    case ChemikManager.AmphotericsExperimentState.AddingNaOH:
                        //Pouring NaOH to both tubes
                        Debug.Log("Tube Holder line 57");
                        foreach (ModelSwapSO amphotericsModelSwapSO in amphotericsModelSwapSOArray) {
                            //Check with every ModelSwapSO for AmphotericsExperiment
                            if (TryCheckAndChangeTube(amphotericsModelSwapSO, chemicalObject, currentTubeOne, currentTubeGameObjectOne, tubeHolderPlaceOne, out currentTubeOne, out currentTubeGameObjectOne) && TryCheckAndChangeTube(amphotericsModelSwapSO, chemicalObject, currentTubeTwo, currentTubeGameObjectTwo, tubeHolderPlaceTwo, out currentTubeTwo, out currentTubeGameObjectTwo)) {
                                OnModelSwapped?.Invoke(this, EventArgs.Empty);
                                break;
                            }
                        }
                        break;
                }
                break;
        }
    }
    private bool TryCheckAndChangeTube(ModelSwapSO modelSwapSO, ChemicalObject chemicalObject, Tube currentTube, GameObject currentTubeGameObject, Transform tubeHolderPlace, out Tube currentTubeOut, out GameObject currentTubeGameObjectOut) {
        currentTubeOut = currentTube;
        currentTubeGameObjectOut = currentTubeGameObject;
        if (modelSwapSO.keyObject == chemicalObject.GetChemicalObjectSO()) {
            //Player holds a keyObject
            if (currentTubeOut.GetTubeSO() == modelSwapSO.inputObject) {
                //The TubeOne is matching the ModelSwapSO's inputObject
                Destroy(currentTubeGameObjectOut);
                currentTubeGameObjectOut = Instantiate(modelSwapSO.outputObject.objectPrefab, tubeHolderPlace);
                currentTubeOut = currentTubeGameObjectOut.GetComponent<Tube>();
                return true;
            }
        }
        return false;
    }
}