using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class LakmusModelSwapSO : ScriptableObject
{
    public ChemicalObjectSO inputObject;
    public ChemicalObjectSO outputObject;
    public TubeSO keyObject;
}
