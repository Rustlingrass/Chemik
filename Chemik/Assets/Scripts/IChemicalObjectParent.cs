using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChemicalObjectParent {

    public Transform GetChemicalObjectFollowTransform();
    public bool HasChemicalObject();
    public ChemicalObject GetChemicalObject();
    public void SetChemicalObject(ChemicalObject chemicalObject);
    public void ClearChemicalObject();

}