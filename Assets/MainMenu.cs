using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPage;
    public GameObject helpPage;
    public void ShowHelp() 
    {
        mainPage.SetActive(false);
        helpPage.SetActive(true);
    }
    public void CloseHelp()
    {
        mainPage.SetActive(true);
        helpPage.SetActive(false);
    }
public void LoadLevel() => SceneLoader.LoadSceneWithBar(2);
    public void Exit() => Application.Quit();
}
