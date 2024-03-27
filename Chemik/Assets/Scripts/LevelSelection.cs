using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void BasicExperiment() 
    {
        SceneManager.LoadScene(sceneName: "BasicExperiment");
    }
    public void AcidsExperiment()
    {
        SceneManager.LoadScene(sceneName: "AcidsExperiment");
    }
    public void AmphotericExperiment()
    {
        SceneManager.LoadScene(sceneName: "AmphotericExperiment");
    }
}
