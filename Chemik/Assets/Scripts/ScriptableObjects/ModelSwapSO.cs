using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class ModelSwapSO : ScriptableObject {
    public TubeSO inputObject;
    public TubeSO outputObject;
    public ChemicalObjectSO keyObject;
}