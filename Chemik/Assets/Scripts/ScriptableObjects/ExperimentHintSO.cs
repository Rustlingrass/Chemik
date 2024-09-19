using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class ExperimentHintSO : ScriptableObject
{
    public ChemikManager.Experiment experimentName;
    public int experimentHintNumber;
    public string experimentHintText;
}
