using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour {

    [SerializeField] private TubeSO tubeSO;

    public TubeSO GetTubeSO() {
        return tubeSO;
    }
}