using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPage;
    public GameObject helpPage;
    public GameObject tutorialPage;

    public bool playedTutorial;
    public static Version currentTutorialVersion = new Version(0, 0, 1);
    private static string playedTutorialKey = "lastPlayedTutorialVersion";
    public void Start()
    {
        if (!PlayerPrefs.HasKey(playedTutorialKey))
            playedTutorial = false;
        else
        { 
            Version playedVersion = new Version(PlayerPrefs.GetString(playedTutorialKey).Trim());
            
            playedTutorial = !(playedVersion < currentTutorialVersion);
        }
        if (!playedTutorial)
            Debug.Log($"Need to play tutorial");
    }
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
    public void PressPlay()
    {
        if (!playedTutorial)
            ShowTutorialDialouge();
        else
            LoadLevel();
    }
    public void ShowTutorialDialouge()
    {
        mainPage.SetActive(false);
        tutorialPage.SetActive(true);
        PlayerPrefs.SetString(playedTutorialKey, currentTutorialVersion.ToString());
    }
    public void LoadTutorial()
    {
        PlayerPrefs.SetString(playedTutorialKey, currentTutorialVersion.ToString());
        SceneLoader.LoadSceneWithBar(2);
    }
    public void LoadLevel() => SceneLoader.LoadSceneWithBar(4);
    public void Exit() => Application.Quit();
}
