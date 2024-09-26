using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    private const string FULLSCREEN_TOGGLE_PLAYER_PREFS = "FullscreenTogglePlayerPrefs";
    private const string SCREEN_RESOLUTION_DROPDOWN_PLAYER_PREFS = "ScreenResolutionDropdownPlayerPrefs";
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown screenResolutionDropdown;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject mainMenu;
    private int screenResolutionWidth;
    private int screenResolutionHeight;
    private FullScreenMode screenMode;

    private enum ScreenState {
        Fullscreen,
        Windowed
    };
    private enum ScreenResolutionState {
        HD,
        FullHD,
        QHD,
        QHDPlus
    };
    private void Awake() {
        string screenTemp = PlayerPrefs.GetString(FULLSCREEN_TOGGLE_PLAYER_PREFS, ScreenState.Fullscreen.ToString());
        string screenResolutionTemp = PlayerPrefs.GetString(SCREEN_RESOLUTION_DROPDOWN_PLAYER_PREFS, "Default");
        switch (screenTemp) {
            case "Fullscreen":
                screenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case "Windowed":
                screenMode = FullScreenMode.Windowed;
                break;
        }
        switch (screenResolutionTemp) {
            case "HD":
                screenResolutionWidth = 1280;
                screenResolutionHeight = 720;
                break;
            case "FullHD":
                screenResolutionWidth = 1920;
                screenResolutionHeight = 1080;
                break;
            case "QHD":
                screenResolutionWidth = 2560;
                screenResolutionHeight = 1440;
                break;
            case "QHDPlus":
                screenResolutionWidth = 2560;
                screenResolutionHeight = 1600;
                break;
            case "Default":
                screenResolutionWidth = Screen.width;
                screenResolutionHeight = Screen.height;
                break;
        }
            Screen.SetResolution(screenResolutionWidth, screenResolutionHeight, screenMode);
    }
    private void Start() {
        fullscreenToggle.onValueChanged.AddListener((bool isToggled) => {
            if (isToggled) {
                screenMode = FullScreenMode.ExclusiveFullScreen;
                PlayerPrefs.SetString(FULLSCREEN_TOGGLE_PLAYER_PREFS, ScreenState.Fullscreen.ToString());
                Screen.SetResolution(screenResolutionWidth, screenResolutionHeight, screenMode);
            } else {
                screenMode = FullScreenMode.Windowed;
                PlayerPrefs.SetString(FULLSCREEN_TOGGLE_PLAYER_PREFS, ScreenState.Windowed.ToString());
                Screen.SetResolution(screenResolutionWidth, screenResolutionHeight, screenMode);
            }
        });
        screenResolutionDropdown.onValueChanged.AddListener((value) => {
            switch (value) {
                case 0:
                    screenResolutionWidth = 1280;
                    screenResolutionHeight = 720;
                    PlayerPrefs.SetString(SCREEN_RESOLUTION_DROPDOWN_PLAYER_PREFS, ScreenResolutionState.HD.ToString());
                    Screen.SetResolution(screenResolutionWidth, screenResolutionHeight, screenMode);
                    break;
                case 1:
                    screenResolutionWidth = 1920;
                    screenResolutionHeight = 1080;
                    PlayerPrefs.SetString(SCREEN_RESOLUTION_DROPDOWN_PLAYER_PREFS, ScreenResolutionState.FullHD.ToString());
                    Screen.SetResolution(screenResolutionWidth, screenResolutionHeight, screenMode);
                    break;
                case 2:
                    screenResolutionWidth = 2560;
                    screenResolutionHeight = 1440;
                    PlayerPrefs.SetString(SCREEN_RESOLUTION_DROPDOWN_PLAYER_PREFS, ScreenResolutionState.QHD.ToString());
                    Screen.SetResolution(screenResolutionWidth, screenResolutionHeight, screenMode);
                    break;
                case 3:
                    screenResolutionWidth = 2560;
                    screenResolutionHeight = 1600;
                    PlayerPrefs.SetString(SCREEN_RESOLUTION_DROPDOWN_PLAYER_PREFS, ScreenResolutionState.QHDPlus.ToString());
                    Screen.SetResolution(screenResolutionWidth, screenResolutionHeight, screenMode);
                    break;
            }   
        });
        backButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
            mainMenu.SetActive(true);
        });
        gameObject.SetActive(false);
    }

}
