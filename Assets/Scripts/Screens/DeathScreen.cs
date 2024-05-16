using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameOver gameOver;
    public void Respawn()
    {
        gameOver.UnDisplay();
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenu()
    {
        gameOver.UnDisplay();
        SceneManager.LoadScene("Main_Title");
    }
}
