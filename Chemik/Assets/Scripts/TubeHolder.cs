using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class TubeHolder : ChemicalObject {
    public static TubeHolder Instance { get; private set; }

    public event EventHandler OnModelSwapped;
    [SerializeField] private ModelSwapSO[] modelSwapSOArray;
    [SerializeField] private Transform tubeHolderPlace;
    [SerializeField] private GameObject currentTubeGameObject;

    private Tube currentTube;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        currentTube = currentTubeGameObject.GetComponent<Tube>();
    }
    public override void SetChemicalObjectParent(IChemicalObjectParent chemicalObjectParent) {
        //Debug.Log("Tube Holder cannot be picked up"); Success! It was a great idea
    }

    public void InteractAlternateTubeHolder(ChemicalObject chemicalObject) {
        if (ChemikManager.Instance.IsCheckingTheEnvironmentState()) {
            if (chemicalObject.TryGetComponent<LakmusIndicator>(out LakmusIndicator lakmusIndicator)) {
                lakmusIndicator.ChangeLakmusModel();
                OnModelSwapped?.Invoke(this, EventArgs.Empty);
            }
        } else {
            foreach (ModelSwapSO modelSwapSO in modelSwapSOArray) {
                //Check with every ModelSwapSO
                if (modelSwapSO.keyObject == chemicalObject.GetChemicalObjectSO()) {
                    //Player holds a keyObject and TubeHolder has a Tube
                    if (currentTube.GetTubeSO() == modelSwapSO.inputObject) {
                        //TubeHolder has right inputObject
                        Destroy(currentTubeGameObject);
                        currentTubeGameObject = Instantiate(modelSwapSO.outputObject.objectPrefab, tubeHolderPlace);
                        currentTube = currentTubeGameObject.GetComponent<Tube>();
                        OnModelSwapped?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
    public TubeSO GetTubeSO() {
        return currentTube.GetTubeSO();
    }
}