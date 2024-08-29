using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedChemicalObject : MonoBehaviour {
    [SerializeField] private ChemicalObject chemicalObject;
    [SerializeField] private GameObject[] selectedChemicalObjectVisualArray;
    private void Start() {
        Player.Instance.OnSelectedObjectChanged += Player_OnSelectedObjectChanged;

        Hide();
    }

    private void Player_OnSelectedObjectChanged(object sender, Player.OnSelectedObjectChangedEventArgs e) {
        if (selectedChemicalObjectVisualArray != null) {
            if (e.selectedObject == chemicalObject) {
                Debug.Log(chemicalObject);
                Show();
            } else {
                Hide();
            }
        }
    }

    private void Show() {
            foreach (GameObject selectedObjectVisual in selectedChemicalObjectVisualArray) {
                selectedObjectVisual.SetActive(true);
            }
    }
    private void Hide() {
        foreach (GameObject selectedObjectVisual in selectedChemicalObjectVisualArray) {
            selectedObjectVisual.SetActive(false);
        }
    }
}