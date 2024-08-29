using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakmusIndicator : ChemicalObject {

    [SerializeField] private LakmusModelSwapSO lakmusModelSwapSO;

    private GameObject spawnedIndicator;
    private ChemicalObject spawnedIndicatorChemicalObject;

    public void ChangeLakmusModel() {
        if (lakmusModelSwapSO.keyObject == TubeHolder.Instance.GetTubeSO() && transform.GetComponent<ChemicalObject>().GetChemicalObjectSO() == lakmusModelSwapSO.inputObject) {
            gameObject.GetComponentInChildren<SelectedChemicalObject>();
            Player.Instance.ClearChemicalObject();
            spawnedIndicator = Instantiate(lakmusModelSwapSO.outputObject.objectPrefab, Player.Instance.GetChemicalObjectFollowTransform(), false);
            spawnedIndicatorChemicalObject = spawnedIndicator.GetComponent<ChemicalObject>();
            Player.Instance.SetSelectedObject(spawnedIndicatorChemicalObject);
            Debug.Log(spawnedIndicator);
            Debug.Log(spawnedIndicatorChemicalObject);
            spawnedIndicatorChemicalObject.SetChemicalObjectParent(Player.Instance);
            this.gameObject.SetActive(false);
        }
    }
}
