using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCallback : MonoBehaviour {
    public static LoadingCallback Instance {  get; private set; }
    private bool isFirstFrame = true;
    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (isFirstFrame) {
            isFirstFrame = false;
        } else {
            Loader.LoadScene();
        }
    }
}