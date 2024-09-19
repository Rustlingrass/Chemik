using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private static int lastSceneNumber;
    public static void LoadingScreen(int sceneNumber) {
        Loader.lastSceneNumber = sceneNumber;
        SceneManager.LoadScene(1);
    }
    public static void LoadScene() {
        SceneManager.LoadScene(lastSceneNumber);
    }
}
