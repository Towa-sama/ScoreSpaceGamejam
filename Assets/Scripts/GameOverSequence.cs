using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSequence : MonoBehaviour
{

    public void TurnDeathScreen()
    {
        gameObject.SetActive(true);
    }

    public void TurnDeathScreenOff()
    {
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
