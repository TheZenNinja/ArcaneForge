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
            AsyncOperation load = SceneManager.LoadSceneAsync(SceneLoader.bufferedScene);
            while (!load.isDone)
            {
                progressBar.value = Mathf.Clamp01(load.progress / 0.9f);
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            SceneLoader.bufferedScene = -1;
        }
        else
            throw new System.ArgumentOutOfRangeException("Didnt pass a scene buffer");
    }
}
