using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad = "Fabio";
    public string mainMenuScene = "MainMenuScene";
    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
