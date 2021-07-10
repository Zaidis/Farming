using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class Settings : MonoBehaviour
{
    public AudioMixer mixer;

    
    public void SetVolume(float value) {
        mixer.SetFloat("volume", value);
    }

    public void SetSensitivity(float value) {
        PlayerManager.instance.mouseSensitivity = value;
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
}
