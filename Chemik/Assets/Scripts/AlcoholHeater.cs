using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholHeater : ChemicalObject, IChemicalObjectParent
{
    public static AlcoholHeater Instance {  get; private set; }

    public event EventHandler OnObjectHeated;
    
    [SerializeField] private ChemicalObjectSO keyChemicalObjectSO;
    [SerializeField] private GameObject ironSpoonHolder;
    [SerializeField] private GameObject alcoholHeaterLid;
    [SerializeField] private GameObject alcoholHeaterPerticlesHolder;
    private enum HeatingState {
        NotHeating,
        Heating,
        Heated
    };

    private HeatingState state;
    private ChemicalObject currentChemicalObject;
    private Animator animator;

    private float heatingTimerMax = 5.0f;
    private float heatingTimer = 0f;
    private void Awake() {
        Instance = this;
        state = HeatingState.NotHeating;
    }
    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void Update() {
        if (state == HeatingState.Heating) {
            heatingTimer += Time.deltaTime;
            if (heatingTimer >= heatingTimerMax) {
                heatingTimer = 0f;
                state = HeatingState.Heated;
                Debug.Log("Heated!");
                OnObjectHeated?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void SetChemicalObjectParent(IChemicalObjectParent chemicalObjectParent) {
        //Nothing
    }

    public void InteractAlternateAlcoholHeater(ChemicalObject chemicalObject) {
        if (chemicalObject?.GetChemicalObjectSO() == keyChemicalObjectSO && state == HeatingState.NotHeating && ChemikManager.Instance.GetAcidsExperimentState() == ChemikManager.AcidsExperimentState.HeatingTheSeraWithAlcoholHeater) {
            chemicalObject.SetChemicalObjectParent(this);
            state = HeatingState.Heating;
            alcoholHeaterLid.SetActive(false);
            alcoholHeaterPerticlesHolder.SetActive(true);
            currentChemicalObject.gameObject.layer = 0; //default layer
        } else if (chemicalObject == null && state == HeatingState.Heated) {
            currentChemicalObject.gameObject.layer = 7; //intaractable1 layer
            currentChemicalObject.SetChemicalObjectParent(Player.Instance);
            alcoholHeaterPerticlesHolder.SetActive(false);
            alcoholHeaterLid.SetActive(true);
        }
    }

    public Transform GetChemicalObjectFollowTransform() {
        return ironSpoonHolder.transform;
    }

    public bool HasChemicalObject() {
        return currentChemicalObject != null;
    }

    public ChemicalObject GetChemicalObject() {
        return currentChemicalObject;
    }

    public void SetChemicalObject(ChemicalObject chemicalObject) {
        currentChemicalObject = chemicalObject;
    }

    public void ClearChemicalObject() {
        currentChemicalObject = null;
    }
}
