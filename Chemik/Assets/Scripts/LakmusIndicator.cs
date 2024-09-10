using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakmusIndicator : ChemicalObject {

    [SerializeField] private LakmusModelSwapSO lakmusToBlueModelSwapSO;
    [SerializeField] private LakmusModelSwapSO lakmusToRedModelSwapSO;
    private GameObject spawnedIndicator;
    private ChemicalObject spawnedIndicatorChemicalObject;

    public void ChangeLakmusModelToBlue(Tube checkingTube) {
        Debug.Log(lakmusToBlueModelSwapSO.keyObject);
        Debug.Log(checkingTube.GetTubeSO());
        if (lakmusToBlueModelSwapSO.keyObject == checkingTube.GetTubeSO() && transform.GetComponent<ChemicalObject>().GetChemicalObjectSO() == lakmusToBlueModelSwapSO.inputObject) {
            gameObject.GetComponentInChildren<SelectedChemicalObject>();
            Player.Instance.ClearChemicalObject();
            spawnedIndicator = Instantiate(lakmusToBlueModelSwapSO.outputObject.objectPrefab, Player.Instance.GetChemicalObjectFollowTransform(), false);
            spawnedIndicatorChemicalObject = spawnedIndicator.GetComponent<ChemicalObject>();
            Player.Instance.SetSelectedObject(spawnedIndicatorChemicalObject);
            Debug.Log(spawnedIndicator);
            Debug.Log(spawnedIndicatorChemicalObject);
            spawnedIndicatorChemicalObject.SetChemicalObjectParent(Player.Instance);
            this.gameObject.SetActive(false);
        }
    }
    public void ChangeLakmusModelToRed(Tube checkingTube) {
        if (lakmusToRedModelSwapSO.keyObject == checkingTube.GetTubeSO() && transform.GetComponent<ChemicalObject>().GetChemicalObjectSO() == lakmusToBlueModelSwapSO.inputObject) {
            gameObject.GetComponentInChildren<SelectedChemicalObject>();
            Player.Instance.ClearChemicalObject();
            spawnedIndicator = Instantiate(lakmusToRedModelSwapSO.outputObject.objectPrefab, Player.Instance.GetChemicalObjectFollowTransform(), false);
            spawnedIndicatorChemicalObject = spawnedIndicator.GetComponent<ChemicalObject>();
            Player.Instance.SetSelectedObject(spawnedIndicatorChemicalObject);
            Debug.Log(spawnedIndicator);
            Debug.Log(spawnedIndicatorChemicalObject);
            spawnedIndicatorChemicalObject.SetChemicalObjectParent(Player.Instance);
            this.gameObject.SetActive(false);
        }
    }
}
