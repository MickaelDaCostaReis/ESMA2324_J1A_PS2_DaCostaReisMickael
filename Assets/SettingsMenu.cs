using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public void SetVolume(float _volume)
    {
        audioMixer.SetFloat("Volume", _volume);
    }

    public void SetFullScreen(bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;
    }
    public void Quit()
    {
        Application.Quit();
    }

}
