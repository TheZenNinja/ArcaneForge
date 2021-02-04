using System;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static int bufferedScene = -1;
    public static void LoadSceneWithBar(int lvlIndex)
    {
        bufferedScene = lvlIndex;
        SceneManager.LoadScene(1);
    }

    public static void LoadMenu()
    {
        bufferedScene = 0;
        SceneManager.LoadScene(0);
    }
}
