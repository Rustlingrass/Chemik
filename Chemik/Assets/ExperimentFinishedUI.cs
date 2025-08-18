using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentFinishedUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    private void Start() {
        mainMenuButton.onClick.AddListener(() => {
            Loader.LoadingScreen(0);
        });
    }
}
