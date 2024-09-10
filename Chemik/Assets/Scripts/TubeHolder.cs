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
    [SerializeField] private Transform tubeHolderPlace;
    [SerializeField] private GameObject currentTubeGameObjectOne;
    [SerializeField] private GameObject currentTubeGameObjectTwo;

    private Tube currentTubeOne;
    private Tube currentTubeTwo;
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
                    if (chemicalObject.TryGetComponent<LakmusIndicator>(out LakmusIndicator lakmusIndicator)) {
                        Debug.Log("TubeHolder 35");
                        lakmusIndicator.ChangeLakmusModelToBlue(currentTubeOne);
                        OnModelSwapped?.Invoke(this, EventArgs.Empty);
                    }
                } else {
                    foreach (ModelSwapSO basicsModelSwapSO in basicsModelSwapSOArray) {
                        //Check with every ModelSwapSO for BasicsExperiment
                        if (TryCheckAndChangeTube(basicsModelSwapSO, chemicalObject, currentTubeOne, currentTubeGameObjectOne, out currentTubeOne, out currentTubeGameObjectOne)) {
                            OnModelSwapped?.Invoke(this, EventArgs.Empty);
                            break;
                        }
                    }
                }
                break;
            case ChemikManager.Experiment.AmphotericsExperiment:
                //Interaction
                if (ChemikManager.Instance.GetAmphotericsExperimentState() == ChemikManager.AmphotericsExperimentState.AddingNaOH) {
                    //Pouring NaOH to both tubes
                    foreach (ModelSwapSO amphotericsModelSwapSO in amphotericsModelSwapSOArray) {
                        //Check with every ModelSwapSO for AmphotericsExperiment
                        TryCheckAndChangeTube(amphotericsModelSwapSO, chemicalObject, currentTubeOne, currentTubeGameObjectOne, out currentTubeTwo, out currentTubeGameObjectOne);
                        TryCheckAndChangeTube(amphotericsModelSwapSO, chemicalObject, currentTubeTwo, currentTubeGameObjectTwo, out currentTubeTwo, out currentTubeGameObjectTwo);
                        OnModelSwapped?.Invoke(this, EventArgs.Empty);
                    }
                } else if (ChemikManager.Instance.GetAmphotericsExperimentState() == ChemikManager.AmphotericsExperimentState.AddingAcidToTheFirstTube) {
                    foreach (ModelSwapSO amphotericsModelSwapSO in amphotericsModelSwapSOArray) {
                        //Check with every ModelSwapSO for AmphotericsExperiment
                        TryCheckAndChangeTube(amphotericsModelSwapSO, chemicalObject, currentTubeOne, currentTubeGameObjectOne, out currentTubeOne, out currentTubeGameObjectOne);
                        OnModelSwapped?.Invoke(this, EventArgs.Empty);
                    }
                } else if (ChemikManager.Instance.GetAmphotericsExperimentState() == ChemikManager.AmphotericsExperimentState.AddingNaOHToTheSecondTube) {
                    foreach (ModelSwapSO amphotericsModelSwapSO in amphotericsModelSwapSOArray) {
                        //Check with every ModelSwapSO for AmphotericsExperiment
                        TryCheckAndChangeTube(amphotericsModelSwapSO, chemicalObject, currentTubeTwo, currentTubeGameObjectTwo, out currentTubeTwo, out currentTubeGameObjectTwo);
                        OnModelSwapped?.Invoke(this, EventArgs.Empty);
                    }
                } else if (ChemikManager.Instance.GetAmphotericsExperimentState() == ChemikManager.AmphotericsExperimentState.CheckingFirstTubeEnvironmentToBeAcid) {
                    //Checking the environment of the acid
                    if (chemicalObject.TryGetComponent<LakmusIndicator>(out LakmusIndicator lakmusIndicator)) {
                        lakmusIndicator.ChangeLakmusModelToRed(currentTubeOne);
                        OnModelSwapped?.Invoke(this, EventArgs.Empty);
                    }
                } else if (ChemikManager.Instance.GetAmphotericsExperimentState() == ChemikManager.AmphotericsExperimentState.CheckingSecondTubeEnvironmentToBeBasic) {
                    //Checking the environment of the basic
                    if (chemicalObject.TryGetComponent<LakmusIndicator>(out LakmusIndicator lakmusIndicator)) {
                        lakmusIndicator.ChangeLakmusModelToBlue(currentTubeTwo);
                        OnModelSwapped?.Invoke(this, EventArgs.Empty);
                    }
                }
                    break;
        }
    }
    private bool TryCheckAndChangeTube(ModelSwapSO modelSwapSO, ChemicalObject chemicalObject, Tube currentTube, GameObject currentTubeGameObject, out Tube currentTubeOut, out GameObject currentTubeGameObjectOut) {
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