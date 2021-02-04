using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public Slider masterSlider;
    public Slider fxSlider;
    public Slider ambientSlider;

    public AudioMixer master;

    public bool open;
    public GameObject ui;

    float _mV;
    public float masterVol
    {
        get
        {
            return _mV;
        }
        set
        {
            _mV = value;
            master.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(value, 0.001f, 1)) * 20);
        }
    }
    float _fxV;
    public float fxVol
    {
        get
        {
            return _fxV;
        }
        set
        {
            _fxV = value;
            master.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(value, 0.001f, 1)) * 20);
        }
    }
    float _aV;
    public float ambientVol
    {
        get
        {
            return _aV;
        }
        set
        {
            _aV = value;
            master.SetFloat("AmbientVolume", Mathf.Log10(Mathf.Clamp(value, 0.001f, 1)) * 20);
        }
    }

    void Start()
    {
        masterVol = masterSlider.value;
        fxVol = fxSlider.value;
        ambientVol = ambientSlider.value;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            open = !open;
            ui.SetActive(open);
            FindObjectOfType<FPCameraController>().ShowCursor(open);
        }
    }
    public void ExitToMenu() => SceneLoader.LoadMenu();
}
