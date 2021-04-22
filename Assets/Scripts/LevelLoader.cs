using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Slider progressBar;


    void Start()
    {
        StartCoroutine(LoadAsync());
        progressBar.value = 0;
    }
    IEnumerator LoadAsync()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        if (SceneLoader.bufferedScene != -1)
        {
            SceneManager.LoadScene("Universal Scene", LoadSceneMode.Single);
            AsyncOperation load = SceneManager.LoadSceneAsync(SceneLoader.bufferedScene, LoadSceneMode.Additive);
            while (!load.isDone)
            {
                progressBar.value = Mathf.Clamp01(load.progress / 0.9f);
                yield return null;
            }
            SceneLoader.bufferedScene = -1;
        }
        else
            throw new System.ArgumentOutOfRangeException("Didnt pass a scene buffer");
    }
}
