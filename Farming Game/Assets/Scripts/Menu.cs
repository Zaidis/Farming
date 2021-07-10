using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    private void Start() {
        Cursor.lockState = CursorLockMode.None;
    }
    public void LoadTheScene() {
        SceneManager.LoadScene(1);
    }

    public void QuitTheGame() {
        Application.Quit();
    }


}
