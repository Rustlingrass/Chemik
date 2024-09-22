using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserManualWindowUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button forwardButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;
    private void Start() {
        closeButton.onClick.AddListener(() => {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        });
        forwardButton.onClick.AddListener(() => {
            page1.SetActive(false); 
            page2.SetActive(true);
        });
        backButton.onClick.AddListener(() => {
            page1.SetActive(true);
            page2.SetActive(false);
        });
    }
}
